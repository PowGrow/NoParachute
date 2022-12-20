using System.Linq;
using UnityEngine;

public class ObstacleSelector : MonoBehaviour
{
    [SerializeField] private int _chanceToCreateObstacle;
    [SerializeField] private int _obstacleCreationDelay;
    private IObstacleProvider _obstacleProvider;

    private void RandomObstacleCreate()
    {
        var randomValue = Random.Range(0, _obstacleProvider.Obstacles.Count());
        var obstacle = Instantiate(_obstacleProvider.Obstacles[randomValue],gameObject.transform);
        ProjectContext.Instance.GameContext.ProgressHandler.PreviousObstacleDelta = 0;
    }
    private bool IsObstacleCanBeCreated()
    {
        if (_obstacleCreationDelay < ProjectContext.Instance.GameContext.ProgressHandler.PreviousObstacleDelta)
            return true;
        else
            return false;
    }

    private void Awake()
    {
        _obstacleProvider = ProjectContext.Instance.GameContext.ObstacleProvider;
        if(IsObstacleCanBeCreated())
        {
            var randomValue = Random.Range(1, 101);
            if(randomValue <= _chanceToCreateObstacle)
                RandomObstacleCreate();
        }
    }
}
