using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private int _levelId;

    private void Start()
    {
        ProjectContext.Instance.Initialize(_levelId, SceneType.MainMenu);
        Destroy(gameObject);
    }
}
