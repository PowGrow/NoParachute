using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuContext : MonoBehaviour, ISceneContext
{
    public ISpriteProvider SpriteProvider { get; private set; }
    public IObstacleProvider ObstacleProvider { get; }
    public IProgressProvider ProgressProvider { get; }

    public MainMenuContext(Dictionary<PrefabType, GameObject> prefabs, ISpriteProvider spriteProvider)
    {
        SpriteProvider = spriteProvider;
        GameInitialize(prefabs[PrefabType.WallController], prefabs[PrefabType.WallContainer]);
    }

    private void GameInitialize(GameObject wallControllerPrefab, GameObject wallContainerPrefab)
    {
        var wallControllerObject = Instantiate(wallControllerPrefab);
        wallControllerObject.transform.rotation = Quaternion.Euler(0, 0, -25);
        Time.timeScale = 0.25f;
        wallControllerObject.SetActive(false);
        var wallController = wallControllerObject.AddComponent<WallController>();
        wallController.Initialize(wallContainerPrefab, null);
    }
}
