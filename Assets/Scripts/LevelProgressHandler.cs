using UnityEngine;

public class LevelProgressHandler : MonoBehaviour, IProgressHandler
{
    private int _levelProgress;
    private int _levelLength;
    public int LevelProgress
    {
        get { return _levelProgress; }
        set { _levelProgress = value; }
    }
    public LevelProgressHandler(Level currentLevel)
    {
        _levelLength = currentLevel.LevelLength;
    }

    public void OnProgress()
    {
        _levelProgress++;
        if (_levelProgress >= _levelLength)
            Debug.Log("Finish!");
    }
}
