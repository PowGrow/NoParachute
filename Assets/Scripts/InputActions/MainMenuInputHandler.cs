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
    private AudioSource _audioSource;
    private MenuInputController _menuInputController;

    public event Action RefreshLevelUiEvent;

    private const string GAME_SCENE_NAME = "Game";


    private void HideIntroSplash(InputAction uiAction, TransitionAnimationController transitionController, GameObject objectToHide)
    {
        uiAction.Disable();
        transitionController.ShowTransition(objectToHide,null);
    }

    public void OnPlayStoryButtonClick()
    {
        _audioSource.Play();
        GameData.SelectedLevelId = GameData.LastSelectedLevelId;
        SubscribeAndShowTransition(_transitionController, _buttonsContainer, _levelsContainer);
    }
    public void OnNextLevelButtonClick()
    {
        _audioSource.Play();
        GameData.SelectedLevelId = ProjectContext.Instance.SceneContext.ProgressProvider.NextLevel.LevelId;
        SubscribeAndShowTransition(_transitionController, null, null);
    }
    public void OnPreviousButtonClick()
    {
        _audioSource.Play();
        GameData.SelectedLevelId = ProjectContext.Instance.SceneContext.ProgressProvider.PreviousLevel.LevelId;
        SubscribeAndShowTransition(_transitionController, null, _levelsContainer) ;
    }
    public void OnBackButtonClick()
    {
        _audioSource.Play();
        GameData.LastSelectedLevelId = GameData.SelectedLevelId;
        GameData.SelectedLevelId = 100;
        SubscribeAndShowTransition(_transitionController, _levelsContainer, _buttonsContainer);
    }
    public void OnStartButtonClick()
    {
        _audioSource.Play();
        _transitionController.ShowTransition();
        SceneManager.LoadScene(GAME_SCENE_NAME);
    }
    public void OnExitButtonClick()
    {
        _audioSource.Play();
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
        ProjectContext.Instance.LoadLevelData(GameData.SelectedLevelId, SceneType.MainMenu);
        RefreshLevelUiEvent?.Invoke();
        _transitionController.ScreenIsBlack -= OnScreenIsBlackEventHandler;
    }

    private void Awake()
    {
        _menuInputController = new MenuInputController();
        _audioSource = GetComponent<AudioSource>();
        _menuInputController.UI.AnyKeyPressed.performed += callbackContext => HideIntroSplash(_menuInputController.UI.AnyKeyPressed, _transitionController, _splashScreenObject);
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
