
public interface IWallTransformation
{
    public bool IsActive { get; set; }
    public void WallTransform(Wall wall);
}
