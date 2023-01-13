using System;
using UnityEngine;

public class CollisionDetectorForTors : MonoBehaviour
{
    public bool IsAlive { get; private set; } = true;
    [SerializeField] private ScoreCounter _scoreCounter;
    private AudioSource _audioSource;
    public static event Action PlayerDeath; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Border")
        {
            if (other.gameObject.tag == "Breakable")
            {
                if (WallAnimator.CurrentSpeed != WallSpeed.Fast)
                    Death(other);
                else
                {
                    var breakable = other.GetComponent<Breakable>();
                    breakable.Break();
                }
            }
            else
                Death(other);
        }
    }

    private void Death(Collider other)
    {
        Movement.Instance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z - 0.2f);
        _audioSource.Play();
        _scoreCounter.Deaths++;
        IsAlive = false;
        PlayerDeath.Invoke();
        Time.timeScale = 0;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
