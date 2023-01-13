using UnityEngine;

public class TunnelShapeChanger : MonoBehaviour
{
    private static float _deltaX = 0;
    private static float _deltaY = 0;
    private static int _steps = 0;
    private static Vector2 _targetShape;
    private IProgressProvider _progressProvider;
    private IObstacleProvider _obstacleProvider;

    public static Vector2 GetDeltaShape(Vector2 currentWallPosition)
    {
        _deltaX = (currentWallPosition.x + _targetShape.x) / _steps;
        _deltaY = (currentWallPosition.y + _targetShape.y) / _steps;
         return new Vector2(_deltaX, _deltaY);
    }
    private void Start()
    {
        _progressProvider = ProjectContext.Instance.SceneContext.ProgressProvider;
        _obstacleProvider = ProjectContext.Instance.SceneContext.ObstacleProvider;
        _steps = _obstacleProvider.ObstacleCreateDistance[_progressProvider.ObstacleToCreateIndex - 1];
        _targetShape = _progressProvider.TunelShape[_progressProvider.TunelShapeId];
        _progressProvider.DoChangeTunelShape = true;
    }
}
