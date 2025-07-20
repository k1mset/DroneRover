using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] Vector3 movementVector;


    Vector3 startPosition;
    Vector3 endPosition;
    float movementFactor;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * movementSpeed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }

}
