
using UnityEngine;

public class WallDeltaPosition : WallDeltaBase, IWallTransformation
{
    protected override void DoTransform(Wall wall)
    {
        wall.transform.localPosition = new Vector3(Delta, wall.transform.position.y, wall.transform.position.z);
    }
}
