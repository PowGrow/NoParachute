using UnityEngine;

internal class SoundProvider : ISoundProvider
{
    public AudioClip LevelTheme { get; private set; }

    public SoundProvider(LevelData currentLevel)
    {
        LevelTheme = currentLevel.LevelTheme;
    }
}