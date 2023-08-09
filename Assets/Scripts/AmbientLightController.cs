using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLightController : MonoBehaviour {

    [SerializeField] private Light ambientLight;
    [SerializeField] private Light[] lampLights;

    public Material dayMaterial, nightMaterial,sunriseMaterial,sunsetMaterial;

    public void ChangeDay(float time) {
        DisactivateLamps();
        //ambientLight.transform.Rotate(new Vector3(25, 0f, 0f));
        //ambientLight.transform.localEulerAngles = new Vector3(time, ambientLight.transform.localEulerAngles.y, ambientLight.transform.localEulerAngles.z);
        /*if (time > 155) {
            RenderSettings.skybox = nightMaterial;
            RenderSettings.ambientIntensity = 0.5f;
            ActivateLamps();
        } else {
            DisactivateLamps();
            RenderSettings.skybox = dayMaterial;
            RenderSettings.ambientIntensity = 1f;
        }*/
        switch (time)
        {
            case 1:
                RenderSettings.skybox = sunriseMaterial;
                DisactivateLamps();
                RenderSettings.fog = true;
                RenderSettings.fogColor = new Color32(0x6C, 0x86, 0x9C, 0xff);
                RenderSettings.fogDensity = 0.023f;
                break;
            case 2:
                RenderSettings.skybox = dayMaterial;
                DisactivateLamps();
                break;
            case 3:
                RenderSettings.skybox = sunsetMaterial;
                RenderSettings.fog = true;
                RenderSettings.fogColor = new Color32(0x67,0x4E,0x28,0xff);
                RenderSettings.fogDensity = 0.023f;
                ActivateLamps();
                break;
            case 4:
                RenderSettings.skybox = nightMaterial;
                RenderSettings.fog = true;
                RenderSettings.fogColor = new Color32(0x25, 0x29, 0x35, 0xff);
                RenderSettings.fogDensity = 0.04f;
                ActivateLamps();
                break;
        }
    }

    public void ChangeIntensity(float value){
        ambientLight.intensity = value;
    }
    public void ChangeTemperature(float value) {
        ambientLight.colorTemperature = value;
    }

    void ActivateLamps() {
        foreach(Light light in lampLights) {
            light.gameObject.SetActive(true);
        }
        //ambientLight.gameObject.SetActive(false);
    }
    void DisactivateLamps() {
        foreach (Light light in lampLights) {
            light.gameObject.SetActive(false);
        }
        ambientLight.gameObject.SetActive(true);
    }
}
