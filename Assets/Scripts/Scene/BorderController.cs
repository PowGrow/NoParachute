using UnityEngine;

public class BorderController : MonoBehaviour
{
    private WallController _wallController;

    public void Initialize(WallController wallController)
    {
        _wallController = wallController;
        this.gameObject.SetActive(true);
        AnimationParent.Instance.InitializationCamera(transform);
    }

    public void RefreshBorderRotationAndPosition(Quaternion rotation, Vector3 position)
    {
        transform.rotation = rotation;
        transform.position = position;
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
