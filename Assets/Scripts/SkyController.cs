using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

//Script encargada de manipular todo lo del global volume
public class SkyController : MonoBehaviour
{
    public Volume skyVolume; //Global volume
    public GameObject sun; //Objeto luz sol
    private HDAdditionalLightData sunData; //Componente data de luz HD
    private Light sunLight; //Componente del objeto luz sol
    private VolumetricClouds volumetricClouds; //Componente en skyVolume de las nubes volumetricas
    [SerializeField] public Material EmissionMaterial; //Emitir luz en materiales
    [SerializeField] public Color LigthEmsvColor = new Color(1f, 0.8f, 0.5f, 1f);
    public VolumetricClouds.CloudPresets[] cloudsPrefabs = { VolumetricClouds.CloudPresets.Overcast }; //Obtener los tipos de nubes predefinidas
    public VolumetricClouds.CloudPresets cloudPresetSelected; //Tupo de nube a seleccionar
    private float emissiveIntensityNight = 7;
    private float emissiveIntensityDay = 0;
    
    //Inicializamos variables y llamamos función de nubes
    void Start()
    {
        sunData = sun.GetComponent<HDAdditionalLightData>();
        sunLight = sun.GetComponent<Light>();
        SetCloudPreset();
    }
    //Función para elegir un tipo de nubes aleatorio que se mantendra durante la ejecución del programa
    void SetCloudPreset() 
    {
        skyVolume.profile.TryGet<VolumetricClouds>(out volumetricClouds);
        int randomCloud = Random.Range(0, cloudsPrefabs.Length - 1);
        cloudPresetSelected = cloudsPrefabs[randomCloud];
        volumetricClouds.cloudPreset = cloudPresetSelected;
    }

    //Función para cambiar la hora del día: amanecer, mediodía, atardecer, noche
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
                //sun set
                sunData.intensity = 550f;
                sun.transform.Rotate(175.0f, 0.0f, 0.0f, Space.Self);
                EmissionMaterial.SetColor("_EmissiveColor", LigthEmsvColor * emissiveIntensityNight);
                sunLight.colorTemperature = 5500f;
                break;
            case 4:
                sun.transform.rotation = Quaternion.identity;
                //night
                sunData.intensity = 1f;
                sun.transform.Rotate(60.0f, 0.0f, 0.0f, Space.Self);
                sunLight.colorTemperature = 15000f;
                EmissionMaterial.SetColor("_EmissiveColor", LigthEmsvColor * emissiveIntensityNight);
                break;
        }
    }
}
