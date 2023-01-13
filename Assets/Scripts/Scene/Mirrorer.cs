using UnityEngine;

public class Mirrorer : MonoBehaviour, IWallTransformation
{
    [SerializeField] private bool _isActive;

    public int Direction { get; set; } = 1;
    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    public void WallTransform(Wall wall)
    {
        if(IsActive)
        {
            var random = Random.Range(1, 101);
            if (random > 50)
                wall.Transform.localScale = new Vector2(-1 * Direction, -1 * Direction);
            random = Random.Range(1, 101);
            if (random > 50)
                wall.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
