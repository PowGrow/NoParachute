using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    private TextMeshProUGUI _scoreCounterLabel;

    private void Awake()
    {
        _scoreCounterLabel = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        _scoreCounterLabel.text = _scoreCounter.Score.ToString();
    }
}
