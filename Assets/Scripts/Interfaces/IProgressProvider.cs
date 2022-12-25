public interface IProgressProvider
{
    public LevelData NextLevel { get; }
    public LevelData PreviousLevel { get; }
    public int PreviousObstacleDelta { get; set; }
    public int ObstacleToCreateIndex { get; set; }
    public void OnProgress();

    public void SubscribingOnWallCreatingEvents(WallController wallController);

    public void UnsubscribingFromWallCreatingEvents(WallController wallController);
}
