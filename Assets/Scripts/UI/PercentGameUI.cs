using TMPro;
using UnityEngine;

public class PercentGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _percentUI;
    private TextMeshProUGUI _label;
    private void Awake()
    {
        _label = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _label.text = _percentUI.text;
    }
}
