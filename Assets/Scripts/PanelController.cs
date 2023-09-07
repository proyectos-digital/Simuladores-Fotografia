using UnityEngine;

public class PanelController : MonoBehaviour{

    [SerializeField] private GameObject panelGeneral;
    [SerializeField] private GameObject panelDay;
    [SerializeField] private GameObject panelCamProp;
    [SerializeField] private bool activatePanelDay;
    [SerializeField] private bool activatePanelCam;
    [SerializeField] private CameraAnimations cameraAnimations;
    [SerializeField] private CameraManager camManager;
    bool isOpenPanel = true;
    //Booleano para configurar en escenas que sean de estudio
    [SerializeField] bool isStudy = false;

    private void Awake() {
        //Si es escena de estudio donde se usa la camara del escenario y no Player
        //Se suscribe el evento correspondiente
        if (isStudy) {
            camManager = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManager>();
            camManager.panelStudy += OpenPanel;
        } else {
            cameraAnimations.openPanel += OpenPanel;
        }
    }

    void Start(){
        panelDay.SetActive(activatePanelDay);
        panelCamProp.SetActive(activatePanelCam);
        OpenPanel();
    }

    void OpenPanel(){
        isOpenPanel = !isOpenPanel;
        panelGeneral.SetActive(isOpenPanel);
    }
}
