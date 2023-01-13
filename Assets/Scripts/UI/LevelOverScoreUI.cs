using TMPro;
using UnityEngine;

public class LevelOverScoreUI : MonoBehaviour
{
    [SerializeField] ScoreCounter _scoreCounter;
    private TextMeshProUGUI _label;

    private void Awake()
    {
        _label = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        int scoreInt = (int)_scoreCounter.Score;
        _label.text = scoreInt.ToString();
    }
}
