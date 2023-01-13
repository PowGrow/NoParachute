using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private CameraRotator _cameraRotator;
    [SerializeField] private PercentUI _percentUI;
    [SerializeField] private StarUI _starUi;
    [SerializeField] private GameObject _technicalDataAdd;
    private void Awake()
    {
        StartGame();
    }

    private void StartGame()
    {
        ProjectContext.Instance.LoadLevelData(ProjectContext.Instance.GameData.SelectedLevelId, SceneType.Game);
        var progressProvider = ProjectContext.Instance.SceneContext.ProgressProvider;
        var objectProvider = ProjectContext.Instance.SceneContext.ObjectProvider;

        objectProvider.AddObject("TechnicalDataHolder", _technicalDataAdd);

        _gameUI.Initialize(progressProvider);
        _scoreCounter.Initialize(progressProvider);
        _cameraRotator.Initiazlie(progressProvider);
        _percentUI.Initialize(progressProvider);
        _starUi.Initialize(progressProvider);



        Destroy(gameObject);
    }
}
