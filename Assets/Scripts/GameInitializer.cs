using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private TimeCounter _timeCounter;
    private void Awake()
    {
        StartGame();
    }

    private void StartGame()
    {
        ProjectContext.Instance.LoadLevelData(ProjectContext.Instance.LevelId, SceneType.Game);
        var progressProvider = ProjectContext.Instance.SceneContext.ProgressProvider;
        _gameUI.Initialize(progressProvider);
        _timeCounter.Initialize(progressProvider);
        Destroy(gameObject);
    }
}
