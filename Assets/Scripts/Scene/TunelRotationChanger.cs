using UnityEngine;

public class TunelRotationChanger : MonoBehaviour
{
    [SerializeField] private float _rotationStep;
    [SerializeField] private int _direction;
    [SerializeField] private bool _doActivation;
    private WallRotation _wallRotation;

    private void Awake()
    {
        _wallRotation = ProjectContext.Instance.SceneContext.WallController.gameObject.GetComponent<WallRotation>();
    }

    private void OnEnable()
    {
        _wallRotation.DeltaStep= _rotationStep;
        _wallRotation.Direction = _direction;
        _wallRotation.IsActive = _doActivation;
    }
}
