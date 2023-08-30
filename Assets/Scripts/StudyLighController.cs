using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyLighController : MonoBehaviour{

    [SerializeField] private Light umbrellaLight;
    [SerializeField] private Light softboxLight;
    [SerializeField] private Light softboxLight2;
    [SerializeField] private Light panelLeftLight;
    [SerializeField] private Light panelRightLight;
    [SerializeField] private Light panelCenterLight;
    [SerializeField] private Material materialOff;
    [SerializeField] private Material materialOn;

    private bool umbrella = true, softbox = true, softbox2 = true;
    private bool panelCenter = true, panelLeft = true, panelRight = true;

    public void OnOffUmbrella(int value){
        switch (value) {
            case 0:
                umbrella = !umbrella;
                umbrellaLight.enabled = umbrella;
                umbrellaLight.GetComponentInChildren<Renderer>().material = umbrella ? materialOn : materialOff;
            break;
            case 1:
                softbox = !softbox;
                softboxLight.enabled = softbox;
                softboxLight.GetComponentInChildren<Renderer>().material = softbox ? materialOn : materialOff;
            break;
            case 2:
                softbox2 = !softbox2;
                softboxLight2.enabled = softbox2;
                softboxLight2.GetComponentInChildren<Renderer>().material = softbox2 ? materialOn : materialOff;
            break;
            case 3:
                panelLeft = !panelLeft;
                panelLeftLight.enabled = panelLeft;
                panelLeftLight.GetComponentInChildren<Renderer>().material = panelLeft ? materialOn : materialOff;
            break;
            case 4:
                panelRight = !panelRight;
                panelRightLight.enabled = panelRight;
                panelRightLight.GetComponentInChildren<Renderer>().material = panelRight ? materialOn : materialOff;
            break;
            case 5:
                panelCenter = !panelCenter;
                panelCenterLight.enabled = panelCenter;
                panelCenterLight.GetComponentInChildren<Renderer>().material = panelCenter ? materialOn : materialOff;
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
