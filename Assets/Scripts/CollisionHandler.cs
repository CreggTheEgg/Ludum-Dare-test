using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip landingSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem explosionParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)){
            collisionDisabled = !collisionDisabled;
        }
    }
    private void OnCollisionEnter(Collision other) {
        if (isTransitioning || collisionDisabled) {return;}
        
        switch (other.gameObject.tag)
        {
            case "Finish":
                StartWinSequence(); 
                break;
            case "Friendly":
                break;
            case "Fuel":
                break;
            default:
                StartCrashSequence();
                break;
        }
    
    }

    void StartCrashSequence()
    {
        explosionParticles.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }

    void StartWinSequence()
    {
        successParticles.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(landingSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", 1f);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
