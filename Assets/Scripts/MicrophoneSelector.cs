using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneSelector : MonoBehaviour
{
    [Header("Elegir microfono correcto del caso")]
    [SerializeField]
    GameObject selectorCase;

    NotificationController notificationController;
    // Start is called before the first frame update
    void Start()
    {
        notificationController = GameObject.FindGameObjectWithTag("Notification").GetComponent<NotificationController>();
    }

    public bool ElegirMicrofono(GameObject gameObject)
    {
        if (gameObject.name == selectorCase.name)
        {
            return true;
        }
        else
        {
            notificationController.SendNotification("Este no es el micrófono correcto");
            return false;
        }
    }
}
