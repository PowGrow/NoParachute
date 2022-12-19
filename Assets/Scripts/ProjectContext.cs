using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    public SpriteProvider SpriteProvider { get; private set; }
    public static ProjectContext Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Initialize(int levelId)
    {
        SpriteProvider = new SpriteProvider(levelId);
    }
}
