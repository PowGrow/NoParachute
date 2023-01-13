using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TimeCounter _timeCounter;
    private TextMeshProUGUI _label;

    private void Awake()
    {
        _label = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _label.text = LevelsUI.GetTimeStringFromFloat(_timeCounter.Timer);
    }
}
