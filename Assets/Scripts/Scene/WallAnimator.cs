
using System;
using System.Collections.Generic;
using UnityEngine;

public class WallAnimator : MonoBehaviour
{
    private WallController _wallController;
    private Movement _playerMovement;

    private const string PLAYER_OBJECT_NAME = "Character";

    public static event Action<WallSpeed> SpeedChangedEvent;
    public static WallSpeed CurrentSpeed { get; set; }

    public void Initialize(WallController walLController)
    {
        CurrentSpeed = WallSpeed.Normal;
        _wallController = walLController;
        gameObject.SetActive(true);
    }

    public void SetWallSpeed(WallSpeed speed)
    {
        if (speed == WallSpeed.Normal || speed == WallSpeed.Fast)
            SpeedChangeEventHandler(speed);
        else if (speed == WallSpeed.Slow)
        {
            foreach (Animator animator in _wallController.WallAnimators)
            {
                animator.speed = 0.2f;
            }
            SpeedChangedEvent?.Invoke(speed);
        }
    }

    private void SpeedChangeEventHandler(WallSpeed speed)
    {
        foreach (Animator animator in _wallController.WallAnimators)
        {
            animator.speed = (float)speed;
        }
        SpeedChangedEvent?.Invoke(speed);
    }


    private void OnEnable()
    {
        try
        {
            _playerMovement = ProjectContext.Instance.SceneContext.ObjectProvider.GetObject(PLAYER_OBJECT_NAME).GetComponent<Movement>();
        }
        catch(KeyNotFoundException) {}
        if(_playerMovement != null)
            _playerMovement.SpeedChangeEvent += SpeedChangeEventHandler;
    }

    private void OnDisable()
    {
        if(_playerMovement != null)
            _playerMovement.SpeedChangeEvent -= SpeedChangeEventHandler;
    }
}
