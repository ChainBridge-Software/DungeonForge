using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    public void NewGame()
    {
        // Instruct the DataPersistenceManager to create a new game
        DataPersistenceManager.instance.DeleteSave();
        DataPersistenceManager.instance.NewGame();

        // Load the first level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        // Instruct the DataPersistenceManager to load the game
        DataPersistenceManager.instance.LoadGame();

        Debug.Log("Scene index: " + DataPersistenceManager.instance.gameData.currentLevelIndex);

        int sceneIndex = DataPersistenceManager.instance.gameData.currentLevelIndex;

        // Load the level that was saved
        SceneManager.LoadScene(sceneIndex);
    }
}
