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

    private const string LEVEL_ID_LOCKED = "?";
    private const string LEVEL_STATS_LOCKED = "??????";
    private void RefreshUiInformation()
    {
        var progressProvider = ProjectContext.Instance.SceneContext.ProgressProvider;
        var levelId = progressProvider.LevelId;
        if (GameData.UnlockedLevels >= levelId)
            UpdateStats(levelId, progressProvider);
        else
            SetPlugText();
    }

    private void UpdateStats(int levelId, IProgressProvider progressProvider)
    {
        _levelId.text = levelId.ToString();
        _levelName.text = progressProvider.LevelName;
        _highScore.text = GameData.LevelStats[levelId].HighScore.ToString();
        _bestTime.text = GetTimeStringFromFloat(GameData.LevelStats[levelId].BestTime);
        _deaths.text = GameData.LevelStats[levelId].Deaths.ToString();
        _limbsLost.text = GameData.LevelStats[levelId].LimbsLost.ToString();
    }

    private void SetPlugText()
    {
        _levelId.text = LEVEL_ID_LOCKED;
        _levelName.text = LEVEL_STATS_LOCKED;
        _highScore.text = LEVEL_STATS_LOCKED;
        _bestTime.text = LEVEL_STATS_LOCKED;
        _deaths.text = LEVEL_STATS_LOCKED;
        _limbsLost.text = LEVEL_STATS_LOCKED;
    }

    private string GetTimeStringFromFloat(float bestLevelTime)
    {
        TimeSpan time = TimeSpan.FromSeconds(bestLevelTime);
        return time.ToString("hh':'mm':'ss");
    }

    private void OnEnable()
    {
        RefreshUiInformation();
        _mainMenuInputHandler.RefreshLevelUiEvent += RefreshUiInformation;
    }

    private void OnDisable()
    {
        _mainMenuInputHandler.RefreshLevelUiEvent -= RefreshUiInformation;
    }
}
