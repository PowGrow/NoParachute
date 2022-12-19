using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private WallEventHandler _eventHandler;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _decorativesSpriteRenderer;
    [SerializeField] private Transform _transform;

    public WallEventHandler EventHandler
    {
        get { return _eventHandler; }
    }

    public SpriteRenderer SpriteRenderer
    {
        get { return _spriteRenderer; }
    }

    public SpriteRenderer DecorativesSpriteRenderer
    {
        get { return _decorativesSpriteRenderer; }
    }

    public Transform Transform
    {
        get { return _transform;}
    }
}
