using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneUponCollide : MonoBehaviour
{

    public string sceneName;
    public string collisionTag;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(collisionTag))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
