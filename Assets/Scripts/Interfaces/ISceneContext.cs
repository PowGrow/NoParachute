public interface ISceneContext
{
    public SceneType SceneType { get; }
    public ISpriteProvider SpriteProvider { get; }
    IObstacleProvider ObstacleProvider { get; }
    IProgressProvider ProgressProvider { get; }
    IObjectProvider ObjectProvider { get; }

    public void SwitchLevelData(LevelData levelData);
    public void Destroy();
}