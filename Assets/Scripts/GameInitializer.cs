using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Awake()
    {
        StartGame();
    }

    private void StartGame()
    {
        ProjectContext.Instance.Initialize(ProjectContext.Instance.LevelId, SceneType.Game);
        Destroy(gameObject);
    }
}
