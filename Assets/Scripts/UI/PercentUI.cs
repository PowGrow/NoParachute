using System;
using TMPro;
using UnityEngine;

public class PercentUI : MonoBehaviour
{
    private IProgressProvider _progressProvider;
    private TextMeshProUGUI _label;
    
    public void Initialize(IProgressProvider progressProvider)
    {
        _progressProvider = progressProvider;
    }

    private void Awake()
    {
        _label = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        if (_progressProvider != null)
        {
            var percent = (Convert.ToInt32(_progressProvider.WallsPassed / _progressProvider.OnePercentOfCompleting));
            percent = Math.Clamp(percent, 0, 100);
            _label.text = $"{percent}%";
        }
    }
}
