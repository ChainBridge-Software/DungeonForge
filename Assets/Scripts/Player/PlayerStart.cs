using UnityEngine;

public class PlayerStart : MonoBehaviour
{

    public bool teleportPlayerOnStart = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(teleportPlayerOnStart)
        {
            // Teleport player to start position
            GameObject player = GameObject.Find("Player");
            player.transform.position = this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
