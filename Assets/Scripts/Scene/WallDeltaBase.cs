using UnityEngine;

public abstract class WallDeltaBase : MonoBehaviour,IWallTransformation
{
    [SerializeField] private bool _isActive;
    [SerializeField] private float _deltaStep;
    [SerializeField] private float _deltaTime;

    protected float Delta;
    private float _deltaTimer;

    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    public void WallTransform(Wall wall)
    {
        if (IsActive)
        {
            var reverseSnakingTime = _deltaTime * 2;
            if (_deltaTimer < _deltaTime)
                Delta += _deltaStep;
            if (_deltaTimer > _deltaTime && _deltaTimer < reverseSnakingTime)
                Delta -= _deltaStep;
            if (_deltaTimer > reverseSnakingTime)
                _deltaTimer = 0;
            DoTransform(wall);
        }
    }

    protected abstract void DoTransform(Wall wall);

    private void FixedUpdate()
    {
        if (IsActive)
            _deltaTimer += Time.deltaTime;
    }
}
