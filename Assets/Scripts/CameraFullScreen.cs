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

    [Header("RenderCameras")]
    [SerializeField] GameObject camRenderPrincipal;
    [SerializeField] GameObject camRenderAuxiliar;
    [SerializeField] GameObject camPlayer;

    void Start()
    {
        ChangeStateRenders(false);
    }

    void Update()
    {
        if (isPanelFullScreen && (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.P)))          
        {
            ResetState();
        }
    }

    //Funcion usada en el UI, los botones BtnFullScreen
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
    //Revisar funcionamiento de LAS FUNCIONES DE ESTE SCRIPT
    void LoopCameras(bool state)
    {
        //Las 3 camaras
        for (int i = 0; i < allCameras.Length; i++)
        {
            allCameras[i].enabled = state;
        }
        //Desactivamos los renders de miniatura revisar en los escenarios
        ChangeStateRenders(false);
    }
    //Funcion para cambiar el estado de los paneles de perspectiva y desactivar los de pantalla completa
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

    //Ajustar desactivado y activado de RendersCamera
    public void ChangeStateRenders(bool state)
    {
        //camRenderPrincipal.SetActive(state);
        //camRenderAuxiliar.SetActive(state);
    }
}
