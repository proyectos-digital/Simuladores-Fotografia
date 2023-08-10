using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimations : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private GameObject panelUI;
    [SerializeField] private GameObject cameraRender;
    private Animator cameraAnimator;
    private bool isOpenPanel = false;
    // Start is called before the first frame update
    void Start()
    {
        cameraAnimator = GetComponent<Animator>();
        cameraManager.cameraAnimation += MoveCamera;
        OnOffPanel();
    }

    void MoveCamera() {
        isOpenPanel = !isOpenPanel;
        cameraAnimator.SetBool("IsMode", isOpenPanel);
        //Desactivar renderizar en miniatura camara si da problemas de rendimiento
        //cameraRender.SetActive(isOpenPanel);
    }

    void OnOffPanel() {
        panelUI.SetActive(isOpenPanel);
    }
}
