using System;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressProvider
{
    public event Action LevelCompletedEvent;

    public int LevelId { get;}
    public string LevelName { get;}
    public LevelData NextLevel { get; }
    public LevelData PreviousLevel { get; }
    public int WallsPassed { get; }
    public int OnePercentOfCompleting { get;}
    public int PreviousObstacleDelta { get; set; }
    public int ObstacleToCreateIndex { get; set; }
    public List<int> Stars { get;}
    public float RotationStep { get;}
    public RotationMode RotationMode { get;}

    public List<Vector3> TunelShape { get; }

    public bool DoChangeTunelShape { get; set; }

    public int TunelShapeId { get; set; }

    public void OnProgress();

    public void SubscribingOnWallCreatingEvents(WallController wallController);

    public void UnsubscribingFromWallCreatingEvents(WallController wallController);

    public void SubscribingOnWallPassesEvent(WallEventHandler eventHandler);

    public void UnsubscribingOnWallPassesEvent(WallEventHandler eventHandler);
}
