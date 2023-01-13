using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    private IProgressProvider _progressProvider;
    private float _rotationStep;
    private RotationMode _rotationMode = RotationMode.None;
    private Transform _transform;
    private int _direction = 1;

    private const float SINUSOID_ROTATION_ANGLE_UPPER = 90;
    private const float SINUSOID_ROTATION_ANGLE_LOWER = 0;

    public void Initiazlie(IProgressProvider progressProvider)
    {
        _progressProvider = progressProvider;
        _rotationStep = _progressProvider.RotationStep;
        _rotationMode = _progressProvider.RotationMode;
        _progressProvider.LevelCompletedEvent += PlayerDeathEventHandler;
    }

    private void PlayerDeathEventHandler()
    {
        _rotationMode = RotationMode.None;
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (_rotationMode != RotationMode.None)
        {
            if (_rotationMode == RotationMode.Sinusoid)
            {
                if (_transform.rotation.z > SINUSOID_ROTATION_ANGLE_UPPER || _transform.rotation.z < SINUSOID_ROTATION_ANGLE_LOWER)
                    _direction *= -1;
                _transform.Rotate(0,0,_direction * _rotationStep);
            }
            else if (_rotationMode == RotationMode.Constant)
            {
                _transform.Rotate(0, 0, _rotationStep);
            }
        }
    }

    private void OnEnable()
    {
        CollisionDetectorForTors.PlayerDeath += PlayerDeathEventHandler;
    }

    private void OnDisable()
    {
        CollisionDetectorForTors.PlayerDeath -= PlayerDeathEventHandler;
        _progressProvider.LevelCompletedEvent -= PlayerDeathEventHandler;
    }
}
