using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRender : MonoBehaviour
{
    [SerializeField] CameraManager camManager;
    bool disableRender;
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
