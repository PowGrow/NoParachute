using UnityEngine;

public class WallRotation : MonoBehaviour,IWallTransformation
{
    public bool IsActive { get; set; } = false;
    public float DeltaStep { get; set; } = 0;
    public int Direction { get; set; } = 0;

    private float _delta;


    public void WallTransform(Wall wall)
    {
        if (IsActive)
        {
            _delta += DeltaStep;
            DoTransform(wall);
        }
    }

    private void DoTransform(Wall wall)
    {
        wall.Transform.rotation = Quaternion.Euler(0, 0, _delta);
    }
}
