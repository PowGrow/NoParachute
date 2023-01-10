using System.Collections;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private IProgressProvider _progressProvider;
    private GameData _gameData;
    private float _timer;
    private bool _isActive = true;

    public int Deaths {get; set;}
    public int LimbsLost { get; set; }
    public float Score { get; set; }
    public int ScoreMultiplier { get; set; } = 2;

    public void Initialize(IProgressProvider progressProvider)
    {
        _progressProvider = progressProvider;
        _progressProvider.LevelCompletedEvent += LevelCompleteEventHandler;
        CollisionDetectorForTors.PlayerDeath += LevelFailedEventHandler;
    }
    private void SaveLevelStats()
    {
        if(_gameData.SelectedLevelId == _gameData.UnlockedLevels) _gameData.UnlockedLevels++;
        FillLevelStats(true);
    }
    private void FillLevelStats(bool isLevelCompleted)
    {
        var levelStats = _gameData.LevelStats[_gameData.SelectedLevelId];
        var IntScore = (int)Score;

        if(isLevelCompleted)
        {
            if (levelStats.BestTime != 0)
            {
                if (_timer < levelStats.BestTime)
                    levelStats.BestTime = _timer;
            }
            else
                levelStats.BestTime = _timer;
            if (IntScore > levelStats.HighScore)
                levelStats.HighScore = IntScore;
        }
        levelStats.Deaths += Deaths;
        levelStats.LimbsLost += LimbsLost;
    }
    private void LevelCompleteEventHandler()
    {
        StartCoroutine(LevelComplete((int)ProjectContext.Instance.LevelCompleteDelay));
    }
    private void LevelFailedEventHandler()
    {
        _isActive = false;
        FillLevelStats(false);
        SaveLoader.SaveData(_gameData);
    }

    private IEnumerator LevelComplete(int delay)
    {
        yield return new WaitForSeconds(delay);
        _isActive = false;
        SaveLevelStats();
        _gameData.LevelStats.Add(new LevelStats(0, 0, 0, 0));
        SaveLoader.SaveData(_gameData);
    }
    private void Start()
    {
        _gameData = ProjectContext.Instance.GameData;
        _progressProvider = ProjectContext.Instance.SceneContext.ProgressProvider;
        _timer = 0;
    }
    private void FixedUpdate()
    {
        if(_isActive)
        {
            _timer += Time.deltaTime;
            Score += ScoreMultiplier * Time.deltaTime;
        }
    }

    private void OnDisable()
    {
        _progressProvider.LevelCompletedEvent -= LevelCompleteEventHandler;
        CollisionDetectorForTors.PlayerDeath -= LevelFailedEventHandler;
    }
}
