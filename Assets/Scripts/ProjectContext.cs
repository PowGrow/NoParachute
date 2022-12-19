using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    [SerializeField] private GameObject _wallControllerPrefab;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _borderControllerPrefab;

    private int _levelId;
    public ISpriteProvider SpriteProvider { get; private set; }
    public IProgressHandler LevelProgressHandler { get; private set; }
    public GameContext GameContext { get; private set; }
    public static ProjectContext Instance { get; private set; }

    public int LevelId
    {
        get { return _levelId; }
        set { _levelId = value; }
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Initialize(int levelId)
    {
        
        var currentLevel = Resources.Load<Level>($"ScriptableObjects/Levels/Level_{levelId}");
        SpriteProvider = new SpriteProvider(currentLevel);
        LevelProgressHandler = new LevelProgressHandler(currentLevel);
        GameContext = new GameContext(_wallControllerPrefab, _wallPrefab, _borderControllerPrefab, LevelProgressHandler, SpriteProvider);
    }
}
