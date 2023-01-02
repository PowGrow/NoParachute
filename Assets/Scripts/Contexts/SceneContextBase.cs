using System.Collections.Generic;
using UnityEngine;

public abstract class SceneContextBase : MonoBehaviour, ISceneContext
{
    public SceneType SceneType { get; protected set; }
    public ISpriteProvider SpriteProvider { get; protected set; }
    public IObstacleProvider ObstacleProvider { get; protected set; }
    public IProgressProvider ProgressProvider { get; protected set; }
    public IObjectProvider ObjectProvider { get; protected set; }

    protected List<GameObject> InstantiatedGameObjects;

    protected SpriteRenderer BottomSpriteRenderer;
    protected SpriteRenderer BackgroundSpriteRenderer;

    protected abstract void InitializeScene(GameObject wallControllerPrefab, GameObject wallContainerPrefab, GameObject bottomPrefab, GameObject backgroundPrefab, GameObject borderControllerPrefab, GameObject wallAnimator, ISpriteProvider spriteProvider, IProgressProvider progressProvider);

    public void SwitchLevelData(LevelData levelData)
    {
        SpriteProvider.SwitchSpriteData(levelData);
        ObstacleProvider.SwitchObstacleData(levelData);
        BottomSpriteRenderer.color = SpriteProvider.BottomColor;
        BackgroundSpriteRenderer.color = SpriteProvider.BackgroundColor;
    }

    public void Destroy()
    {
        foreach (GameObject gameObject in InstantiatedGameObjects)
        {
            Destroy(gameObject);
        }
    }
}
