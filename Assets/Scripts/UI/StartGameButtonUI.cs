using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class StartGameButtonUI : MonoBehaviour
{
    [SerializeField] private MainMenuInputHandler _mainMenuInputHandler;
    private TextMeshProUGUI _label;
    private Button _button;
    private GameData _gameData;

    private void RefreshButtonState()
    {
        if (_gameData.UnlockedLevels < _gameData.SelectedLevelId)
        {
            _label.text = "LOCKED";
            _label.color = Color.gray;
            _button.interactable = false;
        }
        else
        {
            _label.text = "START GAME";
            _label.color = Color.white;
            _button.interactable = true;
        }
    }

    private void Awake()
    {
        _label = GetComponent<TextMeshProUGUI>();
        _button = GetComponent<Button>();
        _gameData = ProjectContext.Instance.GameData;
    }

    private void OnEnable()
    {
        _mainMenuInputHandler.RefreshLevelUiEvent += RefreshButtonState;
    }

    private void OnDisable()
    {
        _mainMenuInputHandler.RefreshLevelUiEvent -= RefreshButtonState;
    }
}
