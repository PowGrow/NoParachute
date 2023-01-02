using System;
using UnityEngine;

public class TransitionAnimationController : MonoBehaviour
{
    private Animator _animator;
    private GameObject _objectToHide;
    private GameObject _objectToShow;

    public event Action ScreenIsBlack;

    private const string FADE_IN = "TransitionFadeIn";
    private const string FADE_OUT = "TransitionFadeOut";

    private void TransitionAnimationEventHandler()
    {
        HideTransition(_objectToHide);
    }

    public void ShowTransition()
    {
        _animator.Play(FADE_IN);
    }

    public void ShowTransition(GameObject objectToHide,GameObject objectToShow)
    {
        _objectToHide = objectToHide;
        _objectToShow = objectToShow;
        _animator.Play(FADE_IN);
    }

    private void HideTransition(GameObject objectToHide)
    {
        ScreenIsBlack?.Invoke();
        if(objectToHide != null)
            objectToHide.SetActive(false);
        if (_objectToShow != null)
            _objectToShow.SetActive(true);
        _animator.Play(FADE_OUT);
        _objectToHide = null;
        _objectToShow = null;
    }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
