using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudyCamElement : MonoBehaviour
{
    bool active = false; // Indica si el modo cámara está activo
    [SerializeField] GameObject panelInfo; // Panel de información
    [SerializeField] CameraManager camManager; // Referencia al gestor de la cámara
    [SerializeField] GameObject camPosStudy; // Posición de la cámara de estudio
    [SerializeField] Slider sldMoveCamera; // Slider para mover la cámara
    TMP_Text txtInfo; // Texto de información

    void Start()
    {
        txtInfo = panelInfo.GetComponentInChildren<TMP_Text>();
        camManager = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManager>();
        sldMoveCamera.onValueChanged.AddListener(v => {
            camPosStudy.transform.localPosition = new Vector3(camPosStudy.transform.localPosition.x, camPosStudy.transform.localPosition.y, v);
        });
    }

    void Update()
    {
        // Activa el modo de estudio de cámara cuando se presiona E y el objeto está activo
        if (active && Input.GetKeyUp(KeyCode.E))
        {
            camManager.PanelCamStudy();
            if (!camManager.isMenu)
            {
                panelInfo.SetActive(!panelInfo.activeSelf);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cuando el jugador entra en el trigger, muestra el panel de información
        if (other.tag == "Player")
        {
            txtInfo.text = "Presiona E para Modo Cámara.";
            panelInfo.SetActive(true);
            active = true;
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
