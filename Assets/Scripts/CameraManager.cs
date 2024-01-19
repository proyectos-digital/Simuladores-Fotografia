using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using System;

public class CameraManager : MonoBehaviour
{
    [SerializeField] bool camHand = true;
    public bool isMenu = false;
    public Camera cameraPhoto;
    public Transform camObj, camPosOrig, camPosStudy;


    //Paneles
    [Header("Paneles")]
   
    public GameObject panelColor;
    public GameObject panelMenu;

    [Header("Buttons")]
    public Button isoButton;
    public Button apertureButton;
    public Button shutterSpeedButton;
    public Button exposureButton;
    public Button focalLengthButton;

    [Header("Other Settings")]
    public Volume volume;
    public TMP_Text txtLens;
    //Ojo de Pez
    private LensDistortion lens = null;
    //Viñeta
    private Vignette vignette = null;
    private ColorAdjustments colorAdjustments = null;
    //Valores iniciales de la camara Foto
    float fovIni, camPhotoValue, sldFov;
    bool isOpenPanel = false, vig, len, tgldepth, tglcolor;
    public Screenshot screenshot;

    //sliders
    [Header("Sliders")]
    public Slider isoSlider;
    public Slider apertureSlider;
    public Slider vignetteSlider;
    public Slider shutterSpeedSlider;
    public Slider exposureSlider;
    public Slider focalLengthSlider;

    public TMP_Text lensInfo;
    private TMP_Text isoText;
    private TMP_Text apertureText;
    private TMP_Text shutterSpeedText;
    private TMP_Text exposureText;
    private TMP_Text focalLengthText;

    //Activadores efectos y flash
    [Header("Toggles")]
    public Toggle tglColor;
    public Toggle tglFlash;
 
    //Crear Delegado y Evento
    public delegate void cameraAnimations(bool isOpen);
    public event cameraAnimations cameraAnimation;
    public delegate void PanelStudy();
    public event PanelStudy panelStudy;


    void Start() {

        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out lens);
        //volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);


        isoSlider.wholeNumbers = true;
        int[] isoValues = { 100, 200, 400, 800, 1600, 3200, 6400 };
        float[] apertureValues = { 1.4f, 2f, 2.8f, 4f, 5.6f, 8f, 11f, 13f, 16f, 22f };
        //float[] shutterSpeedValues = { 2f, 0.25f, 0.125f, 0.0666666666666667f, 0.0333333333333333f, 0.0166666666666667f, 0.008f, 0.004f, 0.002f, 0.001f };
        float[] shutterSpeedValues = { 2f, 4f, 8f, 15f, 30f, 60f, 125f, 250f, 500f, 1000f };
        float[] FocalLengthValues = { 14f, 35f, 50f, 200f, 400f };
        string[] FocalLegthTexts = { "Ultra Angular 14MM", "Gran Angular 35MM", "Distancia Media 50MM", "Teleobjetivo 200MM", "Super Teleobjetivo 400MM" };

        isoSlider.onValueChanged.AddListener(i => {
            cameraPhoto.iso = isoValues[(int)i - 1];
            isoText = isoButton.GetComponentInChildren<TMP_Text>();
            isoText.text = isoValues[(int)i - 1].ToString();
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
            Debug.Log(realshutterspeedvalue);
            cameraPhoto.shutterSpeed = realshutterspeedvalue;
            shutterSpeedText = shutterSpeedButton.GetComponentInChildren<TMP_Text>();
            shutterSpeedText.text = "1/" + shutterSpeedValues[(int)ss - 1].ToString();
        });

        /*tglFlash.onValueChanged.AddListener(delegate {
            ToggleFlash(tglFlash);
        });*/

        exposureSlider.onValueChanged.AddListener(v => {
            colorAdjustments.postExposure.value = v;
            exposureText = exposureButton.GetComponentInChildren<TMP_Text>();
            exposureText.text = "± " + v.ToString();
            //exposureText.text = "± " + v.ToString();
            //Debug.Log(colorAdjustments);
        });

        
        
        SaveCamera();

      

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ActiveSlider(Slider slider){
        if (slider.gameObject.activeSelf)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
        }
    }

    

    private void ToggleColorChanged(Toggle toggle) {
        //colorAdjustments.active = toggle.isOn;
        //panelColor.SetActive(toggle.isOn);
    }

    private void ToggleFlash(Toggle toggle) {
        screenshot.FlashOn(toggle);
        toggle.GetComponentInChildren<Text>().text = toggle.isOn ? "Flash On" : "Flash Off";
    }

    public void OnOffVignette() {
        vignette.active = !vignette.active;
    }

    public void OnOffEyeFish() {
        lens.active = !lens.active;
    }


    //Guardar ajustes de la camara
    void SaveCamera() {
        //vig = vignette.active;
        //len = lens.active;
    }

    //Cargar ajustes Camara
    public void LoadCamera() {
        //lens.active = len;
        //vignette.active = vig;
    }

    void LoadCameraStudy() {
        cameraPhoto.fieldOfView = 30;
    }

    public void ResetCamera() {
        SaveCamera();
        vignette.active = false;
        //lens.active = false;
    }

    void Update(){
        if ((camHand && !isMenu)&& Input.GetKeyUp(KeyCode.P)){
            PanelAction(true);
        }
        //Menu de luces en escena Estudio
        if ((!camHand && !isOpenPanel)&& Input.GetKeyUp(KeyCode.M)) {
            isMenu = !isMenu;
            panelMenu.SetActive(isMenu);
            Cursor.visible = isMenu;
            Cursor.lockState = isMenu ? CursorLockMode.None : CursorLockMode.Locked;
            cameraPhoto.GetComponentInChildren<PlayerCam>().enabled = !isMenu;
        }
    }
    public void PanelCamStudy() {
        if(!isMenu)
            PanelAction(false);
    }

    void PanelAction(bool animate) {
        isOpenPanel = !isOpenPanel;
        //Mostrar Panel, bloquear movimiento mouse y ya
        Cursor.visible = isOpenPanel;
        Cursor.lockState = isOpenPanel ? CursorLockMode.None : CursorLockMode.Locked;
        if (animate) {
            cameraAnimation(isOpenPanel);
        } else {
            panelStudy();
            //playerMov.gameObject.SetActive(!playerMov.gameObject.activeSelf);
            //playerMov.enabled = !playerMov.enabled;
            if (isOpenPanel) {
                //camObj.position = camPosStudy.position;
                cameraPhoto.transform.rotation = camPosStudy.rotation;
                LoadCameraStudy();
            } else {
                //camObj.position = camPosOrig.position;
                cameraPhoto.transform.rotation = camPosOrig.rotation;
                ResetCamera();
            }
            
        }
        cameraPhoto.GetComponentInChildren<PlayerCam>().enabled = !cameraPhoto.GetComponentInChildren<PlayerCam>().enabled;
    }
}
