using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabs; //Cписок игровых объектов необходимых для запуска уровня
    [SerializeField] private List<PrefabType> _prefabTypes;
    [SerializeField] private float _levelCompleteDelay;

    public int LevelId { get; set; }
    public float LevelCompleteDelay
    {
        get { return _levelCompleteDelay; }
    }
    public ISceneContext SceneContext { get; private set; }

    public GameData GameData { get; set; }
    public static ProjectContext Instance { get; private set; }

    public void LoadLevelData(int levelId, SceneType scene)
    {
        var levelData = Resources.Load<LevelData>($"ScriptableObjects/Levels/Level_{levelId}");
        InitializeSceneByData(levelData, scene);
    }

    private void InitializeSceneByData(LevelData levelData, SceneType sceneType)
    {
        var prefabs = GetPrefabDictionary(_prefabs, _prefabTypes);
        if (SceneContext != null)
            SceneContext.Destroy();
        InitializeProvidersAndSceneContext(levelData, prefabs, sceneType);
    }

    private void InitializeProvidersAndSceneContext(LevelData levelData, Dictionary<PrefabType, GameObject> prefabs, SceneType sceneType)
    {
        var spriteProvider = new SpriteProvider(levelData);
        var obstacleProvider = new ObstacleProvider(levelData);
        var progressProvider = new ProgressProvider(levelData);
        var objectProvider = new ObjectProvider();
        var soundProvider = new SoundProvider(levelData);
        switch (sceneType)
        {
            case (SceneType.MainMenu):
                MainMenuInitialize(prefabs, spriteProvider, progressProvider,objectProvider, soundProvider);
            break;
            case (SceneType.Game):
                GameInitialize(prefabs, spriteProvider, obstacleProvider, progressProvider, objectProvider, soundProvider);
            break;
        }
    }

    private void GameInitialize(Dictionary<PrefabType, GameObject> prefabs, ISpriteProvider spriteProvider, IObstacleProvider obstacleProvider, IProgressProvider progressProvider, IObjectProvider objectProvider, ISoundProvider soundProvider)
    {
        SceneContext = new GameContext(prefabs, spriteProvider, obstacleProvider, progressProvider, objectProvider, soundProvider);
    }

    private void MainMenuInitialize(Dictionary<PrefabType, GameObject> prefabs, ISpriteProvider spriteProvider, IProgressProvider progressProvider, IObjectProvider objectProvider, ISoundProvider soundProvider)
    {
        SceneContext = new MainMenuContext(prefabs, spriteProvider, progressProvider,objectProvider, soundProvider);
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
        try
        {
            GameData = SaveLoader.TryToLoadData();
        }
        catch(FileNotFoundException)
        {
            GameData = new GameData(0, 0, 0);
        }
        DontDestroyOnLoad(this);
    }
}
