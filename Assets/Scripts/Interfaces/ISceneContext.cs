public interface ISceneContext
{
    public SceneType SceneType { get; }
    public ISpriteProvider SpriteProvider { get; }
    IObstacleProvider ObstacleProvider { get; }
    IProgressProvider ProgressProvider { get; }
    IObjectProvider ObjectProvider { get; }
    ISoundProvider SoundProvider { get; }

    public WallController WallController { get;}
    public BorderController BorderController { get; }
    public WallAnimator WallAnimator { get; }

    public void SwitchLevelData(LevelData levelData);
    public void Destroy();
}