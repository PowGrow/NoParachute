using UnityEngine;

public class ProgressHandler : MonoBehaviour, IProgressHandler
{
    private int _levelProgress;
    private int _levelLength;
    private int _previousObstacleDelta;
    private int _startObstacleDelay;
    private int _obstacleToCreateIndex;

    public int LevelProgress
    {
        get { return _levelProgress; }
    }
    public int PreviousObstacleDelta
    {
        get { return _previousObstacleDelta; }
        set { _previousObstacleDelta = value; }
    }
    public int ObstacleToCreateIndex
    {
        get { return _obstacleToCreateIndex; }
        set { _obstacleToCreateIndex = value; }
    }

    public ProgressHandler(LevelData currentLevel)
    {
        _startObstacleDelay = currentLevel.StartObstacleDelay;
    }

    public void OnProgress()
    {
        if (_startObstacleDelay > 0)
            _startObstacleDelay--;
        else
        {
            _levelProgress++;
            _previousObstacleDelta++;
        }

    }
}
