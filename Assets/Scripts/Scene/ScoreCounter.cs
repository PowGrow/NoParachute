using System.Collections;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private IProgressProvider _progressProvider;
    private GameData _gameData;
    private float _timer;
    private bool _isActive = true;

    public bool BestTime { get; private set; }
    public bool HighScore { get; private set; }
    public int Deaths {get; set;}
    public int LimbsLost { get; set; }
    public float Score { get; set; }
    public int ScoreMultiplier { get; set; } = 200;
    private const int LIMB_LOST_PENALITY = 500;
    private const int FAST_SPEED_MULTIPLIER = 3;
    private const int DEFAULT_SPEED_MULTIPLIER = 1;

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
                    {
                        levelStats.BestTime = _timer;
                        BestTime = true;
                    }
            }
            else
            {
                levelStats.BestTime = _timer;
                BestTime = true;
            }
            if (IntScore > levelStats.HighScore)
            {
                levelStats.HighScore = IntScore;
                HighScore = true;
            }
        }
        levelStats.Deaths += Deaths;
        levelStats.LimbsLost += LimbsLost;
    }
    private void LevelCompleteEventHandler()
    {
        StartCoroutine(LevelComplete((int)ProjectContext.Instance.LevelCompleteDelay - 1 ));
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
        _gameData.LevelStats.Add(new LevelStats(0, 0, 0, 0, new bool[3] { false, false, false }));
        SaveLoader.SaveData(_gameData);
    }

    public void LimbsLostPenalityApply()
    {
        Score -= LIMB_LOST_PENALITY;
        LimbsLost++;
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
            Score += ScoreMultiplier * SpeedMultiplierApply() * Time.deltaTime;
        }
    }

    private int SpeedMultiplierApply()
    {
        if (WallAnimator.CurrentSpeed == WallSpeed.Fast)
            return FAST_SPEED_MULTIPLIER;
        else
            return DEFAULT_SPEED_MULTIPLIER;
    }

    private void OnDisable()
    {
        _progressProvider.LevelCompletedEvent -= LevelCompleteEventHandler;
        CollisionDetectorForTors.PlayerDeath -= LevelFailedEventHandler;
    }
}
