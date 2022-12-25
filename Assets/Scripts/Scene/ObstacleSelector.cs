using UnityEngine;

public class ObstacleSelector : MonoBehaviour
{
    private IObstacleProvider _obstacleProvider;
    private IProgressProvider _progressProvider;
    private int _obstacleToCreateIndex;

    private void RandomObstacleCreate(GameObject obstaclePrefab, Quaternion rotation)
    {
        var obstacle = Instantiate(obstaclePrefab, gameObject.transform);
        obstacle.transform.rotation = rotation;
        var parentWall = gameObject.transform.parent.transform;
        IncrementObstacleToCreateIndex();
    }
    private void IncrementObstacleToCreateIndex()
    {
        _progressProvider.PreviousObstacleDelta = 0;
        _progressProvider.ObstacleToCreateIndex++;
    }
    private bool IsObstacleCanBeCreated()
    {
        if (_obstacleProvider.ObstacleCreateDistance[_obstacleToCreateIndex] == _progressProvider.PreviousObstacleDelta)
            return true;
        else
            return false;
    }

    private void Awake()
    {
        if (ProjectContext.Instance.SceneContext.SceneType == SceneType.Game)
        {
            var sceneContext = ProjectContext.Instance.SceneContext;
            _obstacleProvider = sceneContext.ObstacleProvider;
            _progressProvider = sceneContext.ProgressProvider;
            _obstacleToCreateIndex = _progressProvider.ObstacleToCreateIndex;
            if (IsObstacleCanBeCreated())
            {
                var obstaclePrefab = _obstacleProvider.ObstacleToCreate[_obstacleToCreateIndex];
                var rotation = Quaternion.Euler(0, 0, _obstacleProvider.ObstacleCreateRotation[_obstacleToCreateIndex]);
                RandomObstacleCreate(obstaclePrefab, rotation);
            }
        }
    }
}
