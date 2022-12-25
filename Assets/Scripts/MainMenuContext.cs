using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuContext : MonoBehaviour, ISceneContext
{
    public SceneType SceneType { get; private set; }
    public ISpriteProvider SpriteProvider { get; private set; }
    public IObstacleProvider ObstacleProvider { get; }
    public IProgressProvider ProgressProvider { get; }

    private List<GameObject> instantiatedGameObjects;

    public MainMenuContext(Dictionary<PrefabType, GameObject> prefabs, ISpriteProvider spriteProvider, IProgressProvider progressProvider )
    {
        SceneType = SceneType.MainMenu;
        SpriteProvider = spriteProvider;
        ProgressProvider = progressProvider;
        instantiatedGameObjects = new List<GameObject>(prefabs.Count());
        SceneInitialize(prefabs[PrefabType.WallController], prefabs[PrefabType.WallContainer], prefabs[PrefabType.Bottom], spriteProvider);
    }

    private void SceneInitialize(GameObject wallControllerPrefab, GameObject wallContainerPrefab, GameObject bottomPrefab, ISpriteProvider spriteProvider)
    {
        Time.timeScale = 0.3f;
        var wallControllerObject = Instantiate(wallControllerPrefab);
        wallControllerObject.transform.rotation = Quaternion.Euler(0, 0, -25);
        var bottomObject = Instantiate(bottomPrefab);
        var bottomSpriteRenderer = bottomObject.GetComponent<SpriteRenderer>();
        bottomSpriteRenderer.color = spriteProvider.BottomColor;

        instantiatedGameObjects.AddRange(new List<GameObject> 
        {
            wallControllerObject,
            bottomObject,
        });

        wallControllerObject.SetActive(false);
        var wallController = wallControllerObject.AddComponent<WallController>();
        wallController.Initialize(wallContainerPrefab, spriteProvider, null);
    }

    public void Destroy()
    {
        foreach (GameObject gameObject in instantiatedGameObjects)
        {
            Destroy(gameObject);
        }
    }
}
