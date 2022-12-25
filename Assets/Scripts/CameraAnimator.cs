using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    private Animator _animator;

    private const string INITIALIZE_ANIMATION = "InitializeAnimation";

    public void PlayStartAnimation()
    {
        _animator.Play(INITIALIZE_ANIMATION,-1,0f);
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
