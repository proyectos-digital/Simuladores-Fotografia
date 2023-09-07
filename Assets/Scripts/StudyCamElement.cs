using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StudyCamElement : MonoBehaviour
{
    bool active = false;
    [SerializeField] GameObject panelInfo;
    [SerializeField] CameraManager camManager;
    TMP_Text txtInfo;
    
    void Start() {
        txtInfo = panelInfo.GetComponentInChildren<TMP_Text>();
        camManager = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManager>();
    }
    void Update()
    {
        if (active && Input.GetKeyUp(KeyCode.E)) {
            camManager.PanelCamStudy();
            panelInfo.SetActive(!panelInfo.activeSelf);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            txtInfo.text = "Presiona E para Modo Cámara.";
            panelInfo.SetActive(true);
            active = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            active = false;
            txtInfo.text = "";
            panelInfo.SetActive(false);
        }
    }
}
