using System;
using System.Linq;

public class ProgressProvider : IProgressProvider
{
    private int _currentLevelId;
    private int _levelProgress;
    private int _previousObstacleDelta;
    private int _startObstacleDelay;
    private int _obstacleToCreateIndex;
    private int _levelLength;

    private event Action<int> LevelCompleteEvent;

    private const int LEVEL_COMPLETE_DELAY = 20;

    public LevelData NextLevel { get; private set; }
    public LevelData PreviousLevel { get; private set; }

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
        NextLevel = currentLevel.NextLevel;
        PreviousLevel = currentLevel.PreviousLevel;
        _currentLevelId = currentLevel.LevelId;
        _startObstacleDelay = currentLevel.StartObstacleDelay;
        _levelLength = currentLevel.ObstacleCreateDistance.Sum() + LEVEL_COMPLETE_DELAY;
    }

    private bool IsGameScene()
    {
        return ProjectContext.Instance.SceneContext.SceneType == SceneType.Game;
    }

    public void OnProgress()
    {
        if(IsGameScene())
        {
            if (_startObstacleDelay > 0)
                _startObstacleDelay--;
            else if (_levelProgress < _levelLength)
            {
                _levelProgress++;
                _previousObstacleDelta++;
            }
            if (_levelProgress >= _levelLength)
                LevelCompleteEvent?.Invoke(_currentLevelId);
        }
    }

    private void WallCreatedEventHandler(WallEventHandler wallEventHandler)
    {
        OnProgress();
    }

    public void SubscribingOnWallCreatingEvents(WallController wallController)
    {
        wallController.WallCreatedEvent += WallCreatedEventHandler;
    }

    public void UnsubscribingFromWallCreatingEvents(WallController wallController)
    {
        wallController.WallCreatedEvent -= WallCreatedEventHandler;
    }
}
