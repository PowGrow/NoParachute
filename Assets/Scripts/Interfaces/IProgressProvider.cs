using System;

public interface IProgressProvider
{
    public event Action LevelCompletedEvent;

    public int LevelId { get;}
    public string LevelName { get;}
    public LevelData NextLevel { get; }
    public LevelData PreviousLevel { get; }
    public int PreviousObstacleDelta { get; set; }
    public int ObstacleToCreateIndex { get; set; }

    public void OnProgress();

    public void SubscribingOnWallCreatingEvents(WallController wallController);

    public void UnsubscribingFromWallCreatingEvents(WallController wallController);
}
