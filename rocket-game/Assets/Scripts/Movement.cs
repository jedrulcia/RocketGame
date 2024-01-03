using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip rocketFlying;
    [SerializeField] AudioClip rocketRotating;
    [SerializeField] ParticleSystem firstEngineParticles;
    [SerializeField] ParticleSystem secondEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
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
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(rocketFlying);
        }
        if (!firstEngineParticles.isPlaying && !secondEngineParticles.isPlaying)
        {
            firstEngineParticles.Play();
            secondEngineParticles.Play();
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        firstEngineParticles.Stop();
        secondEngineParticles.Stop();
    }
    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!leftEngineParticles.isPlaying)
        {
            leftEngineParticles.Play();
        }
    }
    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!rightEngineParticles.isPlaying)
        {
            rightEngineParticles.Play();
        }
    }
    private void StopRotating()
    {
        leftEngineParticles.Stop();
        rightEngineParticles.Stop();
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
