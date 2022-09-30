using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - eg references for readability or speed
    // STATE - private instances (members) variables
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 50f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }

    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            PlayEngineSound();
        }
        if (!mainParticles.isPlaying)
        {
            mainParticles.Play();
        }
    }

    void StopThrusting()
    {
        mainParticles.Stop();
        StopEngineSound();
    }

    private void RotateLeft()
    {
        if (!rightParticles.isPlaying)
        {
            rightParticles.Play();
        }
        ApplyRotation(rotateThrust);
    }

    private void RotateRight()
    {
        if (!leftParticles.isPlaying)
        {
            leftParticles.Play();
        }
        ApplyRotation(-rotateThrust);
    }

    private void StopRotation()
    {
        leftParticles.Stop();
        rightParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void PlayEngineSound()
    {
        audioSource.PlayOneShot(mainEngine);
    }

    void StopEngineSound()
    {
        audioSource.Stop();
    }
}
