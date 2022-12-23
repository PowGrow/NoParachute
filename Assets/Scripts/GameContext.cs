using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour, ISceneContext
{
    public ISpriteProvider SpriteProvider { get; private set; }
    public IObstacleProvider ObstacleProvider { get; private set; }
    public IProgressProvider ProgressProvider { get; private set; }

    public GameContext(Dictionary<PrefabType,GameObject> prefabs, ISpriteProvider spriteProvider, IObstacleProvider obstacleProvider, IProgressProvider progressProvider)
    {
        SpriteProvider = spriteProvider;
        ObstacleProvider = obstacleProvider;
        ProgressProvider = progressProvider;
        GameInitialize(prefabs[PrefabType.WallController], ProgressProvider, prefabs[PrefabType.WallContainer], prefabs[PrefabType.BorderController]);
    }


    //Инициализируем игровые объекты на сцене
    private void GameInitialize(GameObject wallControllerPrefab,IProgressProvider progressProvider, GameObject wallContainerPrefab, GameObject borderControllerPrefab)
    {
        Time.timeScale = 1;
        var wallControllerObject = Instantiate(wallControllerPrefab);
        var borderControllerObject = Instantiate(borderControllerPrefab, Camera.main.transform);

        wallControllerObject.SetActive(false);
        borderControllerObject.SetActive(false);

        var wallController = wallControllerObject.AddComponent<WallController>();
        var borderController = borderControllerObject.AddComponent<BorderController>();
        wallController.Initialize(wallContainerPrefab,progressProvider);
        borderController.Initialize(wallController);
    }
}
