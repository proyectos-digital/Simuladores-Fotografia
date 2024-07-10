using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PanelController : MonoBehaviour{

    [SerializeField] private GameObject panelGeneral;
    [SerializeField] private GameObject panelCamProp;
    [SerializeField] private bool activatePanelCam;
    [SerializeField] private CameraAnimations cameraAnimations;
    [SerializeField] private CameraManager camManager;
    bool isOpenPanel = true;
    [SerializeField] public Volume volume;
    //Booleano para configurar en escenas que sean de estudio
    [SerializeField] bool isStudy = false;
    [SerializeField] bool isTV = false;

    private void Awake() {
        //Si es escena de estudio donde se usa la camara del escenario y no Player
        //Se suscribe el evento correspondiente
        if (isStudy && !isTV) {
            camManager = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManager>();
            camManager.panelStudy += OpenPanel;
        } else if(!isTV){
            cameraAnimations.openPanel += OpenPanel;
        }
    }

    void Start(){
        //panelCamProp.SetActive(activatePanelCam);
        OpenPanel();
    }

    void OpenPanel(){
        isOpenPanel = !isOpenPanel;
        panelGeneral.SetActive(isOpenPanel);
        volume.enabled = isOpenPanel;
        Cursor.lockState = CursorLockMode.None;
    }
}
