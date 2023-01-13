using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSource : MonoBehaviour
{
    private AudioSource _musicSource;

    private void Start()
    {
        _musicSource = GetComponent<AudioSource>();
        _musicSource.clip = ProjectContext.Instance.SceneContext.SoundProvider.LevelTheme;
        _musicSource.Play();
    }
}
