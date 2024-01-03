using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;

    AudioSource ads;

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
    void StartCrashSequence()
    {
        // todo add particle effect upon crash
        isTransitioning = true;
        ads.Stop();
        ads.PlayOneShot(crashAudio);
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
    void StartSuccessSequence()
    {
        // todo add particle effect upon success
        isTransitioning = true;
        ads.Stop();
        ads.PlayOneShot(successAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);

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
