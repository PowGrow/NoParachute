using System.Collections;
using TMPro;
using UnityEngine;

public class SpeedLabelUI : MonoBehaviour
{
    private TextMeshProUGUI _speedCounterLable;

    private int _currentSpeed;
    private int _targetSpeed;
    private const int BASE_SPEED = 230;
    private const int FAST_SPEED = 315;
    private const int SLOW_SPEED = 50;

    private const float DELAY = 0.1f;

    private void SpeedChangedEventHandler(WallSpeed speed)
    {
        if (speed == WallSpeed.Normal)
            _targetSpeed = BASE_SPEED;
        if(speed == WallSpeed.Fast)
            _targetSpeed = FAST_SPEED;
        if (speed == WallSpeed.Slow)
            _targetSpeed = SLOW_SPEED;
    }

    private IEnumerator MatchSpeed(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_currentSpeed < _targetSpeed)
            _currentSpeed++;
        if (_currentSpeed > _targetSpeed)
            _currentSpeed--;
        _speedCounterLable.text = _currentSpeed.ToString();
    }

    private void Awake()
    {
        _currentSpeed = BASE_SPEED;
        _targetSpeed = BASE_SPEED;
        _speedCounterLable = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        StartCoroutine(MatchSpeed(DELAY));
    }

    private void OnEnable()
    {
        WallAnimator.SpeedChangedEvent += SpeedChangedEventHandler;
    }

    private void OnDisable()
    {
        WallAnimator.SpeedChangedEvent -= SpeedChangedEventHandler;
    }
}
