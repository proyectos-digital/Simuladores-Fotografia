using UnityEngine.Rendering;
//using UnityEngine.Rendering.HighDefinition;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Android;

//Script encargada de manipular todo lo del global volume
public class SkyController : MonoBehaviour
{
    public Volume skyVolume; //Global volume
    public GameObject sun; //Objeto luz sol
    //private HDAdditionalLightData sunData; //Componente data de luz HD
    private UniversalAdditionalLightData sunData;
    private Light sunLight; //Componente del objeto luz sol
    //private VolumetricClouds volumetricClouds; //Componente en skyVolume de las nubes volumetricas
    [SerializeField] public Material EmissionMaterial; //Emitir luz en materiales
    [SerializeField] public Color LigthEmsvColor = new Color(1f, 0.8f, 0.5f, 1f);
    //public VolumetricClouds.CloudPresets[] cloudsPrefabs = { VolumetricClouds.CloudPresets.Overcast }; //Obtener los tipos de nubes predefinidas
    //public VolumetricClouds.CloudPresets cloudPresetSelected; //Tupo de nube a seleccionar
    private float emissiveIntensityNight = 10;
    private float emissiveIntensityDay = 0;
    private AmbientLightController ambientLightController;

    [SerializeField]
    private Material[] skyboxes; //[0= Cloudymorning, 1=CasualDay, 2=HighFantasy, 3=CoriolisNight4k]

    //Inicializamos variables y llamamos función de nubes
    void Start()
    {
        sunData = sun.GetComponent<UniversalAdditionalLightData>();
        sunLight = sun.GetComponent<Light>();
        ambientLightController = GetComponent<AmbientLightController>();
        SetCloudPreset();
    }
    //Función para elegir un tipo de nubes aleatorio que se mantendra durante la ejecución del programa
    void SetCloudPreset() 
    {
        int randomCloud = Random.Range(0, skyboxes.Length - 1);
        RenderSettings.skybox = skyboxes[randomCloud];
        SetTimeOfDay(randomCloud + 1);
        ambientLightController.Lamps(false);

    }

    //Función para cambiar la hora del día: amanecer, mediodía, atardecer, noche
    public void SetTimeOfDay(float time)
    {
        switch (time)
        {
            case 1:
                sun.transform.rotation = Quaternion.identity;
                //sunrise
                sunLight.intensity = 1f;
                sun.transform.Rotate(21.0f, 0.0f, 0.0f, Space.Self);
                sunLight.colorTemperature = 4000f;
                EmissionMaterial.SetColor("_EmissiveColor", LigthEmsvColor * emissiveIntensityDay);
                EmissionMaterial.DisableKeyword("_EMISSION");
                RenderSettings.skybox = skyboxes[(int)time-1];
                ambientLightController.Lamps(false);
                break;
            case 2:
                sun.transform.rotation = Quaternion.identity;
                //mid sun
                sunLight.intensity = 2f;
                sun.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
                EmissionMaterial.SetColor("_EmissiveColor", LigthEmsvColor * emissiveIntensityDay);
                EmissionMaterial.DisableKeyword("_EMISSION");
                sunLight.colorTemperature = 5500f;
                RenderSettings.skybox = skyboxes[(int)time - 1];
                ambientLightController.Lamps(false);
                break;
            case 3:
                sun.transform.rotation = Quaternion.identity;
                //sun set
                sunLight.intensity = 1.3f;
                sun.transform.Rotate(135.0f, 0.0f, 0.0f, Space.Self);
                EmissionMaterial.SetColor("_EmissiveColor", LigthEmsvColor * emissiveIntensityNight);
                EmissionMaterial.DisableKeyword("_EMISSION");
                sunLight.colorTemperature = 3000f;
                RenderSettings.skybox = skyboxes[(int)time - 1];
                ambientLightController.Lamps(false);
                break;
            case 4:
                sun.transform.rotation = Quaternion.identity;
                //night
                sunLight.intensity = 0.5f;
                sun.transform.Rotate(60.0f, 0.0f, 0.0f, Space.Self);
                sunLight.colorTemperature = 20000f;
                RenderSettings.skybox = skyboxes[(int)time - 1];
                EmissionMaterial.SetColor("_EmissiveColor", LigthEmsvColor * emissiveIntensityNight);
                EmissionMaterial.EnableKeyword("_EMISSION");
                ambientLightController.Lamps(true);
                break;
        }
    }
}
