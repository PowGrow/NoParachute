using UnityEngine;

public class BorderController : MonoBehaviour
{
    [SerializeField] private WallController _wallController;
    public void RefreshBorderRotationAndPosition(Quaternion rotation, Vector3 position)
    {
        this.transform.rotation = rotation;
        this.transform.position = position;
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
        _wallController.OnWallCreated += SubscribeOnWallEvents;
    }

    private void OnDisable()
    {
        _wallController.OnWallDestoryed -= UnsubscribeFromWallEvents;
    }
}
