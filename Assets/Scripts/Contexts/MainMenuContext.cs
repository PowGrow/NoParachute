using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuContext : SceneContextBase
{
    public MainMenuContext(Dictionary<PrefabType, GameObject> prefabs, ISpriteProvider spriteProvider, IProgressProvider progressProvider, IObjectProvider objectProvider )
    {
        SceneType = SceneType.MainMenu;
        SpriteProvider = spriteProvider;
        ProgressProvider = progressProvider;
        ObjectProvider = objectProvider;
        InstantiatedGameObjects = new List<GameObject>(prefabs.Count());
        InitializeScene(prefabs[PrefabType.WallController], prefabs[PrefabType.WallContainer], prefabs[PrefabType.Bottom],prefabs[PrefabType.Background], prefabs[PrefabType.BorderController], prefabs[PrefabType.WallAnimator], spriteProvider, progressProvider);
    }

    protected override void InitializeScene(GameObject wallControllerPrefab, GameObject wallContainerPrefab, GameObject bottomPrefab, GameObject backgroundPrefab, GameObject borderControllerPrefab, GameObject wallAnimatorPrefab, ISpriteProvider spriteProvider, IProgressProvider progressProvider)
    {
        Time.timeScale = 0.3f;
        var wallControllerObject = Instantiate(wallControllerPrefab);
        wallControllerObject.transform.rotation = Quaternion.Euler(0, 0, -25);
        var bottomObject = Instantiate(bottomPrefab);
        var backgroundObject = Instantiate(backgroundPrefab);
        var bottomSpriteRenderer = bottomObject.GetComponent<SpriteRenderer>();
        var backgrounSpriteRenderer = backgroundObject.GetComponent<SpriteRenderer>();

        bottomSpriteRenderer.color = spriteProvider.BottomColor;
        backgrounSpriteRenderer.color = spriteProvider.BackgroundColor;

        BottomSpriteRenderer = bottomSpriteRenderer;
        BackgroundSpriteRenderer = backgrounSpriteRenderer;

        InstantiatedGameObjects.AddRange(new List<GameObject> 
        {
            wallControllerObject,
            bottomObject,
            backgroundObject,
        });

        wallControllerObject.SetActive(false);
        var wallController = wallControllerObject.AddComponent<WallController>();
        wallController.Initialize(wallContainerPrefab, null, null);
    }
}
