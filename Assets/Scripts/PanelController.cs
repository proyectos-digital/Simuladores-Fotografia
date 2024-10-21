using UnityEngine;
using UnityEngine.Rendering;

public class PanelController : MonoBehaviour{

    [SerializeField] private GameObject panelGeneral;
    [SerializeField] private bool activatePanelCam;
    [SerializeField] private CameraAnimations cameraAnimations;
    [SerializeField] private CameraManager camManager;
    bool isOpenPanel = true;
    [SerializeField] public Volume volume;
    //Booleano para configurar en escenas que sean de estudio
    [SerializeField] bool isStudy = false;
    //Booleano para configurar en escenas que son de simuladores Tv
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
    //Ejecutamos la función al iniciar el simulador
    void Start(){
        OpenPanel();
    }

    //Función para abrir el panel del modo cámara
    void OpenPanel(){
        isOpenPanel = !isOpenPanel;
        panelGeneral.SetActive(isOpenPanel);
        volume.enabled = isOpenPanel;
        Cursor.lockState = CursorLockMode.None;
    }
}
