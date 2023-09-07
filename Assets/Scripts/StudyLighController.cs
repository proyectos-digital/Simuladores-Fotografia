using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyLighController : MonoBehaviour{

    //En Desuso todo el script
    [SerializeField] private Light umbrellaLight;
    [SerializeField] private Light softboxLight;
    [SerializeField] private Light softboxLight2;
    [SerializeField] private Light panelLeftLight;
    [SerializeField] private Light panelRightLight;
    [SerializeField] private Light panelCenterLight;
    [SerializeField] private Material materialOff;
    [SerializeField] private Material materialOn;
    [SerializeField] private Material materialPanelOn;

    private bool umbrella = true, softboxC = true, softboxR = true;
    private bool panelCenter = true, panelLeft = true, panelRight = true;

    public void OnOffUmbrella(int value){
        switch (value) {
            case 1:
                umbrella = !umbrella;
                umbrellaLight.enabled = umbrella;
                umbrellaLight.GetComponentInChildren<Renderer>().material = umbrella ? materialOn : materialOff;
            break;
            case 2:
                softboxC = !softboxC;
                softboxLight.enabled = softboxC;
                softboxLight.GetComponentInChildren<Renderer>().material = softboxC ? materialOn : materialOff;
            break;
            case 3:
                softboxR = !softboxR;
                softboxLight2.enabled = softboxR;
                softboxLight2.GetComponentInChildren<Renderer>().material = softboxR ? materialOn : materialOff;
            break;
            case 4:
                panelLeft = !panelLeft;
                panelLeftLight.enabled = panelLeft;
                panelLeftLight.GetComponentInChildren<Renderer>().material = panelLeft ? materialPanelOn : materialOff;
            break;
            case 5:
                panelCenter = !panelCenter;
                panelCenterLight.enabled = panelCenter;
                panelCenterLight.GetComponentInChildren<Renderer>().material = panelCenter ? materialPanelOn : materialOff;
            break;
            case 6:
                panelRight = !panelRight;
                panelRightLight.enabled = panelRight;
                panelRightLight.GetComponentInChildren<Renderer>().material = panelRight ? materialPanelOn : materialOff;
            break;
        }
        //if (!umbrella) {
        //    mat = umbrellaMaterialOff;
        //} else {
        //    mat = umbrellaMaterialOn;
        //}
        //    Debug.Log("umbrella es_"+mat);
        
    }

    // Update is called once per frame
    void Update(){
        
    }
}
