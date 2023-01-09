using System.Linq;
using UnityEngine;

public class DataFiller : MonoBehaviour
{
    private IProgressProvider _progressProvider;
    public void FillData()
    {
        //GameData.UnlockedLevels++;
        //var levelStats = GameData.LevelStats.Last();
        //levelStats.HighScore = highScore;
        //levelStats.BestTime = bestTime;
        //levelStats.Deaths = deaths;
        //levelStats.LimbsLost = limbsLost;
        //GameData.LevelStats.Add(new LevelStats(0, 0, 0, 0));
        //SaveLoader.SaveData(ProjectContext.Instance.GameData);
    }
    private void Start()
    {
        _progressProvider = ProjectContext.Instance.SceneContext.ProgressProvider;
    }
    private void OnEnable()
    {
        _progressProvider.LevelCompletedEvent += FillData;
    }
    private void OnDisable()
    {
        _progressProvider.LevelCompletedEvent -= FillData;
    }
}
