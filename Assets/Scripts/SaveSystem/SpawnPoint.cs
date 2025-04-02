using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public string playerTag = "Player";

    // Spawnpoint - when the player touches this object, save the game
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Saving...");
            
            if (DataPersistenceManager.instance != null)
            {
                DataPersistenceManager.instance.SaveGame();
            }
            else
            {
                Debug.LogError("DataPersistenceManager instance is null!");
            }

            // Set player's heals to 3
            other.GetComponent<PlayerHealth>().heals = 3;
        }
    }
}
