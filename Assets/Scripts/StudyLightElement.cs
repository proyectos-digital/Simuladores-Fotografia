using TMPro;
using UnityEngine;

public class StudyLightElement : MonoBehaviour
{
    bool active = false; // Indica si el elemento de luz está activo
    Light lightObj; // Referencia al componente de luz
    [SerializeField] GameObject panelInfo; // Panel de información
    [SerializeField] Material materialOff; // Material para cuando la luz está apagada
    [SerializeField] Material materialOn; // Material para cuando la luz está encendida
    TMP_Text txtInfo; // Texto de información

    void Start()
    {
        lightObj = GetComponent<Light>(); // Obtiene el componente de luz
        txtInfo = panelInfo.GetComponentInChildren<TMP_Text>(); // Obtiene el texto del panel de información
    }

    void Update()
    {
        // Si el elemento está activo y se presiona la tecla Q, alterna el estado de la luz
        if (active && Input.GetKeyUp(KeyCode.Q))
        {
            lightObj.enabled = !lightObj.isActiveAndEnabled;
            lightObj.GetComponentInChildren<Renderer>().material = lightObj.enabled ? materialOn : materialOff;
            txtInfo.text = lightObj.enabled ? "Presiona Q para apagar la Luz." : "Presiona Q para encender Luz.";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cuando el jugador entra en el trigger, muestra el panel de información
        if (other.tag == "Player")
        {
            active = true;
            panelInfo.SetActive(true);
            txtInfo.text = lightObj.enabled ? "Presiona Q para apagar la Luz." : "Presiona Q para encender Luz.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cuando el jugador sale del trigger, oculta el panel de información
        if (other.tag == "Player")
        {
            active = false;
            txtInfo.text = "";
            panelInfo.SetActive(false);
        }
    }
}
