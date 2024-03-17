using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    public Volume skyVolume;
    public Light sun;
    [SerializeField] private VolumetricClouds volumetricClouds = null;
    [SerializeField] private Material EmissionMaterial;

    public VolumetricClouds.CloudPresets[] cloudsPrefabs = { VolumetricClouds.CloudPresets.Overcast };
    public VolumetricClouds.CloudPresets cloudPresetSelected;
    // Start is called before the first frame update
    void Start()
    {
        SetCloudPreset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetCloudPreset() 
    {
        skyVolume.profile.TryGet<VolumetricClouds>(out volumetricClouds);
        cloudPresetSelected = cloudsPrefabs[Random.Range(0, cloudsPrefabs.Length - 1)];
        volumetricClouds.cloudPreset = cloudPresetSelected;
        Debug.Log(cloudPresetSelected);
    }

    public void SetTimeOfDay(float time)
    {
       sun.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
        switch (time)
        {
            case 1:
                //sunrise
                sun.intensity = 5000f;
                sun.colorTemperature = 6500f;
                //sun.transform.Rotate(5.0f,0.0f,0.0f);
                //EmissionMaterial.DisableKeyword("_EMISSION");
                break;
            case 2:

                //EmissionMaterial.DisableKeyword("_EMISSION");
                break;
            case 3:

                //EmissionMaterial.DisableKeyword("_EMISSION");
                break;
            case 4:
                //EmissionMaterial.EnableKeyword("_EMISSION");
                break;
        }
    }
}
