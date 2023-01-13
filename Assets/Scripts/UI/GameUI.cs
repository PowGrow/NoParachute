using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedObject;
    [SerializeField] private GameObject _gameUiObject;
    [SerializeField] private TextMeshProUGUI _levelCompleteLabel;
    [SerializeField] private GameObject _nextLevelButton;
    [SerializeField] private GameObject _timeLabel;
    [SerializeField] private GameObject _percentLabel;
    [SerializeField] private GameObject _starsObject;
    [SerializeField] private GameObject _bestTimeObject;
    [SerializeField] private GameObject _highScore;
    [SerializeField] private Color LevelCompleteColor;
    [SerializeField] private Color DeathColor;
    [SerializeField] private ScoreCounter _scoreCounter;

    private const string GAME = "Game";
    private const string MAIN_MENU = "MainMenu";
    private const string LEVEL_COMPLETE = "LEVEL COMPLETE";
    private const string YOU_DEAD = "YOU DEAD";

    private IProgressProvider _progressProvider;

    private void LevelCompletedEventHandler()
    {
        StartCoroutine(FinishingLevelUI(true, ProjectContext.Instance.LevelCompleteDelay,_levelCompletedObject,_gameUiObject));
        _levelCompleteLabel.text = LEVEL_COMPLETE;
        _levelCompleteLabel.color = LevelCompleteColor;
        _nextLevelButton.SetActive(true);
        _starsObject.SetActive(true);
    }

    private void PlayerDeathEventHandler()
    {
        StartCoroutine(FinishingLevelUI(false, 0, _levelCompletedObject, _gameUiObject));
        _levelCompleteLabel.text = YOU_DEAD;
        _levelCompleteLabel.color = DeathColor;
        _nextLevelButton.SetActive(false);
        _timeLabel.SetActive(false);
        _percentLabel.SetActive(true);
    }

    private IEnumerator FinishingLevelUI(bool isFinished, float delayOnFinishing, GameObject objectToShow, GameObject objectToHide)
    {
        yield return new WaitForSeconds(delayOnFinishing);
        if(isFinished)
        {
            if (_scoreCounter.BestTime)
                _bestTimeObject.SetActive(true);
            if (_scoreCounter.HighScore)
                _highScore.SetActive(true);
        }
        objectToShow.SetActive(true);
        objectToHide.SetActive(false);
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
