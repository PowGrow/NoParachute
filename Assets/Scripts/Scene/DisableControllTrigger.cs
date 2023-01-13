using UnityEngine;

public class DisableControllTrigger : MonoBehaviour
{

    private void OnEnable()
    {
        Movement.DisableControlls();
    }
}
