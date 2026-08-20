using UnityEngine;

public class AmbientLightController : MonoBehaviour {

    [SerializeField] private Light[] Lights;

    //Configura las luces de los postes en los entornos abiertos
    public void Lamps(bool value){
        foreach (Light light in Lights)
        {
            light.gameObject.SetActive(value);
        }
    }
}
