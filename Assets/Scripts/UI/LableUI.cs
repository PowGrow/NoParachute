using TMPro;
using UnityEngine;

public class LableUI : MonoBehaviour
{
    [SerializeField] private Color LevelCompletedColor;
    [SerializeField] private Color DeadColor;
    [SerializeField] private CollisionDetectorForTors _torsCollision;
    private TextMeshProUGUI _lable;

    private const string LEVEL_COMPLETED = "LEVEL COMPLETE";
    private const string DEAD = "YOU DIED";
    private void Start()
    {
        _lable = this.GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        if (_torsCollision.IsAlive)
        {
            _lable.text = LEVEL_COMPLETED;
            _lable.color = LevelCompletedColor;
        }
        else
        {
            _lable.text = DEAD;
            _lable.color = DeadColor;
        }
    }
}
