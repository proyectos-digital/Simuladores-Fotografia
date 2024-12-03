using UnityEngine;
using UnityEngine.UI;

public class ElevarCamara : MonoBehaviour
{
    public Slider yPositionSlider;
    public float minValue = 0f;
    public float maxValue = 0.31f;
    public Transform objectToElevate;

    // Start is called before the first frame update
    void Start()
    {
        if (yPositionSlider != null)
        {
            yPositionSlider.minValue = minValue;
            yPositionSlider.maxValue = maxValue;
            yPositionSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    void OnSliderValueChanged(float value)
    {
            Vector3 currentPosition = objectToElevate.localPosition;
            currentPosition.y = value;
            objectToElevate.localPosition = currentPosition;
                
            
        
    }
}
