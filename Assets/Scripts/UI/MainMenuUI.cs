using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void LoadLevel(int levelId)
    {
        ProjectContext.Instance.LevelId = levelId;
        SceneManager.LoadScene(1);
    }
}
