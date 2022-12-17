using System.Linq;
using UnityEngine;

public class SidePlaneController : MonoBehaviour
{
    [SerializeField] private GameObject _planePrefab;
    [SerializeField] private Transform _floorTransform;
    [SerializeField] private int _initializePlanesCount;

    private int _levelId = 0;
    private Level _levelInformation;
    private int _previosWallId;

    private GameObject CreatePlane(int previousWallId)
    {
        var plane = Instantiate(_planePrefab, this.transform);
        var planeSpriteRenderer = plane.GetComponent<SpriteRenderer>();
        planeSpriteRenderer.sprite = GetNextWall(previousWallId);
        var planeEventHandler = plane.GetComponent<SidePlaneEventHandler>();
        planeEventHandler.SpawnNewPlaneEvent += SpawnNewPlaneEventHandler;
        planeEventHandler.DestroyPlaneEvent += DestroyPlaneEventHandler;
        return plane;
    }

    private void DestroyPlaneEventHandler(SidePlaneEventHandler planeEventHandler)
    {
        planeEventHandler.SpawnNewPlaneEvent -= SpawnNewPlaneEventHandler;
        planeEventHandler.DestroyPlaneEvent -= DestroyPlaneEventHandler;

        Destroy(planeEventHandler.gameObject);
    }

    private void SpawnNewPlaneEventHandler()
    {
        CreatePlane(_previosWallId);
    }

    private Sprite GetNextWall(int previousWallId)
    {
        _previosWallId = ++previousWallId % _levelInformation.Walls.Count();
        return _levelInformation.Walls[_previosWallId];
    }

    private void Awake()
    {
        _levelInformation = Resources.Load<Level>($"ScriptableObjects/Levels/Level_{_levelId}");
        CreatePlane(_previosWallId);
    }
}
