using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    private IProgressProvider _progressProvider;
    private float _rotationStep;
    private RotationMode _rotationMode = RotationMode.None;
    private Transform _transform;
    private int _direction = 1;

    private const float SINUSUID_ROTATION_ANGLE = 180;

    public void Initiazlie(IProgressProvider progressProvider)
    {
        _progressProvider = progressProvider;
        _rotationStep = _progressProvider.RotationStep;
        _rotationMode = _progressProvider.RotationMode;
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
                if (_transform.rotation.z > 90 || _transform.rotation.z < 0)
                    _direction *= -1;
                _transform.Rotate(0,0,_direction * _rotationStep);
            }
            else if (_rotationMode == RotationMode.Constant)
            {
                _transform.Rotate(0, 0, _rotationStep);
            }
        }
    }
}
