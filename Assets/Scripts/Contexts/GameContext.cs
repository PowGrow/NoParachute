using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameContext : SceneContextBase
{
    public GameContext(Dictionary<PrefabType,GameObject> prefabs, ISpriteProvider spriteProvider, IObstacleProvider obstacleProvider, IProgressProvider progressProvider, IObjectProvider objectProvider, ISoundProvider soundProvider)
    {
        SceneType = SceneType.Game;
        SpriteProvider = spriteProvider;
        ObstacleProvider = obstacleProvider;
        ProgressProvider = progressProvider;
        ObjectProvider = objectProvider;
        SoundProvider = soundProvider;
        InstantiatedGameObjects = new List<GameObject>(prefabs.Count());
        InitializeScene(prefabs[PrefabType.WallController], prefabs[PrefabType.WallContainer], prefabs[PrefabType.Bottom],prefabs[PrefabType.Background], prefabs[PrefabType.BorderController], prefabs[PrefabType.WallAnimator], spriteProvider, progressProvider);
    }

    protected override void InitializeScene(GameObject wallControllerPrefab, GameObject wallContainerPrefab, GameObject bottomPrefab, GameObject backgroundPrefab, GameObject borderControllerPrefab, GameObject wallAnimatorPrefab, ISpriteProvider spriteProvider, IProgressProvider progressProvider)
    {
        Time.timeScale = 1;
        var wallControllerObject = Instantiate(wallControllerPrefab);
        var wallAnimatorObject = Instantiate(wallAnimatorPrefab);
        var borderControllerObject = Instantiate(borderControllerPrefab,AnimationParent.Instance.transform);
        var bottomObject = Instantiate(bottomPrefab);
        var backgroundObject = Instantiate(backgroundPrefab);
        var backgrounSpriteRenderer = backgroundObject.GetComponent<SpriteRenderer>();
        var bottomSpriteRenderer = bottomObject.GetComponent<SpriteRenderer>();
        bottomSpriteRenderer.color = spriteProvider.BottomColor;
        backgrounSpriteRenderer.color = spriteProvider.BackgroundColor;

        BottomSpriteRenderer = bottomSpriteRenderer;
        BackgroundSpriteRenderer = backgrounSpriteRenderer;

        InstantiatedGameObjects.AddRange(new List<GameObject>
        {
            wallControllerObject,
            borderControllerObject,
            bottomObject,
            backgroundObject,
            wallAnimatorObject,
        });

        wallControllerObject.SetActive(false);
        wallAnimatorObject.SetActive(false);
        borderControllerObject.SetActive(false);

        WallController = wallControllerObject.AddComponent<WallController>();
        var wallAnimator = wallAnimatorObject.AddComponent<WallAnimator>();
        var borderController = borderControllerObject.AddComponent<BorderController>();
        WallController.Initialize(wallContainerPrefab, progressProvider, wallAnimator);
        wallAnimator.Initialize(WallController);
        borderController.Initialize(WallController);
    }
}
