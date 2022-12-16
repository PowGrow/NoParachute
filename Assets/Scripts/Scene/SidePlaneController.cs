using UnityEngine;

public class SidePlaneController : MonoBehaviour
{
    [SerializeField] private GameObject _planePrefab;
    [SerializeField] private Transform _floorTransform;

    private const int DISTANCE_BEETWEN_PLANES = 2;

    private void Initialize(int distanceBetweenCameraAndFloor)
    {
        var initializePlanesCount = distanceBetweenCameraAndFloor / DISTANCE_BEETWEN_PLANES;

        for (int i = initializePlanesCount; i > 0; i--)
        {
            var plane = Instantiate(_planePrefab, this.transform);
            float planeAnimationPosition = (float)i / (float)initializePlanesCount;
            var planeAnimator = plane.GetComponent<Animator>();
            planeAnimator.Play("PlaneMovement", -1 , planeAnimationPosition);
        }
    }
    private void SpawnNewPlane()
    {
        var plane = Instantiate(_planePrefab,this.transform);
        var planeEventHandler = plane.GetComponent<SidePlaneEventHandler>();

        planeEventHandler.SpawnNewPlaneEvent += SpawnNewPlaneEventHandler;
        planeEventHandler.DestroyPlaneEvent += DestroyPlaneEventHandler;
    }

    private void DestroyPlaneEventHandler(SidePlaneEventHandler planeEventHandler)
    {
        planeEventHandler.SpawnNewPlaneEvent -= SpawnNewPlaneEventHandler;
        planeEventHandler.DestroyPlaneEvent -= DestroyPlaneEventHandler;

        Destroy(planeEventHandler.gameObject);
    }

    private void SpawnNewPlaneEventHandler()
    {
        SpawnNewPlane();
    }

    private void Awake()
    {
        Initialize((int)_floorTransform.position.z);
    }
}
