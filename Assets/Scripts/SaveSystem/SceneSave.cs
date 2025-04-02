using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSave : MonoBehaviour, IDataPersistence
{

    public void LoadData(GameData data)
    {
        Debug.Log("Current level: " + data.currentLevelIndex);
    }

    public void SaveData(ref GameData data)
    {
        // Check if the current scene is not the main menu
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            data.currentLevelIndex = currentSceneIndex;
        }
    }


}
