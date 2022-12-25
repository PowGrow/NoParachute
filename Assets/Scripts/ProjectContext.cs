using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabs; //Cписок игровых объектов необходимых для запуска уровня
    [SerializeField] private List<PrefabType> _prefabTypes;

    public int LevelId { get; set; }
    public ISceneContext SceneContext { get; private set; }

    public GameData GameData { get; private set; }
    public static ProjectContext Instance { get; private set; }

    public void Initialize(int levelId, SceneType scene)
    {
        var levelData = Resources.Load<LevelData>($"ScriptableObjects/Levels/Level_{levelId}");
        InitializeSceneByData(levelData, scene);
    }

    private void InitializeSceneByData(LevelData levelData, SceneType scene)
    {
        var prefabs = GetPrefabDictionary(_prefabs, _prefabTypes);
        if (SceneContext != null)
            SceneContext.Destroy();
        switch (scene)
        {
            case (SceneType.MainMenu):
                MainMenuInitialize(levelData, prefabs);
                break;
            case (SceneType.Game):
                GameInitialize(levelData, prefabs);
                break;
        }
    }

    private void GameInitialize(LevelData currentLevel, Dictionary<PrefabType, GameObject> prefabs)
    {
        var spriteProvider = new SpriteProvider(currentLevel);
        var obstacleProvider = new ObstacleProvider(currentLevel);
        var progressProvider = new ProgressProvider(currentLevel);
        SceneContext = new GameContext(prefabs, spriteProvider, obstacleProvider, progressProvider);
    }

    private void MainMenuInitialize(LevelData currentLevel, Dictionary<PrefabType, GameObject> prefabs)
    {
        var spriteProvider = new SpriteProvider(currentLevel);
        var progressProvider = new ProgressProvider(currentLevel);
        SceneContext = new MainMenuContext(prefabs, spriteProvider, progressProvider);
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
        catch(FileNotFoundException e)
        {
            GameData = new GameData(0, 0);
        }
        DontDestroyOnLoad(this);
    }
}
