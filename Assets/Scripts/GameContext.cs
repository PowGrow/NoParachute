using UnityEngine;

public class GameContext : MonoBehaviour
{
    private IProgressHandler _progressHandler;
    private ISpriteProvider _spriteProvider;
    private static WallController _wallController;
    private static BorderController _borderController;

    public static WallController WallController
    {
        get { return _wallController; }
        private set { _wallController = value; }
    }
    public static BorderController BorderController
    {
        get { return _borderController; }
        private set { _borderController = value; }
    }

    public GameContext(GameObject wallControllerPrefab, GameObject wallPrefab, GameObject borderControllerPrefab, IProgressHandler levelProgressHandler, ISpriteProvider spriteProvider)
    {
        _progressHandler = levelProgressHandler;
        _spriteProvider = spriteProvider;
        GameInitialize(wallControllerPrefab, wallPrefab, borderControllerPrefab, WallController, BorderController, _progressHandler,_spriteProvider);
    }

    private void GameInitialize(GameObject wallControllerPrefab, GameObject wallPrefab, GameObject borderControllerPrefab, WallController wallController, BorderController borderController,
                                IProgressHandler progressHandler, ISpriteProvider spriteProvider)
    {
        var wallControllerObject = Instantiate(wallControllerPrefab);
        var borderControllerObject = Instantiate(borderControllerPrefab, Camera.main.transform);
        var wallSpriteChanger = wallControllerObject.GetComponent<SpriteChanger>();

        wallControllerObject.SetActive(false);
        borderControllerObject.SetActive(false);

        wallController = wallControllerObject.AddComponent<WallController>();
        borderController = borderControllerObject.AddComponent<BorderController>();
        wallController.Initialize(wallPrefab, progressHandler);
        wallSpriteChanger.Initialize(spriteProvider);
        borderController.Initialize(wallController);
    }
}
