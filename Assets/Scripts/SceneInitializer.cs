using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private int _levelId;

    private void Start()
    {
        ProjectContext.Instance.LoadLevelData(_levelId, SceneType.MainMenu);
        Destroy(gameObject);
    }
}
