using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System.Diagnostics;
using UnityEngine.UI;
using TMPro;
using System;

public class CameraManager : MonoBehaviour
{
    [SerializeField] bool camHand = true;
    public bool isMenu = false;
    public Camera cameraPhoto;
    public Transform camObj, camPosOrig, camPosStudy;
    [SerializeField] private Slider[] sliders;

    //Paneles
    [Header("Paneles")]
    public GameObject panelMenu;
    public GameObject dayControlPanel;

    [Header("Buttons")]
    public Button isoButton;
    public Button apertureButton;
    public Button shutterSpeedButton;
    public Button exposureButton;
    public Button focalLengthButton;

    [Header("Other Settings")]
    public Volume volume;
    public TMP_Text txtLens;
    private LensDistortion lens = null;
    private ColorAdjustments colorAdjustments = null;
    private FilmGrain filmGrain = null;
    bool isOpenPanel = false;
    public Screenshot screenshot;
    public NotificationController nc;

    //sliders
    [Header("Sliders")]
    public Slider isoSlider;
    public Slider apertureSlider;
    public Slider shutterSpeedSlider;
    public Slider exposureSlider;
    public Slider focalLengthSlider;
    public Slider focusDistanceSlider;

    public TMP_Text lensInfo;
    private TMP_Text isoText;
    private TMP_Text apertureText;
    private TMP_Text shutterSpeedText;
    private TMP_Text exposureText;
    private TMP_Text focalLengthText;
    private string notificationText;

    //Activadores efectos y flash
    [Header("Toggles")]
    public Toggle tglFlash;
 
    //Crear Delegado y Evento
    public delegate void cameraAnimations(bool isOpen);
    public event cameraAnimations cameraAnimation;
    public delegate void PanelStudy();
    public event PanelStudy panelStudy;


    void Start() {
        //Obtenemos los componentes de Volume y los almacenamos en variables
        volume.profile.TryGet<LensDistortion>(out lens);
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        volume.profile.TryGet<FilmGrain>(out filmGrain);

        //Asignamos los valores que tendran cada cada parámetro de la cámara
        isoSlider.wholeNumbers = true;
        int[] isoValues = { 100, 200, 400, 800, 1600, 3200, 6400 };
        float[] FGValues = { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.7f, 0.9f };
        float[] apertureValues = { 1.4f, 2f, 2.8f, 4f, 5.6f, 8f, 11f, 13f, 16f, 22f };
        float[] shutterSpeedValues = { 2f, 4f, 8f, 15f, 30f, 60f, 125f, 250f, 500f, 1000f };
        float[] FocalLengthValues = { 14f, 35f, 50f, 200f, 400f };
        string[] FocalLegthTexts = { "Ultra Angular 14MM", "Gran Angular 35MM", "Distancia Media 50MM", "Teleobjetivo 200MM", "Super Teleobjetivo 400MM" };

        //Asignamos que tendra los sliders según interactuemos para cada Slider correspondiente con sus variables
        isoSlider.onValueChanged.AddListener(i => {
            cameraPhoto.iso = isoValues[(int)i - 1];
            isoText = isoButton.GetComponentInChildren<TMP_Text>();
            filmGrain.intensity.value = FGValues[(int)i - 1];
            isoText.text = isoValues[(int)i - 1].ToString();
        });

        focusDistanceSlider.onValueChanged.AddListener(fd => {
            cameraPhoto.focusDistance = focusDistanceSlider.value;
        });

        focalLengthSlider.onValueChanged.AddListener(fl => {
            cameraPhoto.focalLength = FocalLengthValues[(int)fl - 1];
            focalLengthText = focalLengthButton.GetComponentInChildren<TMP_Text>();
            lensInfo.text = FocalLegthTexts[(int)fl - 1];
        });

        apertureSlider.onValueChanged.AddListener(a => {
            cameraPhoto.aperture = apertureValues[(int)a - 1];
            apertureText = apertureButton.GetComponentInChildren<TMP_Text>();
            apertureText.text = apertureValues[(int)a - 1].ToString();
        });

        shutterSpeedSlider.onValueChanged.AddListener(ss => {
            float realshutterspeedvalue = 1 / shutterSpeedValues[(int)ss - 1];
            cameraPhoto.shutterSpeed = realshutterspeedvalue;
            shutterSpeedText = shutterSpeedButton.GetComponentInChildren<TMP_Text>();
            shutterSpeedText.text = "1/" + shutterSpeedValues[(int)ss - 1].ToString();
        });

        exposureSlider.onValueChanged.AddListener(v => {
            colorAdjustments.postExposure.value = v;
            exposureText = exposureButton.GetComponentInChildren<TMP_Text>();
            exposureText.text = "± " + v.ToString();
        });
        //Bloqueamos el mouse para que no se pueda salir de la ventana del simulador
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Función que desactiva todos los sliders de la cámara y despues activa la seleccionada
    public void ActiveSlider(Slider slider){
        bool checkSlider = CheckActiveSlider(sliders);
        if (checkSlider)
        {
            foreach (var s in sliders)
            {
                s.gameObject.SetActive(false);
            }
        }
        if (slider.gameObject.activeSelf)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
        }
    }
    
