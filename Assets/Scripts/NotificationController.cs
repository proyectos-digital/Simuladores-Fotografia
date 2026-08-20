using UnityEngine;
using TMPro;

public class NotificationController : MonoBehaviour
{

    public GameObject notificationPanel;
    public TMP_Text notificationText;
    public Animation notificationAnimation;

    //Asignamos el componente animation del panel de notificaciones para poder realizar los cambios
    void Start()
    {
        notificationAnimation = notificationPanel.GetComponent<Animation>();
    }

    //Cuando es llamado muestra el mensaje en pantalla por unos segundos y luego se vuelve a ocultar
    public void SendNotification(string valueText)
    {
        notificationPanel.SetActive(true);
        notificationAnimation.PlayQueued("Fade In", QueueMode.CompleteOthers);
        notificationText.text = valueText;
        notificationAnimation.PlayQueued("Fade Out", QueueMode.CompleteOthers);
    }
}
