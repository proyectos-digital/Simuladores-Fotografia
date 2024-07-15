using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    public Volume skyVolume;
    public GameObject sun;
    private HDAdditionalLightData sunData;
    private Light sunLight;
    private VolumetricClouds volumetricClouds;
    [SerializeField] public Material EmissionMaterial;
    [SerializeField] public Color LigthEmsvColor = new Color(1f, 0.8f, 0.5f, 1f);
    public VolumetricClouds.CloudPresets[] cloudsPrefabs = { VolumetricClouds.CloudPresets.Overcast };
    public VolumetricClouds.CloudPresets cloudPresetSelected;
    private float emissiveIntensityNight = 7;
    private float emissiveIntensityDay = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        sunData = sun.GetComponent<HDAdditionalLightData>();
        sunLight = sun.GetComponent<Light>();
        SetCloudPreset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetCloudPreset() 
    {
        skyVolume.profile.TryGet<VolumetricClouds>(out volumetricClouds);
        int randomCloud = Random.Range(0, cloudsPrefabs.Length - 1);
        cloudPresetSelected = cloudsPrefabs[randomCloud];
        volumetricClouds.cloudPreset = cloudPresetSelected;
    }

    public void SetTimeOfDay(float time)
    {
        switch (time)
        {
            case 1:
                sun.transform.rotation = Quaternion.identity;
                //sunrise
                sunData.intensity = 5000f;
                sun.transform.Rotate(21.0f, 0.0f, 0.0f, Space.Self);
                sunLight.colorTemperature = 4000f;
                EmissionMaterial.SetColor("_EmissiveColor", LigthEmsvColor * emissiveIntensityDay);
                break;
            case 2:
                sun.transform.rotation = Quaternion.identity;
                //mid sun
                sunData.intensity = 30000f;
                sun.transform.Rotate(75.0f, 0.0f, 0.0f, Space.Self);
                EmissionMaterial.SetColor("_EmissiveColor", LigthEmsvColor * emissiveIntensityDay);
                sunLight.colorTemperature = 5500f;
                break;
            case 3:
                sun.transform.rotation = Quaternion.identity;
                //mid sun
                sunData.intensity = 550f;
                sun.transform.Rotate(175.0f, 0.0f, 0.0f, Space.Self);
                EmissionMaterial.SetColor("_EmissiveColor", LigthEmsvColor * emissiveIntensityNight);
                sunLight.colorTemperature = 5500f;
                break;
            case 4:
                sun.transform.rotation = Quaternion.identity;
                //mid sun
                sunData.intensity = 1f;
                sun.transform.Rotate(60.0f, 0.0f, 0.0f, Space.Self);
                sunLight.colorTemperature = 15000f;
                EmissionMaterial.SetColor("_EmissiveColor", LigthEmsvColor * emissiveIntensityNight);
                break;
        }
    }
}
