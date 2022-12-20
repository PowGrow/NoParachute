using UnityEngine;

public class GameContext : MonoBehaviour
{
    private ISpriteProvider _spriteProvider;
    private IObstacleProvider _obstacleProvider;
    private IProgressHandler _progressHandler;

    public ISpriteProvider SpriteProvider
    {
        get { return _spriteProvider; }
        private set { _spriteProvider = value; }
    }
    public IProgressHandler ProgressHandler
    {
        get { return _progressHandler; }
        private set { _progressHandler = value; }
    }
    public IObstacleProvider ObstacleProvider
    {
        get { return _obstacleProvider; }
        private set { _obstacleProvider = value; }
    }

    public GameContext(GameObject wallControllerPrefab, GameObject wallContainerPrefab, GameObject borderControllerPrefab, IProgressHandler progressHandler, ISpriteProvider spriteProvider, IObstacleProvider obstacleProvider)
    {
        _spriteProvider = spriteProvider;
        _obstacleProvider = obstacleProvider;
        _progressHandler = progressHandler;
        GameInitialize(wallControllerPrefab,progressHandler, wallContainerPrefab, borderControllerPrefab);
    }

    private void GameInitialize(GameObject wallControllerPrefab,IProgressHandler progressHandler, GameObject wallContainerPrefab, GameObject borderControllerPrefab)
    {
        var wallControllerObject = Instantiate(wallControllerPrefab);
        var borderControllerObject = Instantiate(borderControllerPrefab, Camera.main.transform);

        wallControllerObject.SetActive(false);
        borderControllerObject.SetActive(false);

        var wallController = wallControllerObject.AddComponent<WallController>();
        var borderController = borderControllerObject.AddComponent<BorderController>();
        wallController.Initialize(wallContainerPrefab,progressHandler);
        borderController.Initialize(wallController);
    }
}
