using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedObject;

    private const string GAME = "Game";
    private const string MAIN_MENU = "MainMenu";

    private IProgressProvider _progressProvider;

    private void LevelCompletedEventHandler()
    {
        StartCoroutine(FinishingLevelUI(ProjectContext.Instance.LevelCompleteDelay,_levelCompletedObject));
    }

    private IEnumerator FinishingLevelUI(float delayOnFinishing, GameObject objectToShow)
    {
        yield return new WaitForSeconds(delayOnFinishing);
        objectToShow.SetActive(true);
    }

    public void Initialize(IProgressProvider progressProvider)
    {
        _progressProvider = progressProvider;
        _progressProvider.LevelCompletedEvent += LevelCompletedEventHandler;
    }

    public void OnNextLevelButtonClick()
    {
        ProjectContext.Instance.GameData.SelectedLevelId = ProjectContext.Instance.SceneContext.ProgressProvider.NextLevel.LevelId;
        SceneManager.LoadScene(GAME);
    }

    public void OnRestartLevelButtonClick()
    {
        SceneManager.LoadScene(GAME);
    }

    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene(MAIN_MENU);
    }

    private void OnDisable()
    {
        _progressProvider.LevelCompletedEvent -= LevelCompletedEventHandler;
    }
}
