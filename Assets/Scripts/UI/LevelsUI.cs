using System;
using TMPro;
using UnityEngine;

public class LevelsUI : MonoBehaviour
{
    [SerializeField] private MainMenuInputHandler _mainMenuInputHandler;
    [SerializeField] private TextMeshProUGUI _levelId;
    [SerializeField] private TextMeshProUGUI _levelName;
    [SerializeField] private TextMeshProUGUI _highScore;
    [SerializeField] private TextMeshProUGUI _bestTime;
    [SerializeField] private TextMeshProUGUI _deaths;
    [SerializeField] private TextMeshProUGUI _limbsLost;

    private GameData _gameData;

    private const string LEVEL_ID_LOCKED = "?";
    private const string LEVEL_NAME_LOCKED = "??????";
    private const string LEVEL_STATS_ZERO = "-";
    private void RefreshUiInformation()
    {
        var progressProvider = ProjectContext.Instance.SceneContext.ProgressProvider;
        var levelId = progressProvider.LevelId;
        if (_gameData.UnlockedLevels >= levelId)
            UpdateStats(levelId, progressProvider);
        else
            SetPlugText();
    }

    private void UpdateStats(int levelId, IProgressProvider progressProvider)
    {
        _levelId.text = levelId.ToString();
        _levelName.text = progressProvider.LevelName;
        if (_gameData.LevelStats[levelId].HighScore == 0)
            _highScore.text = LEVEL_STATS_ZERO;
        else
            _highScore.text = _gameData.LevelStats[levelId].HighScore.ToString();
        if (_gameData.LevelStats[levelId].BestTime == 0)
            _bestTime.text = LEVEL_STATS_ZERO;
        else
            _bestTime.text = GetTimeStringFromFloat(_gameData.LevelStats[levelId].BestTime);
        if (_gameData.LevelStats[levelId].Deaths == 0)
            _deaths.text = LEVEL_STATS_ZERO;
        else
            _deaths.text = _gameData.LevelStats[levelId].Deaths.ToString();
        if (_gameData.LevelStats[levelId].LimbsLost == 0)
            _limbsLost.text = LEVEL_STATS_ZERO;
        else
            _limbsLost.text = _gameData.LevelStats[levelId].LimbsLost.ToString();
    }

    private void SetPlugText()
    {
        _levelId.text = LEVEL_ID_LOCKED;
        _levelName.text = LEVEL_NAME_LOCKED;
        _highScore.text = LEVEL_STATS_ZERO;
        _bestTime.text = LEVEL_STATS_ZERO;
        _deaths.text = LEVEL_STATS_ZERO;
        _limbsLost.text = LEVEL_STATS_ZERO;
    }

    private string GetTimeStringFromFloat(float bestLevelTime)
    {
        TimeSpan time = TimeSpan.FromSeconds(bestLevelTime);
        return time.ToString("hh':'mm':'ss");
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
