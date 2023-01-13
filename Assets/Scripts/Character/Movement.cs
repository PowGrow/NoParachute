using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private static bool IsActive = true;
    private MovementController _movementController;
    private Rigidbody2D rb;
    [SerializeField] private Animator _animator;
    private float speed = 3.5f;

    public static Movement Instance;
    public event Action<WallSpeed> SpeedChangeEvent;

    private void ChangeFallSpeed(WallSpeed speed)
    {
        WallAnimator.CurrentSpeed = speed;
        SpeedChangeEvent?.Invoke(speed);
    }

    public static void DisableControlls()
    {
        IsActive = false;
    }

    public void DecreseSpeed()
    {
        speed = speed - 0.55f;
    }

    private void Move()
    {
        if(IsActive)
        {
            Vector2 direction = _movementController.Player.Movement.ReadValue<Vector2>();
            Animation(direction);
            rb.AddRelativeForce(direction*speed);
        }
    }

    private void Animation(Vector2 direction)
    {
        _animator.SetFloat("Y",direction.y);
        _animator.SetFloat("X",direction.x);
    }

    void Update()
    {
        Move();
    }

    private void Awake()
    {
        Instance = this;
        _movementController = new MovementController();
        rb = GetComponent<Rigidbody2D>();
        CollisionDetectorForTors.PlayerDeath += Disable;
    }

    private void Disable()
    {
        _movementController.Disable();
    }

    private void OnEnable()
    {
        _movementController.Player.SpeedUp.performed += callbackContext => ChangeFallSpeed(WallSpeed.Fast);
        _movementController.Player.SpeedUp.canceled += callbackContext => ChangeFallSpeed(WallSpeed.Normal);
        _movementController.Enable();
    }

    private void OnDisable()
    {
        Disable();
        CollisionDetectorForTors.PlayerDeath -= Disable;
    }
}
