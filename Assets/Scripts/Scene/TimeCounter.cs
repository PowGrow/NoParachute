using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public float Timer { get; private set; }

    private IProgressProvider _progressProvider;
    private bool IsGameActive = true;

    private void LevelCompletedEventHandler()
    {
        IsGameActive = false;
        _progressProvider.LevelCompletedEvent -= LevelCompletedEventHandler;
    }

    public void Initialize(IProgressProvider progressProvider)
    {
        _progressProvider = progressProvider;
        _progressProvider.LevelCompletedEvent += LevelCompletedEventHandler;
    }

    private void Update()
    {
        if(IsGameActive)
            Timer += Time.deltaTime;
    }
}
