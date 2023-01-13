using UnityEngine;

public class WallSwitcher : MonoBehaviour
{
    [SerializeField] private string _targetLevel;
    [SerializeField] private bool IsNormalWalls;
    private ISpriteProvider _spriteProvider;
    private IObjectProvider _objectProvider;

    private const string TECH_DATA_HOLDER = "TechnicalDataHolder";

    private void Awake()
    {
        _spriteProvider = ProjectContext.Instance.SceneContext.SpriteProvider;
        _objectProvider = ProjectContext.Instance.SceneContext.ObjectProvider;
    }

    private void OnEnable()
    {
        var technicalDataHolder = _objectProvider.GetObject(TECH_DATA_HOLDER).GetComponent<TechnicalLevelDataHolder>();
        var levelData = technicalDataHolder.GetLevelData(_targetLevel);
        if(levelData != null)
        {
            _spriteProvider.SwitchSpriteData(levelData);
            if (IsNormalWalls)
                ProjectContext.Instance.SceneContext.BorderController.IsNormalWalls = true;
            else
                ProjectContext.Instance.SceneContext.BorderController.IsNormalWalls = false;
        }
    }
}
