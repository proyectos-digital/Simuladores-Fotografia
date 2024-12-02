using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ElevarCamara : MonoBehaviour
{
    public Slider xRotationSlider;
    public float minValue = 0f;
    public float maxValue = 0f;
    public Transform[] objectToElevate;

    // Start is called before the first frame update
    void Start()
    {
        if (xRotationSlider != null)
        {
            xRotationSlider.minValue = minValue;
            xRotationSlider.maxValue = maxValue;
            xRotationSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    void OnSliderValueChanged(float value)
    {
        if (objectToElevate != null)
        {
            for (int i = 0; i < objectToElevate.Length ; i++)
            {
                Vector3 currentPosition = objectToElevate[i].localPosition;
                currentPosition.y = value;
                objectToElevate[i].localPosition = currentPosition;
                
            }
        }
    }
}
