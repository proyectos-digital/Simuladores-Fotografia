using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimations : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Screenshot screenshot;
    [SerializeField] private GameObject cameraRender;
    private Animator cameraAnimator;

    //Crear Delegado y Evento
    public delegate void OpenPanel();
    public event OpenPanel openPanel;
    [SerializeField] bool isStudy;

    void Start(){
        screenshot = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Screenshot>();
        cameraAnimator = GetComponent<Animator>();
        if (!isStudy) {
            cameraManager.cameraAnimation += MoveCamera;
            screenshot.cameraOrientation += ChangeOrientation;
        }
    }
    //Funciones para controlar el estado de animaciones, entra o sale de modo cámara
    void MoveCamera(bool isOpen) {
        cameraAnimator.SetBool("IsMode", isOpen);
    }
    //Funciones para controlar el estado de animaciones, cambia orientación de cámara
    void ChangeOrientation(bool isHorizontal) {
        cameraAnimator.SetBool("IsHorizontal", isHorizontal);
    }
    //Funcion para ejecutar el Evento openPanel
    void OnOffPanel() {
        openPanel();
    }
    void ResetCamera() {
        cameraManager.ResetCamera();
    }
}
