public interface ISceneContext
{
    public SceneType SceneType { get; }
    public ISpriteProvider SpriteProvider { get; }
    IObstacleProvider ObstacleProvider { get; }
    IProgressProvider ProgressProvider { get; }

    public void Destroy();
}