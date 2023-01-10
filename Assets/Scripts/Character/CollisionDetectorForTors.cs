using System;
using UnityEngine;

public class CollisionDetectorForTors : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    private AudioSource _audioSource;
    public static event Action PlayerDeath; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Border")
        {
            Movement.Instance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z-0.2f);
            _audioSource.Play();
            _scoreCounter.Deaths++;
            PlayerDeath.Invoke();
            Time.timeScale = 0;
        }
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
