using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUI : MonoBehaviour
{
    [SerializeField] private List<Image> _stars;
    [SerializeField] private Sprite _starOn;
    [SerializeField] private Sprite _starOff;
    [SerializeField] private ScoreCounter _scoreCounter;

    private IProgressProvider _progressProvider;

    public void Initialize(IProgressProvider progressProvider)
    {
        _progressProvider = progressProvider;
    }

    private void OnEnable()
    {
        for(int i = 0; i < _stars.Count; i++) 
        {
            if (_scoreCounter.Score >= _progressProvider.Stars[i])
            {
                _stars[i].sprite = _starOn;
                ProjectContext.Instance.GameData.LevelStats[ProjectContext.Instance.GameData.SelectedLevelId].Stars[i] = true;
            }
            else
            {
                _stars[i].sprite = _starOff;
                ProjectContext.Instance.GameData.LevelStats[ProjectContext.Instance.GameData.SelectedLevelId].Stars[i] = false;
            }
        }
    }
}