    // Función que retorna el estado activo de los sliders
    private bool CheckActiveSlider(Slider[] sliderList)
    {
        bool sliderStatus = false;
        foreach (var slider in sliderList)
        {
            if (slider.gameObject.activeSelf) {
                sliderStatus = false;
            }
            else
            {
                sliderStatus = true;
                break;
            }
        }
        return sliderStatus;
    }
    //Función para activar el flash de la cámara
    public void ToggleFlash(Toggle toggle) {
        screenshot.FlashOn(toggle);
    }
    //Función para activar el efecto ojo de pez
    public void OnOffEyeFish() {
        lens.active = !lens.active;
    }
    //Función para activar panel en concreto si no está activo
    public void OnOffPanel(GameObject panel) {
        if (panel.gameObject.activeSelf)
        {
            panel.gameObject.SetActive(false);
        }
        else
        {
            panel.gameObject.SetActive(true);
        }
    }

    //Al entrar en modo camara en escenas Studio y Studio People carga parametros a la cámara
    void LoadCameraStudy() {
        cameraPhoto.fieldOfView = 30;
    }

    //Al salir del modo cámara reinicia los valores de la cámara y desactiva efectos del global volume
    public void ResetCamera() {
        cameraPhoto.focalLength = 23.5f;
        volume.enabled = false;
    }

    void Update(){
        //Entra en modo cámara si no hay menú abierto y se oprime la tecla C
        if ((camHand && !isMenu)&& Input.GetKeyUp(KeyCode.C))
        {
            PanelAction(true);
            volume.enabled = true;
            
        }
        //Menu de luces en escena Estudio y se abre al presionar la tecla M
        if ((!camHand && !isOpenPanel) && Input.GetKeyUp(KeyCode.M)) {
            isMenu = !isMenu;
            panelMenu.SetActive(isMenu);
            cameraPhoto.GetComponentInChildren<PlayerCam>().MouseLocked();
        }

        if ((camHand) && Input.GetKeyUp(KeyCode.X))
        {
                DayPanel();
        }
    }
    //Abre y cierra el panel de día para cambiar el modo de iluminación, activa el mouse
    public void DayPanel()
    {
        isMenu = !isMenu;
        dayControlPanel.SetActive(isMenu);
        cameraPhoto.GetComponentInChildren<PlayerCam>().MouseLocked();// = !cameraPhoto.GetComponentInChildren<PlayerCam>().enabled;
    }

    //Abre el panel de la cámara en escenas Studio y Studio People
    public void PanelCamStudy() {
        if(!isMenu)
            PanelAction(false);
    }

    //Función para el panel de cámara, inicia animación, envia textos al NotificationController
    void PanelAction(bool animate) {
        isOpenPanel = !isOpenPanel;
        if (isOpenPanel)
        {
            notificationText = "Se activo el modo cámara";
            nc.SendNotification(notificationText);
        }
        else
        {
            notificationText = "Se desactivo el modo cámara";
            nc.SendNotification(notificationText);
        }

        //Mostrar Panel, bloquear movimiento mouse
        Cursor.visible = isOpenPanel;
        if (animate) {
            cameraAnimation(isOpenPanel);
        } else {
            panelStudy();
            if (isOpenPanel) {
                cameraPhoto.transform.rotation = camPosStudy.rotation;
                LoadCameraStudy();
            } else {
                cameraPhoto.transform.rotation = camPosOrig.rotation;
                ResetCamera();
            }
        }
        //Se llama a la función MouseLocked en vez de deshabilitar el script...
        //para poder usar el drag del mouse y girar la cámara en modo Cámara
        cameraPhoto.GetComponentInChildren<PlayerCam>().MouseLocked(); // = !cameraPhoto.GetComponentInChildren<PlayerCam>().enabled;
    }

    //Función para abrir la carpeta donde se almacenan las imágenes 
    public void OpenFolder()
    {
        string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Screenshots";
        Process.Start(path);
    }
}
