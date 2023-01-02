using System;

[Serializable]
public class LevelStats
{
    public LevelStats(int highScore, float bestTime, int deaths, int limbsLost)
    {
        HighScore = highScore;
        BestTime = bestTime;
        Deaths = deaths;
        LimbsLost = limbsLost;
    }
    public int HighScore;
    public float BestTime;
    public int Deaths;
    public int LimbsLost;
}
