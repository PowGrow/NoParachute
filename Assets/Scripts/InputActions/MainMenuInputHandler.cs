using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject _splashScreenObject;
    [SerializeField] private TransitionAnimationController _transitionController;
    [SerializeField] private CameraAnimator _cameraAnimator;
    [SerializeField] private GameObject _buttonsContainer;
    [SerializeField] private GameObject _levelsContainer;
    private MenuInputController _menuInputController;
    private GameData _gameData;

    public event Action RefreshLevelUiEvent;

    private const string GAME_SCENE_NAME = "Game";


    private void HideIntroSplash(InputAction uiAction, TransitionAnimationController transitionController, GameObject objectToHide)
    {
        uiAction.Disable();
        transitionController.ShowTransition(objectToHide,null);
    }

    public void OnPlayStoryButtonClick()
    {
        _gameData.SelectedLevelId = _gameData.LastSelectedLevelId;
        SubscribeAndShowTransition(_transitionController, _buttonsContainer, _levelsContainer);
    }
    public void OnNextLevelButtonClick()
    {
        if (ProjectContext.Instance.SceneContext.ProgressProvider.NextLevel.LevelId == ProjectContext.Instance.SceneContext.ProgressProvider.LevelId)
            _gameData.SelectedLevelId = 0;
        else
            _gameData.SelectedLevelId = ProjectContext.Instance.SceneContext.ProgressProvider.NextLevel.LevelId;
        SubscribeAndShowTransition(_transitionController, null, null);
    }
    public void OnPreviousButtonClick()
    {
        _gameData.SelectedLevelId = ProjectContext.Instance.SceneContext.ProgressProvider.PreviousLevel.LevelId;
        SubscribeAndShowTransition(_transitionController, null, _levelsContainer) ;
    }
    public void OnBackButtonClick()
    {
        _gameData.LastSelectedLevelId = _gameData.SelectedLevelId;
        _gameData.SelectedLevelId = 100;
        SubscribeAndShowTransition(_transitionController, _levelsContainer, _buttonsContainer);
    }
    public void OnStartButtonClick()
    {
        _transitionController.ShowTransition();
        SceneManager.LoadScene(GAME_SCENE_NAME);
    }
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
    private void SubscribeAndShowTransition(TransitionAnimationController transitionController, GameObject objectToHide, GameObject objectToShow)
    {
        transitionController.ScreenIsBlack += OnScreenIsBlackEventHandler;
        transitionController.ShowTransition(objectToHide, objectToShow);
    }

    private void OnScreenIsBlackEventHandler()
    {
        _cameraAnimator.PlayStartAnimation();
        ProjectContext.Instance.LoadLevelData(_gameData.SelectedLevelId, SceneType.MainMenu);
        RefreshLevelUiEvent?.Invoke();
        _transitionController.ScreenIsBlack -= OnScreenIsBlackEventHandler;
    }

    private void Awake()
    {
        _menuInputController = new MenuInputController();

        _menuInputController.UI.AnyKeyPressed.performed += callbackContext => HideIntroSplash(_menuInputController.UI.AnyKeyPressed, _transitionController, _splashScreenObject);
    }

    private void Start()
    {
        _gameData = ProjectContext.Instance.GameData;
    }

    private void OnEnable()
    {
        _menuInputController.Enable();
    }

    private void OnDisable()
    {
        _menuInputController.Disable();
    }
}
