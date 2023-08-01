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
    public Camera cameraPhoto;
    //Paneles
    [Header("Paneles")]
    public GameObject panelUI;
    public GameObject panelDepth;
    public GameObject panelMotion;
    public GameObject panelColor;
    
    //Sliders Prop Camera Zoom
    public Slider sliderFoV; //sliderFoV, sliderNear, sliderFar, sliderSensorX, sliderSensorY, 
    //Sliders Efectos
    public Slider sliderVignette;
    //Enfoque
    [Header("Panel Depth")]
    public Slider sliderDepthFocusDistance;
    public Slider sliderDepthFocalLength;
    public Slider sliderDepthAperture;

    //Motion Blur
    [Header("Panel Motion")]
    public Slider sliderMotionIntensity;
    public Slider sliderMotionClamp;

    //Color
    [Header("Panel Color")]
    public Slider sliderExposure;
    public Slider sliderContrast;
    public Slider sliderHue;
    public Slider sliderSaturation;

    public TMP_Dropdown dropdown;
    public TMP_Text textLente;

    //Activadores efectos y flash
    [Header("Toggles")]
    public Toggle tglDepth;
    public Toggle tglMotion;
    public Toggle tglColor;
    public Toggle tglFlash;
    //Valores iniciales de la camara Foto
    float foV, near, far, sensorSizeX, sensorSizeY, focalLength, vigneteValue;        
    bool isOpenPanel = false;

    public Volume volume;
    //Efecto Bokeh
    private DepthOfField depth;
    //Ojo de Pez
    private LensDistortion lens = null;
    //Viñeta
    private Vignette vignette = null;
    //Motion Blur
    private MotionBlur motion = null;
    //Ajuste de color
    private ColorAdjustments colorAdjustments = null;

    public Screenshot screenshot;

    //Lentes
    [Header("Distancia Lentes")]
    public float lenteNormal = 60f;// = new float[] { 60f, 40f, 24f,20.7f };
    public float lenteAngular = 20.4f;// = new float[] { 20.4f, 10.26f, 7.49f, 20.7f };
    public float lenteTeleObjetivo = 101f;// = new float[] { 101, 70, 51, 20.47f };
    public float lenteSuperTele = 120f;// = new float[] { 101, 70, 51, 20.47f };


    void Start() {
        
        panelUI.SetActive(false);
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out lens);
        volume.profile.TryGet(out depth);
        volume.profile.TryGet(out motion);
        volume.profile.TryGet(out colorAdjustments);
        foV = cameraPhoto.fieldOfView; sliderFoV.value = foV;

        sliderFoV.onValueChanged.AddListener(v =>{
            cameraPhoto.fieldOfView = v;
            if (v > 110) {
                //Debug.Log("Lente Gran Angular");
                textLente.text = "Lente Gran Angular";
            }
            if (v > 80 && v < 110) {
                //Debug.Log("Lente Angular");
                textLente.text = "Lente Angular";
            }
            if (v > 40 && v < 80) {
                //Debug.Log("Lente Normal");
                textLente.text = "Lente Normal";
            }
            if(v < 40) {
                //Debug.Log("Lente TeleObjetivo");
                textLente.text = "Lente TeleObjetivo";
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
        sliderHue.onValueChanged.AddListener(v => {
            colorAdjustments.hueShift.value = v;
        });
        sliderSaturation.onValueChanged.AddListener(v => {
            colorAdjustments.saturation.value = v;
        });
        tglDepth.onValueChanged.AddListener(delegate {
            ToggleValueChanged(tglDepth);
        });

        tglMotion.onValueChanged.AddListener(delegate {
            ToggleMotionChanged(tglMotion);
        });
        tglFlash.onValueChanged.AddListener(delegate {
            ToggleFlash(tglFlash);
        });
        tglColor.onValueChanged.AddListener(delegate {
            ToggleColorChanged(tglColor);
        });

        DropDownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate { DropDownItemSelected(dropdown); });

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ToggleValueChanged(Toggle toggle){
        depth.active = toggle.isOn;
        panelDepth.SetActive(toggle.isOn);
        //crear panel para sliders de propiedades Depth
    }
    private void ToggleMotionChanged(Toggle toggle){
        motion.active = toggle.isOn;
        panelMotion.SetActive(toggle.isOn);
        //crear panel para sliders de propiedades Depth
    }
    private void ToggleColorChanged(Toggle toggle) {
        colorAdjustments.active = toggle.isOn;
        panelColor.SetActive(toggle.isOn);
        //crear panel para sliders de propiedades Depth
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

    public void ResetCamera() {
        cameraPhoto.fieldOfView = foV;
        sliderFoV.value = foV;
        vignette.active = false;
        lens.active = false;
        dropdown.value = 0;
        colorAdjustments.active = false;
        colorAdjustments.postExposure.value = 0;
        colorAdjustments.contrast.value = 0;
        colorAdjustments.hueShift.value = 0;
        colorAdjustments.saturation.value = 0;
        sliderExposure.value = colorAdjustments.postExposure.value;
        sliderContrast.value = colorAdjustments.contrast.value;
        sliderHue.value = colorAdjustments.hueShift.value;
        sliderSaturation.value = colorAdjustments.saturation.value;
    }

    void DropDownItemSelected(TMP_Dropdown dropdown){
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
    }

    void Update(){
        //if(Input.GetKeyUp(KeyCode.F)){
        //    lens.active = !lens.active;
        //}
        if (Input.GetKeyUp(KeyCode.P)){
            isOpenPanel = !isOpenPanel;
            //Mostrar Panel, bloquear movimiento mouse y ya
            Cursor.visible = !Cursor.visible;
            if (Cursor.visible){
                Cursor.lockState = CursorLockMode.None;
            }else{
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (!isOpenPanel) {
                ResetCamera();
            }
            panelUI.SetActive(!panelUI.activeSelf);
            cameraPhoto.GetComponentInChildren<PlayerCam>().enabled = !cameraPhoto.GetComponentInChildren<PlayerCam>().enabled;
        }
    }
}
