using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudyCamElement : MonoBehaviour
{
    bool active = false;
    [SerializeField] GameObject panelInfo;
    [SerializeField] CameraManager camManager;
    [SerializeField] GameObject camPosStudy;
    [SerializeField] Slider sldMoveCamera;
    TMP_Text txtInfo;
    
    void Start() {
        txtInfo = panelInfo.GetComponentInChildren<TMP_Text>();
        camManager = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManager>();
        sldMoveCamera.onValueChanged.AddListener(v => {
            camPosStudy.transform.localPosition = new Vector3(camPosStudy.transform.localPosition.x, camPosStudy.transform.localPosition.y, v);
        });
    }
    void Update()
    {
        if (active && Input.GetKeyUp(KeyCode.E)) {
            camManager.PanelCamStudy();
            if (!camManager.isMenu) {
                panelInfo.SetActive(!panelInfo.activeSelf);
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            txtInfo.text = "Presiona E para Modo Cámara.";
            panelInfo.SetActive(true);
            active = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            active = false;
            txtInfo.text = "";
            panelInfo.SetActive(false);
        }
    }
}
