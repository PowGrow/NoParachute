using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject _splashScreenObject;
    [SerializeField] private TransitionAnimationController _transitionController;
    [SerializeField] private CameraAnimator _cameraAnimator;
    [SerializeField] private GameObject _buttonsContainer;
    [SerializeField] private GameObject _levelsContainer;
    private MenuInputController _menuInputController;

    public event Action RefreshLevelUiEvent;


    private void HideIntroSplash(InputAction uiAction, TransitionAnimationController transitionController, GameObject objectToHide)
    {
        uiAction.Disable();
        transitionController.ShowTransition(objectToHide,null);
    }

    public void OnPlayStoryButtonClick()
    {
        GameData.SelectedLevelId = GameData.LastSelectedLevelId;
        SubscribeAndShowTransition(_transitionController, _buttonsContainer, _levelsContainer);
    }
    public void OnNextLevelButtonClick()
    {
        GameData.SelectedLevelId = ProjectContext.Instance.SceneContext.ProgressProvider.NextLevel.LevelId;
        SubscribeAndShowTransition(_transitionController, null, null);
    }
    public void OnPreviousButtonClick()
    {
        GameData.SelectedLevelId = ProjectContext.Instance.SceneContext.ProgressProvider.PreviousLevel.LevelId;
        SubscribeAndShowTransition(_transitionController, null, _levelsContainer) ;
    }
    public void OnBackButtonClick()
    {
        GameData.LastSelectedLevelId = GameData.SelectedLevelId;
        GameData.SelectedLevelId = 100;
        SubscribeAndShowTransition(_transitionController, _levelsContainer, _buttonsContainer);
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
        ProjectContext.Instance.Initialize(GameData.SelectedLevelId, SceneType.MainMenu);
        RefreshLevelUiEvent?.Invoke();
        _transitionController.ScreenIsBlack -= OnScreenIsBlackEventHandler;
    }

    private void Awake()
    {
        _menuInputController = new MenuInputController();

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
