using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsMenuUI : MonoBehaviour
{
    [SerializeField] private MainMenuInputHandler _mainMenuInputHandler;
    [SerializeField] private List<Image> _stars;
    [SerializeField] private Sprite _starOn;
    [SerializeField] private Sprite _starOff;

    private GameData _gameData;

    private void RefreshUiInformation()
    {
        if (_gameData.SelectedLevelId <= _gameData.UnlockedLevels)
        {
            for (int i = 0; i < _stars.Count; i++)
            {
                if (_gameData.LevelStats[_gameData.SelectedLevelId].Stars[i])
                    _stars[i].sprite = _starOn;
                else
                    _stars[i].sprite = _starOff;
            }
        }
    }

    private void OnEnable()
    {
        _gameData = ProjectContext.Instance.GameData;
        RefreshUiInformation();
        _mainMenuInputHandler.RefreshLevelUiEvent += RefreshUiInformation;
    }

    private void OnDisable()
    {
        _mainMenuInputHandler.RefreshLevelUiEvent -= RefreshUiInformation;
    }
}
