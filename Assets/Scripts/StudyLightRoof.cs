using UnityEngine;
using UnityEngine.UI;

public class StudyLightRoof : MonoBehaviour
{
    bool active = true; // Indica si la luz está activa
    Light lightObj; // Referencia al componente de luz
    [SerializeField] Material materialOff; // Material para cuando la luz está apagada
    [SerializeField] Material materialOn; // Material para cuando la luz está encendida

    void Start()
    {
        lightObj = GetComponent<Light>(); // Obtiene el componente de luz
    }

    // Función para cambiar el estado de la luz basado en el estado de un Toggle
    public void ChangeState(Toggle toggle)
    {
        active = toggle.isOn; // Actualiza el estado activo basado en el Toggle
        lightObj.enabled = active; // Habilita o deshabilita la luz
        lightObj.GetComponentInChildren<Renderer>().material = active ? materialOn : materialOff; // Cambia el material del renderizador
    }
}
