using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOldCode : MonoBehaviour
{
    /*   public GameObject panelDepth;
       public GameObject panelMotion;

       public Slider sliderContrast;
       public Slider sliderSaturation;
       public Slider sliderExposure;

       fovIni = cameraPhoto.fieldOfView;
       sliderFoV.value = fovIni;

       volume.profile.TryGet(out depth);
       volume.profile.TryGet(out motion);

       sliderFoV.onValueChanged.AddListener(v => {
           cameraPhoto.fieldOfView = v;
           if (v > 110) {
           txtLens.text = "Lente Gran Angular";
           }
   if (v > 80 && v < 110)
   {
       txtLens.text = "Lente Angular";
   }
   if (v > 40 && v < 80)
   {
       txtLens.text = "Lente Normal";
   }
   if (v > 20 && v < 40)
   {
       txtLens.text = "Lente TeleObjetivo";
   }
   if (v < 20)
   {
       txtLens.text = "Lente Super TeleObjetivo";
   }
       });
     

   sliderVignette.onValueChanged.AddListener(v =>
   {
       vignette.intensity.value = v;
   });
   sliderDepthFocusDistance.onValueChanged.AddListener(v =>
   {
       depth.focusDistance.value = v;
   });
   sliderDepthFocalLength.onValueChanged.AddListener(v =>
   {
       depth.focalLength.value = v;
   });
   sliderDepthAperture.onValueChanged.AddListener(v =>
   {
       depth.aperture.value = v;
   });

   sliderContrast.onValueChanged.AddListener(v =>
         {
             colorAdjustments.contrast.value = v;
         });
   sliderHue.onValueChanged.AddListener(v =>
   {
       colorAdjustments.hueShift.value = v;
   });
   sliderSaturation.onValueChanged.AddListener(v =>
   {
       colorAdjustments.saturation.value = v;
   });
   tglDepth.onValueChanged.AddListener(delegate
   {
       ToggleValueChanged(tglDepth);
   });

   tglMotion.onValueChanged.AddListener(delegate
   {
       ToggleMotionChanged(tglMotion);
   });

   tglColor.onValueChanged.AddListener(delegate
   {
       ToggleColorChanged(tglColor);
   });

   private void ToggleValueChanged(Toggle toggle)
   {
       depth.active = toggle.isOn;
       panelDepth.SetActive(toggle.isOn);
   }

   private void ToggleMotionChanged(Toggle toggle)
   {
       motion.active = toggle.isOn;
       panelMotion.SetActive(toggle.isOn);
       crear panel para sliders de propiedades Depth
       }

   DropDownItemSelected(dropdown);
   dropdown.onValueChanged.AddListener(delegate
   {
       DropDownItemSelected(dropdown);

       camPhotoValue = cameraPhoto.fieldOfView;
       sldFov = sliderFoV.value;
       tgldepth = tglDepth.isOn;
       tglcolor = tglColor.isOn;


       cameraPhoto.fieldOfView = camPhotoValue;
       sliderFoV.value = sldFov;
       tglDepth.isOn = tgldepth;
       tglColor.isOn = tglcolor;

       sliderFoV.value = fovIni;
       cameraPhoto.fieldOfView = fovIni;
       tglDepth.isOn = false;
       tglColor.isOn = false;

       //Obsoleto Select de lente SIN USO
       void DropDownItemSelected(TMP_Dropdown dropdown)
       {
           int index = dropdown.value;
           //Usar solo Fov y focalLength
           switch (index)
           {
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
                   //cameraPhoto.sensorSize.Set(lenteAngular[1], lenteAngular[2]);
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
   });*/
}
