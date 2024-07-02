using UnityEngine;
using UnityEngine.UI;

public class StudyLightRoof : MonoBehaviour
{
    bool active = true;
    Light lightObj;
    [SerializeField] Material materialOff;
    [SerializeField] Material materialOn;
    void Start()
    {
        lightObj = GetComponent<Light>();
    }

    public void ChangeState(Toggle toggle) {
        active = toggle.isOn;
        lightObj.enabled = active;
        lightObj.GetComponentInChildren<Renderer>().material = active ? materialOn : materialOff;
    }
}
