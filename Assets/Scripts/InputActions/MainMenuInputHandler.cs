using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject _splashScreenObject;
    [SerializeField] private TransitionAnimationController _transitionController;
    private MenuInputController _menuInputController;


    private void HideIntroSplash(InputAction uiAction, TransitionAnimationController controller, GameObject objectToHide)
    {
        uiAction.Disable();
        controller.ShowTransition(objectToHide);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
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
