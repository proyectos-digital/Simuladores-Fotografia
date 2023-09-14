using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StudyLightElement : MonoBehaviour { 


    bool active = false;
    Light lightObj;
    [SerializeField] GameObject panelInfo;
    [SerializeField] Material materialOff;
    [SerializeField] Material materialOn;
    TMP_Text txtInfo;

    void Start(){
        lightObj = GetComponent<Light>();
        txtInfo = panelInfo.GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        if(active && Input.GetKeyUp(KeyCode.Q)) {
            lightObj.enabled = !lightObj.isActiveAndEnabled;
            lightObj.GetComponentInChildren<Renderer>().material = lightObj.enabled ? materialOn : materialOff;
            txtInfo.text = lightObj.enabled ? "Presiona Q para apagar la Luz.": "Presiona Q para encender Luz.";
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            active = true;
            panelInfo.SetActive(true);
            txtInfo.text = lightObj.enabled ? "Presiona Q para apagar la Luz." : "Presiona Q para encender Luz.";
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
