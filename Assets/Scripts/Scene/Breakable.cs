using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;

    private BoxCollider _collider;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }
    public void Break()
    {
        _collider.enabled = false;
        _spriteRenderer.enabled = false;
        _audioSource.Play();
        _particles.Play();
    }
}
