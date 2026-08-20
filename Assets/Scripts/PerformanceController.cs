using UnityEngine;

public class PerformanceController : MonoBehaviour
{
    //Ajustamos la velocidad de los frames por segundo a 60
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }

    
}
