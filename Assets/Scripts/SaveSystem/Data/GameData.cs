using UnityEngine;

[System.Serializable]
public class GameData
{

    // Game Data
    public int currentLevelIndex;

    // Player Data
    public Vector3 player_position;



    public GameData()
    {
        this.currentLevelIndex = 0;
        this.player_position = new Vector3(-60, 37, 0);
    }

}
