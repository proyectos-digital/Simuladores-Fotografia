using UnityEngine;

public class DisableRender : MonoBehaviour
{
    [SerializeField] CameraManager camManager;
    bool disableRender;

    //Script para activar render de la camara en studio... Sin usar por motivos de rendimiento
    void Start()
    {
        disableRender = true;
        camManager.panelStudy += DisableR;
    }

    void DisableR() {
        disableRender = !disableRender;
        gameObject.SetActive(disableRender);
    }
}
