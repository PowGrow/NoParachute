public interface IProgressHandler
{
    public int PreviousObstacleDelta { get; set; }
    public int ObstacleToCreateIndex { get; set; }
    public void OnProgress();
}
