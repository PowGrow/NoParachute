using UnityEngine;

public class WallDeltaRotation : WallDeltaBase, IWallTransformation
{
    protected override void DoTransform(Wall wall)
    {
        wall.Transform.rotation = Quaternion.Euler(0, 0, Delta);
    }
}
