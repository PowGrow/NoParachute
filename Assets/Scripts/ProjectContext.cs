using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabs; //Cписок игровых объектов необходимых для запуска уровня
    [SerializeField] private List<PrefabType> _prefabTypes;

    public int LevelId { get; set; }
    public ISceneContext SceneContext { get; private set; }
    public static ProjectContext Instance { get; private set; }

    public void Initialize(int levelId, SceneType scene)
    {
        var currentLevel = Resources.Load<LevelData>($"ScriptableObjects/Levels/Level_{levelId}");
        var prefabs = GetPrefabDictionary(_prefabs, _prefabTypes);
        switch (scene)
        {
            case (SceneType.MainMenu):
                MainMenuInitialize(currentLevel, prefabs);
                break;
            case (SceneType.Game):
                GameInitialize(currentLevel, prefabs);
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
        SceneContext = new MainMenuContext(prefabs, spriteProvider);
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
