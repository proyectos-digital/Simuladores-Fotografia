using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationController : MonoBehaviour
{

    public GameObject notificationPanel;
    public TMP_Text notificationText;
    public Animation notificationAnimation;
    // Start is called before the first frame update
    void Start()
    {
        notificationAnimation = notificationPanel.GetComponent<Animation>();
    }

    public void SendNotification(string valueText)
    {
        notificationPanel.SetActive(true);
        Debug.Log(valueText);
        notificationAnimation.PlayQueued("Fade In", QueueMode.CompleteOthers);
        notificationText.text = valueText;
        notificationAnimation.PlayQueued("Fade Out", QueueMode.CompleteOthers);
        //notificationAnimation.Play("Fade Out");
    }





}
