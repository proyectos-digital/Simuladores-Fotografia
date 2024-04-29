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

    void MoveCamera(bool isOpen) {
        cameraAnimator.SetBool("IsMode", isOpen);
        //Desactivar renderizar en miniatura camara si da problemas de rendimiento
        //cameraRender.SetActive(!isOpen);
    }
    void ChangeOrientation(bool isHorizontal) {
        cameraAnimator.SetBool("IsHorizontal", isHorizontal);
    }
    //Funcion para ejecutar el Evento openPanel
    void OnOffPanel() {
        openPanel();
    }
    //Funciones usadas como eventos en las animaciones de la camara
    void LoadCamera() {
        cameraManager.LoadCamera();
    }
    void ResetCamera() {
        cameraManager.ResetCamera();
    }
}
