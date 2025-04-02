using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    private static GameObject[] persistentObjects = new GameObject[10];
    public int objectIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        if(persistentObjects[objectIndex] == null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistentObjects[objectIndex] != gameObject)
        {
            // Set the position of the new object to the position of the old object
            persistentObjects[objectIndex].transform.position = gameObject.transform.position;

            Destroy(gameObject);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
