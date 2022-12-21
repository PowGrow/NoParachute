using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabs;
    [SerializeField] private List<PrefabType> _prefabTypes;
    [SerializeField] private int _startObstacleDelay;

    public int LevelId { get; set; }
    public GameContext GameContext { get; private set; }
    public static ProjectContext Instance { get; private set; }

    public void Initialize(int levelId)
    {
        var currentLevel = Resources.Load<LevelData>($"ScriptableObjects/Levels/Level_{levelId}");
        var providerList = InstantiateProviders(currentLevel);
        var prefabs = GetPrefabDictionary(_prefabs, _prefabTypes);
        GameContext = new GameContext(prefabs, providerList);
    }
    private Dictionary<ProviderType,IProvider> InstantiateProviders(LevelData currentLevel)
    {
        var providers = new Dictionary<ProviderType, IProvider>
        {
            { ProviderType.Sprite, new SpriteProvider(currentLevel) },
            { ProviderType.Obstacle, new ObstacleProvider(currentLevel) },
            { ProviderType.Progress, new ProgressProvider(currentLevel) }
        };
        return providers;
        
    }
    private Dictionary<PrefabType, GameObject> GetPrefabDictionary(List<GameObject> prefabs, List<PrefabType> prefabTypes)
    {
        var dictionary = new Dictionary<PrefabType, GameObject>();
        for(int i = 0; i < prefabs.Count(); i++)
        {
            dictionary.Add(prefabTypes[i], prefabs[i]);
        }
        return dictionary;
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
