public class ProgressProvider : IProgressProvider, IProvider
{
    private int _levelProgress;
    private int _previousObstacleDelta;
    private int _startObstacleDelay;
    private int _obstacleToCreateIndex;

    public Status Status { get; private set; }

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

    public ProgressProvider(LevelData currentLevel)
    {
        Status = Status.Starting;
        _startObstacleDelay = currentLevel.StartObstacleDelay;
        Status = Status.Running;
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
