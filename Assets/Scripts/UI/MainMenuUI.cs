using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void LoadLevel(int levelId)
    {
        ProjectContext.Instance.Initialize(levelId);
        SceneManager.LoadScene(1);
    }
}
