using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFullScreen : MonoBehaviour
{
    public Camera[] allCameras;
    public GameObject[] panelFullScreen;
    public GameObject[] panelPanelsPerspective;
    bool isPanelFullScreen = false;

    void Update()
    {
        if (isPanelFullScreen && (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.P)))
        {
            ResetState();
        }
    }
    public void OffScreenCamera(Camera cam)
    {
        //LoopCameras(false);
        //REVISAR FUNCION
        cam.enabled = true;
    }
    public void ActivatePanel(GameObject panel)
    {
        LoopPanels(false);
        panel.SetActive(true);
        isPanelFullScreen = true;
    }
    public void ResetState()
    {

        LoopCameras(true);
        LoopPanels(true);
        isPanelFullScreen = false;
    }
    void LoopCameras(bool state)
    {
        //Las 3 camaras
        for (int i = 0; i < allCameras.Length; i++)
        {
            allCameras[i].enabled = state;
        }
    }
    void LoopPanels(bool state)
    {
        //Los 2 paneles de pantalla completa
        for (int i = 0; i < panelFullScreen.Length; i++)
        {
            panelFullScreen[i].gameObject.SetActive(false);
        }
        //Las 3 miniaturas de cada camara en el modo Configuracion
        for (int i = 0; i < panelPanelsPerspective.Length; i++)
        {
            panelPanelsPerspective[i].SetActive(state);
        }
    }
}
