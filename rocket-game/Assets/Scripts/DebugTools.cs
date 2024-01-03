using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugTools : MonoBehaviour
{
    CapsuleCollider collider;

    bool isColliderActive = true;
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            TurnOffCollisions();
        }
    }
    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log("DebugTools: Next scene loaded");
    }
    void TurnOffCollisions()
    {
        if (isColliderActive)
        {
            GetComponent<Collider>().enabled = false;
            isColliderActive = false;
            Debug.Log("DebugTools: Collisions turned off");
        }
        else if (!isColliderActive)
        {
            GetComponent<Collider>().enabled = true;
            isColliderActive = true;
            Debug.Log("DebugTools: Collisions turned on");
        }
    }
}
