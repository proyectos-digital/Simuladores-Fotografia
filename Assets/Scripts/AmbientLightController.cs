using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLightController : MonoBehaviour {

    [SerializeField] private Light ambientLight;
    [SerializeField] private Light[] lampLights;

    public Material dayMaterial, nightMaterial;

    public void ChangeDay(float time) {
        //ambientLight.transform.Rotate(new Vector3(25, 0f, 0f));
        ambientLight.transform.localEulerAngles = new Vector3(time, 0, 0);
        if (time > 155) {
            Debug.Log("time: " + time);
            RenderSettings.skybox = nightMaterial;
            ActivateLamps();
        } else {
            DisactivateLamps();
            RenderSettings.skybox = dayMaterial;
        }
    }

    public void ChangeTemperature(float value) {
        ambientLight.colorTemperature = value;
    }

    void ActivateLamps() {
        foreach(Light light in lampLights) {
            light.gameObject.SetActive(true);
        }
    }
    void DisactivateLamps() {
        foreach (Light light in lampLights) {
            light.gameObject.SetActive(false);
        }
    }
}
