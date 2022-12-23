public interface ISceneContext
{
    public ISpriteProvider SpriteProvider { get; }
    IObstacleProvider ObstacleProvider { get; }
    IProgressProvider ProgressProvider { get; }
}