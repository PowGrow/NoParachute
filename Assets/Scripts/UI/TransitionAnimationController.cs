using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Image _transitionImage;
    private GameObject _objectToHide;

    private const string FADE_IN = "TransitionFadeIn";
    private const string FADE_OUT = "TransitionFadeOut";

    private void TransitionAnimationEventHandler()
    {
        HideTransition(FADE_IN, _objectToHide);
    }

    public void ShowTransition(GameObject objectToHide)
    {
        _transitionImage.enabled = true;
        _objectToHide = objectToHide;
        _animator.Play(FADE_IN);
    }

    private void HideTransition(string animationToPlay, GameObject objectToHide)
    {
        if(objectToHide != null)
        {
            objectToHide.SetActive(false);
            var animationLength = _animator.GetCurrentAnimatorClipInfo(0).Length;
            StartCoroutine(PlayAndDisable(animationToPlay, animationLength));
        }
    }

    IEnumerator PlayAndDisable(string animationToPlay, int animationLength)
    {
        _animator.Play(FADE_OUT);
        yield return new WaitForSeconds(animationLength);
        _transitionImage.enabled = false;
        _objectToHide = null;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _transitionImage = GetComponent<Image>();
    }
}
