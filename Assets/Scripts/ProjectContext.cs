using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    [SerializeField] private GameObject _wallControllerPrefab;
    [SerializeField] private GameObject _wallContainer;
    [SerializeField] private GameObject _borderControllerPrefab;
    [SerializeField] private int _startObstacleDelay;

    public int LevelId { get; set; }
    public GameContext GameContext { get; private set; }
    public static ProjectContext Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Initialize(int levelId)
    {
        var currentLevel = Resources.Load<LevelData>($"ScriptableObjects/Levels/Level_{levelId}");
        var spriteProvider = new SpriteProvider(currentLevel);
        var obstacleProvider = new ObstacleProvider(currentLevel);
        var progressHandler = new ProgressHandler(currentLevel);
        GameContext = new GameContext(_wallControllerPrefab, _wallContainer, _borderControllerPrefab, progressHandler, spriteProvider, obstacleProvider);
    }
}
