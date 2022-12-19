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
            var random = Random.Range(1, 101);
            if (random > 50)
                wall.Transform.localScale = new Vector2(-1, -1);
            random = Random.Range(1, 101);
            if (random > 50)
                wall.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
