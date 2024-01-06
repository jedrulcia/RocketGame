using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource ads;
    ParticleSystem particle;

    bool isTransitioning = false;

    void Start()
    {
        ads = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You've bumped into friendly object");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;

        }
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        ads.Stop();
        ads.PlayOneShot(successAudio);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        ads.Stop();
        ads.PlayOneShot(crashAudio);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void ReloadLevel()
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            Application.Quit();
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
