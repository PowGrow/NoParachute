using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private MovementController _movementController;
    private Rigidbody2D rb;
    [Header("Range of Movement")] 
    [SerializeField] private float xMaxBorder=0.5f;
    [SerializeField] private float xMinBorder=-0.6f;
    [SerializeField] private float yMaxBorder=0.5f;
    [SerializeField] private float yMinBorder=-0.5f;
    public event Action<WallSpeed> SpeedChangeEvent;

    private void ChangeFallSpeed(WallSpeed speed)
    {
        WallAnimator.CurrentSpeed = speed;
        SpeedChangeEvent?.Invoke(speed);
    }

    private void Move()
    {
        Vector2 direction = _movementController.Player.Movement.ReadValue<Vector2>();
        rb.AddForce(direction*5f);
        if (rb.transform.position.x > xMaxBorder)
        {
            rb.Sleep();
            rb.AddForce(new Vector2(-0.1f, 0f));
            rb.AddForce(new Vector2(-10f, 0f));

        }
        if (rb.transform.position.x < xMinBorder)
        {
            rb.Sleep();
            rb.AddForce(new Vector2(10f, 0f));
        }
        if (rb.transform.position.y > yMaxBorder)
        {
            rb.Sleep();
            rb.AddForce(new Vector2(0, -10f));
        }
        if (rb.transform.position.y < yMinBorder)
        {
            rb.Sleep();
            rb.AddForce(new Vector2(0, 10f));
        }

    }

    void Update()
    {
        Move();
    }

    private void Awake()
    {
        _movementController = new MovementController();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        
        _movementController.Player.SpeedUp.performed += callbackContext => ChangeFallSpeed(WallSpeed.Fast);
        _movementController.Player.SpeedUp.canceled += callbackContext => ChangeFallSpeed(WallSpeed.Normal);
        _movementController.Enable();
    }

    private void OnDisable()
    {
        _movementController.Disable();
    }
}
