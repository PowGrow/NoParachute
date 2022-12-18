using UnityEngine;

public class Mirrorer : MonoBehaviour, IWallTransformation
{
    [SerializeField] private bool _isActive;
    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    public void WallTransform(Wall wall)
    {
        if(IsActive)
        {
            var random = Random.Range(0, 100);
            if (random > 50)
                wall.Transform.localScale = new Vector2(-1, -1);
        }
    }
}
