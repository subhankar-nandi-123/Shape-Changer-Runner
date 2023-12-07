using UnityEngine;

public class GameLevelConttroller 
{

    public void setCurrentGameLevel(int level)
    {
        if (level > 0)
        {
            PlayerPrefs.SetInt("GameLevel", level);
        }
    }

    public int getCurrentGameLevel()
    {
        int gameLevel = PlayerPrefs.GetInt("GameLevel") <= 0 ? 1 : PlayerPrefs.GetInt("GameLevel");
        return gameLevel;
    }
}
