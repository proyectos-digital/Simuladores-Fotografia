using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StudyLightRoof : MonoBehaviour
{
    bool active = true;
    Light lightObj;
    [SerializeField] Material materialOff;
    [SerializeField] Material materialOn;
    void Start()
    {
        lightObj = GetComponent<Light>();
    }

    public void ChangeState() {
        active =! active;
        lightObj.enabled = active;
        lightObj.GetComponentInChildren<Renderer>().material = active ? materialOn : materialOff;
    }
}
