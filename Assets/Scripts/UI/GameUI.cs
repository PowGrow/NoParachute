using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedObject;
    [SerializeField] private TextMeshProUGUI _levelCompleteLabel;
    [SerializeField] private GameObject _nextLevelButton;
    [SerializeField] private Color LevelCompleteColor;
    [SerializeField] private Color DeathColor;

    private const string GAME = "Game";
    private const string MAIN_MENU = "MainMenu";
    private const string LEVEL_COMPLETE = "LEVEL COMPLETE";
    private const string YOU_DEAD = "YOU DEAD";

    private IProgressProvider _progressProvider;

    private void LevelCompletedEventHandler()
    {
        StartCoroutine(FinishingLevelUI(ProjectContext.Instance.LevelCompleteDelay,_levelCompletedObject));
        _levelCompleteLabel.text = LEVEL_COMPLETE;
        _levelCompleteLabel.color = LevelCompleteColor;
        _nextLevelButton.SetActive(true);
    }

    private void PlayerDeathEventHandler()
    {
        StartCoroutine(FinishingLevelUI(0, _levelCompletedObject));
        _levelCompleteLabel.text = YOU_DEAD;
        _levelCompleteLabel.color = DeathColor;
        _nextLevelButton.SetActive(false);
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

    private void OnEnable()
    {
        CollisionDetectorForTors.PlayerDeath += PlayerDeathEventHandler;
    }

    private void OnDisable()
    {
        _progressProvider.LevelCompletedEvent -= LevelCompletedEventHandler;
        CollisionDetectorForTors.PlayerDeath -= PlayerDeathEventHandler;
    }
}
