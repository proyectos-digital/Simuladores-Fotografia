using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using System;

public class CameraManager : MonoBehaviour
{
    public bool camHand = true;
    public Camera cameraPhoto;
    public Transform camObj, camPosOrig, camPosStudy;
    
    //Paneles
    [Header("Paneles")]
    public GameObject panelDepth;
    //public GameObject panelMotion;
    public GameObject panelColor;
    
    //Sliders Propiedades Camera Zoom
    public Slider sliderFoV; //sliderNear, sliderFar, sliderSensorX, sliderSensorY, 
    //Sliders Efectos
    public Slider sliderVignette;
    //Enfoque
    [Header("Panel Depth")]
    public Slider sliderDepthFocusDistance;
    public Slider sliderDepthFocalLength;
    public Slider sliderDepthAperture;

    //Motion Blur
    //[Header("Panel Motion")]
    //public Slider sliderMotionIntensity;
    //public Slider sliderMotionClamp;

    //Color
    [Header("Panel Color")]
    public Slider sliderExposure;
    public Slider sliderContrast;
    public Slider sliderHue;
    public Slider sliderSaturation;

    //Obsoleto
    //public TMP_Dropdown dropdown;
    public TMP_Text txtLens;

    //Activadores efectos y flash
    [Header("Toggles")]
    public Toggle tglDepth;
    public Toggle tglMotion;
    public Toggle tglColor;
    public Toggle tglFlash;
    //Valores iniciales de la camara Foto
    float fovIni, camPhotoValue, sldFov;        
    bool isOpenPanel = false, vig, len, tgldepth, tglcolor;

    public Volume volume;
    //Efecto Bokeh o Enfoque
    private DepthOfField depth;
    //Ojo de Pez
    private LensDistortion lens = null;
    //Viñeta
    private Vignette vignette = null;
    //Motion Blur Sin usar
    private MotionBlur motion = null;
    //Ajuste de color
    private ColorAdjustments colorAdjustments = null;

    public Screenshot screenshot;

    //Crear Delegado y Evento
    public delegate void cameraAnimations(bool isOpen);
    public event cameraAnimations cameraAnimation;
    public delegate void PanelStudy();
    public event PanelStudy panelStudy;

    //Lentes Obsoleto
    /*[Header("Distancia Lentes")]
    public float lenteNormal = 60f;// = new float[] { 60f, 40f, 24f,20.7f };
    public float lenteAngular = 20.4f;// = new float[] { 20.4f, 10.26f, 7.49f, 20.7f };
    public float lenteTeleObjetivo = 101f;// = new float[] { 101, 70, 51, 20.47f };
    public float lenteSuperTele = 120f;// = new float[] { 101, 70, 51, 20.47f };
    */

    void Start() {
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out lens);
        volume.profile.TryGet(out depth);
        volume.profile.TryGet(out motion);
        volume.profile.TryGet(out colorAdjustments);
        fovIni = cameraPhoto.fieldOfView; 
        sliderFoV.value = fovIni;
        SaveCamera();

        sliderFoV.onValueChanged.AddListener(v =>{
            cameraPhoto.fieldOfView = v;
            if (v > 110) {
                txtLens.text = "Lente Gran Angular";
            }
            if (v > 80 && v < 110) {
                txtLens.text = "Lente Angular";
            }
            if (v > 40 && v < 80) {
                txtLens.text = "Lente Normal";
            }
            if(v > 20 && v < 40) {
                txtLens.text = "Lente TeleObjetivo";
            }
            if (v < 20) {
                txtLens.text = "Lente Super TeleObjetivo";
            }
        });
        sliderVignette.onValueChanged.AddListener(v =>{
            vignette.intensity.value = v;
        });
        sliderDepthFocusDistance.onValueChanged.AddListener(v => {
            depth.focusDistance.value = v;
        });
        sliderDepthFocalLength.onValueChanged.AddListener(v =>{
            depth.focalLength.value = v;
        });
        sliderDepthAperture.onValueChanged.AddListener(v => {
            depth.aperture.value = v;
        });
        sliderExposure.onValueChanged.AddListener(v => {
            colorAdjustments.postExposure.value = v;
        });
        sliderContrast.onValueChanged.AddListener(v => {
            colorAdjustments.contrast.value = v;
        });
        //sliderHue.onValueChanged.AddListener(v => {
        //    colorAdjustments.hueShift.value = v;
        //});
        sliderSaturation.onValueChanged.AddListener(v => {
            colorAdjustments.saturation.value = v;
        });
        tglDepth.onValueChanged.AddListener(delegate {
            ToggleValueChanged(tglDepth);
        });

