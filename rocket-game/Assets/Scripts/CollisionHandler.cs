using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You've bumped into friendly object");
                break;
            case "Finish":
                Debug.Log("You've won");
                break;
            default:
                Debug.Log("You've bumped into thing");
                break;

        }
    }
}
