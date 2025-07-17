using UnityEngine;
using UnityEngine.UI;

public class RotateObjectY : MonoBehaviour
{
    public Slider yRotationSlider; // Asigna el slider desde el editor
    public Transform objectToRotate; // Asigna el objeto que quieres rotar

    void Start()
    {
        if (yRotationSlider != null)
        {
            yRotationSlider.minValue = 0;
            yRotationSlider.maxValue = 360;
            yRotationSlider.value = 180;
            yRotationSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    void OnSliderValueChanged(float value)
    {
        if (objectToRotate != null)
        {
            Vector3 currentRotation = objectToRotate.localEulerAngles;
            currentRotation.y = value;
            objectToRotate.localEulerAngles = currentRotation;
        }
    }
}