        //tglMotion.onValueChanged.AddListener(delegate {
        //    ToggleMotionChanged(tglMotion);
        //});
        tglFlash.onValueChanged.AddListener(delegate {
            ToggleFlash(tglFlash);
        });
        tglColor.onValueChanged.AddListener(delegate {
            ToggleColorChanged(tglColor);
        });

        //DropDownItemSelected(dropdown);
        //dropdown.onValueChanged.AddListener(delegate { DropDownItemSelected(dropdown); });

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void ToggleValueChanged(Toggle toggle){
        depth.active = toggle.isOn;
        panelDepth.SetActive(toggle.isOn);
    }
    //private void ToggleMotionChanged(Toggle toggle){
    //    motion.active = toggle.isOn;
    //    panelMotion.SetActive(toggle.isOn);
    //    //crear panel para sliders de propiedades Depth
    //}
    private void ToggleColorChanged(Toggle toggle) {
        colorAdjustments.active = toggle.isOn;
        panelColor.SetActive(toggle.isOn);
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
        camPhotoValue = cameraPhoto.fieldOfView;
        sldFov = sliderFoV.value;
        vig = vignette.active;
        len = lens.active;
        tgldepth = tglDepth.isOn;
        tglcolor = tglColor.isOn;
    }
    //Cargar ajustes Camara
    public void LoadCamera() {
        cameraPhoto.fieldOfView = camPhotoValue;
        sliderFoV.value =sldFov;
        vignette.active = vig;
        lens.active = len;
        tglDepth.isOn = tgldepth;
        tglColor.isOn = tglcolor;
    }
    void LoadCameraStudy() {
        cameraPhoto.fieldOfView = 24;
        sliderFoV.value = 24;
    }
    public void ResetCamera() {
        SaveCamera();
        sliderFoV.value = fovIni;
        cameraPhoto.fieldOfView = fovIni;
        vignette.active = false;
        lens.active = false;
        tglDepth.isOn = false;
        tglColor.isOn = false;
    }
    //Obsoleto Select de lente SIN USO
    /*void DropDownItemSelected(TMP_Dropdown dropdown){
        int index = dropdown.value;
        //Usar solo Fov y focalLength
        switch (index) {
            case 0:
                sliderFoV.minValue = 10;
                sliderFoV.maxValue = 127;
                cameraPhoto.fieldOfView = lenteNormal;
                sliderFoV.value = cameraPhoto.fieldOfView;
                //cameraPhoto.sensorSize.Set(lenteNormal[1], lenteNormal[2]);
                //cameraPhoto.focalLength = lenteNormal[3];
                break;
            case 1:
                sliderFoV.minValue = 10;
                sliderFoV.maxValue = 30;
                cameraPhoto.fieldOfView = lenteAngular;
                sliderFoV.value = cameraPhoto.fieldOfView;
                //cameraPhoto.sensorSize.Set(lenteAngular[                                                                                           1], lenteAngular[2]);
                //cameraPhoto.focalLength = lenteAngular[3];
            break;
            case 2:
                sliderFoV.minValue = 70;
                sliderFoV.maxValue = 110;
                cameraPhoto.fieldOfView = lenteTeleObjetivo;
                sliderFoV.value = cameraPhoto.fieldOfView;
                //cameraPhoto.sensorSize.Set(lenteTeleObjetivo[1], lenteTeleObjetivo[2]);
                //cameraPhoto.focalLength = lenteTeleObjetivo[3];
                break;
            case 3:
                sliderFoV.minValue = 110;
                sliderFoV.maxValue = 127;
                cameraPhoto.fieldOfView = lenteSuperTele;
                sliderFoV.value = cameraPhoto.fieldOfView;
                //cameraPhoto.sensorSize.Set(lenteSuperTele[1], lenteSuperTele[2]);
                //cameraPhoto.focalLength = lenteSuperTele[3];
                break;
        }
        //float floatValue = float.Parse(strFloatValue, CultureInfo.InvariantCulture.NumberFormat);
    }*/

    void Update(){
        if (camHand && Input.GetKeyUp(KeyCode.P)){
            PanelAction(true);
        }
    }
    public void PanelCamStudy() {
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
