using UnityEngine;

public class Oscillator : MonoBehaviour
{
    const float tau = Mathf.PI * 2;
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 4f;

    private void Start() {
        startingPos = transform.position;
    }

    private void Update() {
        if (period <= Mathf.Epsilon ){return;}
        float cycles = Time.time / period; //continually growing over time
        float rawSineWave = Mathf.Sin(tau * cycles); //going from -1 to 1
        movementFactor = (rawSineWave + 1f)/2f; //recalc to go from 0 to 1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }

    /*
    when compariing floats
    two floats can vary by a tiny amount
    so u have to specify the acceptable difference
    the smallest float is Mathf.Epsilon
    so always compare to that vs zero
    so for example instead of
    if (variable == 0)
    it should be
    if (variable <= Mathf.Epsilon)
    */
    
}
