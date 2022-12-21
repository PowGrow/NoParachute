using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public ISpriteProvider SpriteProvider { get; private set; }
    public IProgressProvider ProgressProvider { get; private set; }
    public IObstacleProvider ObstacleProvider { get; private set; }

    public GameContext(Dictionary<PrefabType,GameObject> prefabs, Dictionary<ProviderType,IProvider> providerList)
    {
        SpriteProvider = (ISpriteProvider)providerList[ProviderType.Sprite];
        ObstacleProvider = (IObstacleProvider)providerList[ProviderType.Obstacle];
        ProgressProvider = (IProgressProvider)providerList[ProviderType.Progress];
        GameInitialize(prefabs[PrefabType.WallController], ProgressProvider, prefabs[PrefabType.WallContainer], prefabs[PrefabType.BorderController]);
    }

    private void GameInitialize(GameObject wallControllerPrefab,IProgressProvider progressProvider, GameObject wallContainerPrefab, GameObject borderControllerPrefab)
    {
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
