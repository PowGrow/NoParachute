using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private GameInputController _gameInputController;

    public event Action<WallSpeed> SpeedChangeEvent;

    private void ChangeFallSpeed(WallSpeed speed)
    {
        WallAnimator.CurrentSpeed = speed;
        SpeedChangeEvent?.Invoke(speed);
    }
    private void Awake()
    {
        _gameInputController = new GameInputController();
    }

    private void OnEnable()
    {
        _gameInputController.Game.SpeedUp.performed += callbackContext => ChangeFallSpeed(WallSpeed.Fast);
        _gameInputController.Game.SpeedUp.canceled += callbackContext => ChangeFallSpeed(WallSpeed.Normal);
        _gameInputController.Enable();
    }

    private void OnDisable()
    {
        _gameInputController.Disable();
    }
}
