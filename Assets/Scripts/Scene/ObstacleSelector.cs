using System.Linq;
using UnityEngine;

public class ObstacleSelector : MonoBehaviour
{
    private IObstacleProvider _obstacleProvider;
    private IProgressHandler _progressHandler;
    private int _obstacleToCreateIndex;

    private void RandomObstacleCreate(GameObject obstaclePrefab, Quaternion rotation)
    {
        var obstacle = Instantiate(obstaclePrefab, gameObject.transform);
        obstacle.transform.rotation = rotation;
        Debug.Log($"Obstacle {obstacle.name} created");
        var parentWall = gameObject.transform.parent.transform;
        SwitchCreationIndexToNext();
    }
    private void SwitchCreationIndexToNext()
    {
        _progressHandler.PreviousObstacleDelta = 0;
        _progressHandler.ObstacleToCreateIndex++;
    }
    private bool IsObstacleCanBeCreated()
    {
        if (_obstacleProvider.ObstacleCreateDistance[_obstacleToCreateIndex] == ProjectContext.Instance.GameContext.ProgressHandler.PreviousObstacleDelta)
            return true;
        else
            return false;
    }

    private void Awake()
    {
        _obstacleProvider = ProjectContext.Instance.GameContext.ObstacleProvider;
        _progressHandler = ProjectContext.Instance.GameContext.ProgressHandler;
        _obstacleToCreateIndex = _progressHandler.ObstacleToCreateIndex;
        if (IsObstacleCanBeCreated())
        {
            var obstaclePrefab = _obstacleProvider.ObstacleToCreate[_obstacleToCreateIndex];
            var rotation = Quaternion.Euler(0, 0, _obstacleProvider.ObstacleCreateRotation[_obstacleToCreateIndex]);
            RandomObstacleCreate(obstaclePrefab,rotation);
        }
    }
}
