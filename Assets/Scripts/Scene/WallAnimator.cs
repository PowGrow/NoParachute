
using UnityEngine;

public class WallAnimator : MonoBehaviour
{
    private WallController _wallController;
    private Movement _playerMovement;

    private const string PLAYER_OBJECT_NAME = "Character";
    public static WallSpeed CurrentSpeed { get; set; }

    public void Initialize(WallController walLController)
    {
        CurrentSpeed = WallSpeed.Normal;
        _wallController = walLController;
        gameObject.SetActive(true);
    }

    private void SpeedChangeEventHandler(WallSpeed speed)
    {
        foreach (Animator animator in _wallController.WallAnimators)
        {
            animator.speed = (float)speed;
        }
    }


    private void OnEnable()
    {
        _playerMovement = ProjectContext.Instance.SceneContext.ObjectProvider.GetObject(PLAYER_OBJECT_NAME).GetComponent<Movement>();
        if(_playerMovement != null)
            _playerMovement.SpeedChangeEvent += SpeedChangeEventHandler;
    }

    private void OnDisable()
    {
        if(_playerMovement != null)
            _playerMovement.SpeedChangeEvent -= SpeedChangeEventHandler;
    }
}
