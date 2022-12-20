using UnityEngine;

public class ProgressHandler : MonoBehaviour, IProgressHandler
{
    private int _levelProgress;
    private int _levelLength;
    private int _previousObstacleDelta;
    private int _startObstacleDelay;

    public int LevelProgress
    {
        get { return _levelProgress; }
    }
    public int PreviousObstacleDelta
    {
        get { return _previousObstacleDelta; }
        set { _previousObstacleDelta = value; }
    }

    public ProgressHandler(Level currentLevel,int startObstacleDealy)
    {
        _levelLength = currentLevel.LevelLength;
        _startObstacleDelay = startObstacleDealy;
    }

    public void OnProgress()
    {
        _levelProgress++;
        if (_startObstacleDelay > 0)
            _startObstacleDelay--;
        else
            _previousObstacleDelta++;
        if (_levelProgress >= _levelLength)
            Debug.Log("Finish!");
    }
}
