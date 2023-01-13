using System.Linq;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    private WallController _wallController;
    private GameObject _normalColliders;
    private GameObject _wideColliders;
    private bool _isStateChanged = false;
    private bool _isNormalWalls = true;
    private int _delay;

    private const int DELAY = 80;
    public bool IsNormalWalls 
    { 
        get
        {
            return _isNormalWalls;
        }
        set
        {
            _isStateChanged = true;
            SetColliderChangeDelay();
            _isNormalWalls = value;
        }
    }

    private void SetColliderChangeDelay()
    {
        var obstacleProvider = ProjectContext.Instance.SceneContext.ObstacleProvider;
        var progressProvider = ProjectContext.Instance.SceneContext.ProgressProvider;
        _delay = DELAY;
    }

    public void Initialize(WallController wallController)
    {
        _wallController = wallController;
        var borderData = gameObject.GetComponent<BorderDataHolder>();
        _normalColliders = borderData.NormalWalls;
        _wideColliders = borderData.WideWalls;
        this.gameObject.SetActive(true);
        AnimationParent.Instance.InitializationCamera(transform);
    }

    public void RefreshBorderRotationAndPosition(Quaternion rotation, Vector3 position)
    {
        transform.rotation = rotation;
        transform.position = position;
        if(_isStateChanged)
        {
            if (_delay <= 0)
            {
                if (IsNormalWalls)
                    _normalColliders.SetActive(true);
                else
                    _normalColliders.SetActive(false);
                _isStateChanged = false;
            }
            else
            {
                _delay--;
                Debug.Log(_delay);
            }
                
        }
    }

    private void SubscribeOnWallEvents(WallEventHandler wallEventHandler)
    {
        wallEventHandler.RefreshBorderEvent += RefreshBorderRotationAndPosition;
    }

    private void UnsubscribeFromWallEvents(WallEventHandler wallEventHandler)
    {
        wallEventHandler.RefreshBorderEvent -= RefreshBorderRotationAndPosition;
    }

    private void OnEnable()
    {
        _wallController.WallCreatedEvent += SubscribeOnWallEvents;
    }

    private void OnDisable()
    {
        _wallController.WallCreatedEvent += UnsubscribeFromWallEvents;
    }
}
