public interface IWallEventListener
{
    public void SubscribeOnWallEvents(WallEventHandler wallEventHandler);
    public void UnsubscribeFromWallEvents(WallEventHandler wallEventHandler);
}