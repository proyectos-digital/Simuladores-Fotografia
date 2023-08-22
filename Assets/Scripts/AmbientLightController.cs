using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AmbientLightController : MonoBehaviour {

    [SerializeField] private Light ambientLight;
    [SerializeField] private LensFlareComponentSRP sunFlare;
    [SerializeField] private Light[] lampLights;

    public Material dayMaterial, nightMaterial,sunriseMaterial,sunsetMaterial;

    public void ChangeDay(float time) {
        switch (time)
        {
            case 1:
                RenderSettings.skybox = sunriseMaterial;
                RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
                RenderSettings.fog = true;
                RenderSettings.fogColor = new Color32(0x47, 0x6A, 0x7D, 0xff);
                sunFlare.gameObject.SetActive(true);
                break;
            case 2:
                RenderSettings.skybox = dayMaterial;
                RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
                sunFlare.gameObject.SetActive(false);
                break;
            case 3:
                RenderSettings.skybox = sunsetMaterial;
                RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
                RenderSettings.fog = true;
                RenderSettings.fogColor = new Color32(0x67,0x4E,0x28,0xff);
                RenderSettings.fogDensity = 0.023f;
                sunFlare.gameObject.SetActive(true);
                break;
            case 4:
                RenderSettings.skybox = nightMaterial;
                RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
                RenderSettings.ambientLight = new Color32(0x46, 0x4E, 0x62, 0xff);
                RenderSettings.fog = true;
                RenderSettings.fogColor = new Color32(0x25, 0x29, 0x35, 0xff);
                RenderSettings.fogDensity = 0.04f;
                sunFlare.gameObject.SetActive(false);
                break;
        }
    }

    public void ChangeIntensity(float value){
        ambientLight.intensity = value;
    }

    public void ChangeDensitiyFog(float value){
        RenderSettings.fogDensity = value;
    }

    public void Lamps(bool value){
        Debug.Log("Bool: " + value);
        foreach (Light light in lampLights)
        {
            light.gameObject.SetActive(value);
        }
    }

    public void ChangeTemperature(float value) {
        ambientLight.colorTemperature = value;
    }
}
