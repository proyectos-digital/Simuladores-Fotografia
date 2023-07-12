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
    //public GameObject camFirst;
    //Paneles
    public GameObject panelUI, panelDepth, panelMotion;
    //Sliders Prop Camera
    public Slider sliderFoV; //sliderFoV, sliderNear, sliderFar, sliderSensorX, sliderSensorY, 
    //Sliders Efectos
    public Slider sliderVignette;
    public Slider sliderDepthFocusDistance, sliderDepthFocalLength, sliderDepthAperture;
    public Slider sliderMotionIntensity, sliderMotionClamp;
    public TMP_Dropdown dropdown;
    public TMP_Text txtItem;
    public Toggle tglDepth, tglMotion;
    //Valores iniciales de la camara Foto
    float foV, near, far, sensorSizeX, sensorSizeY, focalLength, vigneteValue;

    public Volume volume;
    //Depth of Field o Efecto Bokeh
    private DepthOfField depth;
    private LensDistortion lens = null;
    private Vignette vignette = null;
    private MotionBlur motion = null;

    //fov max 179
    //Lentes
    public float lenteNormal = 60f;// = new float[] { 60f, 40f, 24f,20.7f };
    public float lenteAngular = 20.4f;// = new float[] { 20.4f, 10.26f, 7.49f, 20.7f };
    public float lenteTeleObjetivo = 101f;// = new float[] { 101, 70, 51, 20.47f };
    public float lenteSuperTele = 150.64f;// = new float[] { 101, 70, 51, 20.47f };


    void Start() {
        
        panelUI.SetActive(false);
        //camFirst.SetActive(false);
        //camThird.SetActive(true);
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out lens);
        volume.profile.TryGet(out depth);
        volume.profile.TryGet(out motion);
        //vignette.active = true;
        //foV = cameraPhoto.fieldOfView; sliderFoV.value = foV;
        //near = cameraPhoto.nearClipPlane; sliderNear.value = near;
        //far = cameraPhoto.farClipPlane; sliderFar.value = far;
        //sensorSizeX = cameraPhoto.sensorSize.x; sliderSensorX.value = sensorSizeX;
        //sensorSizeY = cameraPhoto.sensorSize.y; sliderSensorY.value = sensorSizeY;
        //focalLength = cameraPhoto.focalLength; sliderFocalLength.value = focalLength;
        //vigneteValue = vignette.intensity.value; sliderVignette.value = vigneteValue;

        sliderFoV.onValueChanged.AddListener(v =>
        {
            cameraPhoto.fieldOfView = v;
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
        tglDepth.onValueChanged.AddListener(delegate {
            ToggleValueChanged(tglDepth);
        });

        tglMotion.onValueChanged.AddListener(delegate {
            ToggleMotionChanged(tglMotion);
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

    void DropDownItemSelected(TMP_Dropdown dropdown){
        int index = dropdown.value;
        txtItem.text = "Lente seleccionado: " + dropdown.options[index].text;
        //Usar solo Fov y focalLength
        switch (index) {
            case 0:
                cameraPhoto.fieldOfView = lenteNormal;
                sliderFoV.value = cameraPhoto.fieldOfView;
                //cameraPhoto.sensorSize.Set(lenteNormal[1], lenteNormal[2]);
                //cameraPhoto.focalLength = lenteNormal[3];
                break;
            case 1:
                cameraPhoto.fieldOfView = lenteAngular;
                sliderFoV.value = cameraPhoto.fieldOfView;
                //cameraPhoto.sensorSize.Set(lenteAngular[1], lenteAngular[2]);
                //cameraPhoto.focalLength = lenteAngular[3];
            break;
            case 2:
                cameraPhoto.fieldOfView = lenteTeleObjetivo;
                sliderFoV.value = cameraPhoto.fieldOfView;
                //cameraPhoto.sensorSize.Set(lenteTeleObjetivo[1], lenteTeleObjetivo[2]);
                //cameraPhoto.focalLength = lenteTeleObjetivo[3];
                break;
            case 3:
                cameraPhoto.fieldOfView = lenteSuperTele;
                sliderFoV.value = cameraPhoto.fieldOfView;
                //cameraPhoto.sensorSize.Set(lenteSuperTele[1], lenteSuperTele[2]);
                //cameraPhoto.focalLength = lenteSuperTele[3];
                break;
        }
        //float floatValue = float.Parse(strFloatValue, CultureInfo.InvariantCulture.NumberFormat);
    }

    void Update(){
        if (Input.GetKeyUp(KeyCode.V)){
            vignette.active = !vignette.active;
        }
        if(Input.GetKeyUp(KeyCode.F)){
            lens.active = !lens.active;
        }
        if (Input.GetKeyUp(KeyCode.P)){
            //Mostrar Panel, bloquear movimiento mouse y ya
            Cursor.visible = !Cursor.visible;
            if (Cursor.visible){
                Cursor.lockState = CursorLockMode.None;
            }else{
                Cursor.lockState = CursorLockMode.Locked;
            }
            panelUI.SetActive(!panelUI.activeSelf);
            cameraPhoto.GetComponentInChildren<PlayerCam>().enabled = !cameraPhoto.GetComponentInChildren<PlayerCam>().enabled;
        }
    }
}
