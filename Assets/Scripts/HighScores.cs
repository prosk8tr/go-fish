using UnityEngine;

public static class HighScores
{
    public static int GetStageHighScore(int stage)
    {
        string key = "HighScore_Stage" + stage;
        return PlayerPrefs.GetInt(key);
    }

    public static int GetTotalHighScore()
    {
        return PlayerPrefs.GetInt("HighScore_Total");
    }

    public static bool TrySetStageHighScore(int stage, int score)
    {
        string key = "HighScore_Stage" + stage;
        if(score > GetStageHighScore(stage))
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool TrySetTotalHighScore(int score)
    { 
        if (score > GetTotalHighScore())
        {
            PlayerPrefs.SetInt("HighScore_Total", score);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
    }
}
