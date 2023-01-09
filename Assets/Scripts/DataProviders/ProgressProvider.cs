using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProgressProvider : IProgressProvider
{
    private int _previousObstacleDelta;
    private int _startObstacleDelay;
    private int _obstacleToCreateIndex;
    private bool _isLevelRunning = true;

    private const int UNREACHABLE_VALUE = int.MaxValue;
    private const int FIRST_INDEX = 0;

    public event Action LevelCompletedEvent;

    public int LevelId { get; private set; }
    public string LevelName { get; private set; }
    public LevelData NextLevel { get; private set; }
    public LevelData PreviousLevel { get; private set; }

    public int PreviousObstacleDelta
    {
        get { return _previousObstacleDelta; }
        set { _previousObstacleDelta = value; }
    }
    public int ObstacleToCreateIndex
    {
        get { return _obstacleToCreateIndex; }
        set { _obstacleToCreateIndex = value; }
    }
    public float RotationStep { get; private set; }

    public RotationMode RotationMode { get; private set; }

    public List<Vector3> TunelShape { get; private set; }

    public bool DoChangeTunelShape { get; set; } = false;

    public int TunelShapeId { get; set; } = 0;

    public ProgressProvider(LevelData currentLevel)
    {
        LevelId = currentLevel.LevelId;
        LevelName = currentLevel.LevelName;
        NextLevel = currentLevel.NextLevel;
        PreviousLevel = currentLevel.PreviousLevel;
        _startObstacleDelay = currentLevel.StartObstacleDelay;
        RotationStep = currentLevel.RotationSpeed;
        RotationMode = currentLevel.RotationMode;
        TunelShape = currentLevel.TunnelShape;
    }

    private bool IsGameScene()
    {
        return ProjectContext.Instance.SceneContext.SceneType == SceneType.Game;
    }

    private bool AreObstaclesEnded()
    {
        return ObstacleToCreateIndex >= ProjectContext.Instance.SceneContext.ObstacleProvider.ObstacleToCreate.Count();
    }

    public void CreateLevelTransition()
    {
        if (IsGameScene())
        {
            PreviousObstacleDelta = UNREACHABLE_VALUE;
            ObstacleToCreateIndex = FIRST_INDEX;
            ProjectContext.Instance.SceneContext.SwitchLevelData(NextLevel);
        }
    }

    public void OnProgress()
    {
        if(IsGameScene() && _isLevelRunning)
        {
            if (IsDelayed())
                _startObstacleDelay--;                
            else
                _previousObstacleDelta++;
            if (AreObstaclesEnded() && _isLevelRunning)
            {
                _startObstacleDelay = UNREACHABLE_VALUE;
                _isLevelRunning = false;
                LevelCompletedEvent?.Invoke();
                CreateLevelTransition();
            }
        }
    }

    private bool IsDelayed()
    {
        return _startObstacleDelay > 0;
    }

    private void WallCreatedEventHandler(WallEventHandler wallEventHandler)
    {
        OnProgress();
    }

    public void SubscribingOnWallCreatingEvents(WallController wallController)
    {
        wallController.WallCreatedEvent += WallCreatedEventHandler;
    }

    public void UnsubscribingFromWallCreatingEvents(WallController wallController)
    {
        wallController.WallCreatedEvent -= WallCreatedEventHandler;
    }

}
