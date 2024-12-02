using UnityEngine;
using UnityEngine.UI;

public class RotateObject : MonoBehaviour
{
    public Slider xRotationSlider; // Asigna el slider desde el editor
    public Transform objectToRotate; // Asigna el objeto que quieres rotar

    void Start()
    {
        if (xRotationSlider != null)
        {
            xRotationSlider.minValue = -90;
            xRotationSlider.maxValue = 90;
            xRotationSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    void OnSliderValueChanged(float value)
    {
        if (objectToRotate != null)
        {
            Vector3 currentRotation = objectToRotate.localEulerAngles;
            currentRotation.x = value;
            objectToRotate.localEulerAngles = currentRotation;
        }
    }
}
