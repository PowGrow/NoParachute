using UnityEngine;

public class ObjectAutoAdd : MonoBehaviour
{
    private IObjectProvider _objectProvider;
    public void Awake()
    {
        _objectProvider = ProjectContext.Instance.SceneContext.ObjectProvider;
    }

    private void OnEnable()
    {
        _objectProvider.AddObject(gameObject.name, gameObject);
    }

    private void OnDisable()
    {
        _objectProvider.RemoveObject(gameObject.name);
    }
}
