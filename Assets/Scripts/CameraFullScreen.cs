using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFullScreen : MonoBehaviour
{
    public Camera[] allCameras;
    public GameObject[] panelFullScreen;

    public void OffScreenCamera(Camera cam)
    {
        for (int i = 0; i < allCameras.Length; i++)
        {
            allCameras[i].enabled = false;
        }

        cam.enabled = true;
    }
    public void ActivatePanel(GameObject panel)
    {
        for (int i = 0; i < panelFullScreen.Length; i++)
        {
            panelFullScreen[i].gameObject.SetActive(false);
        }
        panel.SetActive(true);
    }
}
