using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ElevarCamara : MonoBehaviour
{
    public Slider yPositionSlider;
    public float minValue = 0f;
    public float maxValue = 0.31f;
    public Transform objectToElevate;
    [SerializeField]Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MusicFestival") {
            yPositionSlider.minValue = minValue;
            yPositionSlider.maxValue = maxValue;
            yPositionSlider.onValueChanged.AddListener(OnSliderValueChangedDron);
            return;
        }
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
    //Función para elevar el dron usando el Rigidbody
    void OnSliderValueChangedDron(float value)
    {
        Vector3 currentPosition = rb.position;
        currentPosition.y = value;
        rb.position = currentPosition;
    }
    public void UpdateSliderDronValue(float value) 
    {
        yPositionSlider.value = value;
    }
}
