using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] float thrustStrength = 10f;
    [SerializeField] InputAction rotation;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrusterParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;

    AudioSource audioSource;
    Rigidbody rb;


    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        rb.freezeRotation = true;

        if (rotationInput < 0)
        {
            RotateLeft();
        }
        else if (rotationInput > 0)
        {
            RotateRight();
        }
        else
        {
            leftThrusterParticle.Stop();
            rightThrusterParticle.Stop();
        }

        
        rb.freezeRotation = false;
    }

    private void RotateRight()
    {
        // right
        transform.Rotate(Vector3.back * rotationSpeed * Time.fixedDeltaTime);
        if (!rightThrusterParticle.isPlaying)
        {
            rightThrusterParticle.Play();
        }
    }

    private void RotateLeft()
    {
        //left
        transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
        if (!leftThrusterParticle.isPlaying)
        {
            leftThrusterParticle.Play();
        }
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            audioSource.Stop();
            mainThrusterParticle.Stop();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainThrusterParticle.isPlaying)
        {
            mainThrusterParticle.Play();
        }
    }
}
