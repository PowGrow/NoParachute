using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameContext : MonoBehaviour, ISceneContext
{
    public SceneType SceneType { get; private set; }
    public ISpriteProvider SpriteProvider { get; private set; }
    public IObstacleProvider ObstacleProvider { get; private set; }
    public IProgressProvider ProgressProvider { get; private set; }

    private List<GameObject> instantiatedGameObjects;

    public GameContext(Dictionary<PrefabType,GameObject> prefabs, ISpriteProvider spriteProvider, IObstacleProvider obstacleProvider, IProgressProvider progressProvider)
    {
        SceneType = SceneType.Game;
        SpriteProvider = spriteProvider;
        ObstacleProvider = obstacleProvider;
        ProgressProvider = progressProvider;
        instantiatedGameObjects = new List<GameObject>(prefabs.Count());
        GameInitialize(prefabs[PrefabType.WallController], prefabs[PrefabType.Bottom], prefabs[PrefabType.WallContainer], prefabs[PrefabType.BorderController], spriteProvider, progressProvider);
    }


    //Инициализируем игровые объекты на сцене
    private void GameInitialize(GameObject wallControllerPrefab, GameObject bottomPrefab, GameObject wallContainerPrefab, GameObject borderControllerPrefab, ISpriteProvider spriteProvider, IProgressProvider progressProvider)
    {
        Time.timeScale = 1;
        var wallControllerObject = Instantiate(wallControllerPrefab);
        var borderControllerObject = Instantiate(borderControllerPrefab, Camera.main.transform);
        var bottomObject = Instantiate(bottomPrefab);
        var bottomSpriteRenderer = bottomObject.GetComponent<SpriteRenderer>();
        bottomSpriteRenderer.color = spriteProvider.BottomColor;

        instantiatedGameObjects.AddRange(new List<GameObject>
        {
            wallControllerObject,
            borderControllerObject,
            bottomObject,
        });

        wallControllerObject.SetActive(false);
        borderControllerObject.SetActive(false);

        var wallController = wallControllerObject.AddComponent<WallController>();
        var borderController = borderControllerObject.AddComponent<BorderController>();
        wallController.Initialize(wallContainerPrefab,spriteProvider, progressProvider);
        borderController.Initialize(wallController);
    }

    public void Destroy()
    {
        foreach(GameObject gameObject in instantiatedGameObjects)
        {
            Destroy(gameObject);
        }
    }
}
