using System.Collections.Generic;
using UnityEngine;

public class ObstacleAnimator : MonoBehaviour
{
    [SerializeField] private List<Animator> _animators;

    private WallEventHandler _wallEventHandler;

    private const string OBSTACLE_DISAPEAR_ANIMATION = "ObstacleDisapear";
    private void DisapearObstacleEventHandler()
    {
        foreach(Animator animator in _animators)
        {
            animator.Play(OBSTACLE_DISAPEAR_ANIMATION);
        }
    }

    private void Awake()
    {
        _wallEventHandler = transform.parent.GetComponent<WallEventHandler>();
    }

    private void OnEnable()
    {
        _wallEventHandler.ObstacleFadeOutEvent += DisapearObstacleEventHandler;
    }

    private void OnDisable()
    {
        _wallEventHandler.ObstacleFadeOutEvent -= DisapearObstacleEventHandler;
    }
}
