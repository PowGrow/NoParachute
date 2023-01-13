using UnityEngine;

public class SpeedTrigger : MonoBehaviour
{
    [SerializeField] private WallSpeed _speed;
    private ISceneContext _sceneContext;
    private void Awake()
    {
        _sceneContext = ProjectContext.Instance.SceneContext;
    }

    private void OnEnable()
    {
        _sceneContext.WallAnimator.SetWallSpeed(_speed);
    }
}
