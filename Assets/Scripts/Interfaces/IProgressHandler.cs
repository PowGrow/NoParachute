public interface IProgressHandler
{
    public int PreviousObstacleDelta { get; set; }
    public void OnProgress();
}
