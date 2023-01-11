using System;

[Serializable]
public class LevelStats
{
    public LevelStats(int highScore, float bestTime, int deaths, int limbsLost, bool[] stars)
    {
        HighScore = highScore;
        BestTime = bestTime;
        Deaths = deaths;
        LimbsLost = limbsLost;
        Stars = stars;
    }
    public int HighScore;
    public float BestTime;
    public int Deaths;
    public int LimbsLost;
    public bool[] Stars;
}
