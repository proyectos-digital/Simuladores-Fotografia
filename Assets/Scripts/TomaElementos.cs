using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TomaElementos : MonoBehaviour
{
    [Header("Toma de elementos")]
    public GameObject elementos;            //El elemento que tomaré
    public GameObject canvasInfo;
    public GameObject grabInfoImg;
    public GameObject exitInfoImg;
    public GameObject dropInfoImg;
    [SerializeField] bool noMovable = false;
    public bool isGrabbed;
    private Transform posicionElemento;     //Mano
    private bool activ;                     //Para saber cuando estoy dentro o fuera de la zona del objeto
    private ActivarPanel activarPanel;
    TvController tvController;
    public NotificationController nc;

    private void Start()
    {
        GameObject objetoMano = GameObject.FindWithTag("Mano");
        activarPanel = this.GetComponent<ActivarPanel>();
        tvController = GameObject.FindWithTag("Tv").GetComponent<TvController>();
        nc = GameObject.FindWithTag("Notification").GetComponent<NotificationController>();
        MensajesPanel(grabInfoImg);
        if (objetoMano != null)
        {
            posicionElemento = objetoMano.transform;
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con la etiqueta Mano.");
        }
    }
    void Update()
    {
        TomaElemento();
    }
    public void BloquearPaneles(int value)
    {
        tvController.isOpenGeneral = true;
        tvController.isOpenInventory = true;
        //Cambio de manejo de mensajes, ahora activaremos objetos en el PanelInfo
        MensajesPanel(isGrabbed ? dropInfoImg : value > 0 ? exitInfoImg : grabInfoImg);
            //"-Configura los valores de la izquierda.\n\n-<b><size=22>Q</size></b> SALIR.");
    }
    public void DesactivarInfo()
    {
        canvasInfo.SetActive(false);
        activ = false;
        activarPanel.active = false;
        isGrabbed = false;
        tvController.isOpenGeneral = false;
        tvController.isOpenInventory = false;
        MensajesPanel(null);
    }

    public void TomaElemento()
    {
        if (activ && !activarPanel.pressQ)
        {
            //Toma el elemento
            if (Input.GetKeyUp(KeyCode.T) && posicionElemento.childCount == 0)
            {
                isGrabbed = true;
                elementos.transform.SetParent(posicionElemento);
                elementos.transform.position = posicionElemento.position;
                elementos.transform.rotation = posicionElemento.rotation;
                BloquearPaneles(0);
            }
            else if (Input.GetKeyUp(KeyCode.T) && !isGrabbed && posicionElemento.childCount > 0)
            {
                nc.SendNotification("Toma de a un objeto!");
            }
        }
        //Suelta el elemento
        if (Input.GetKeyUp(KeyCode.E) && posicionElemento.childCount > 0)
        {
            elementos.transform.eulerAngles = new Vector3(0, elementos.transform.eulerAngles.y, elementos.transform.eulerAngles.z);
            elementos.transform.SetParent(null);
            DesactivarInfo();
        }
    }
    public void MensajesPanel(GameObject obj)
    {
        grabInfoImg.SetActive(false);
        dropInfoImg.SetActive(false);
        exitInfoImg.SetActive(false);
        if (obj == null)
        {
            canvasInfo.SetActive(false);
            return;
        }
        obj.SetActive(true);
    }
    public bool CallCheck()
    {
        return tvController.CheckActivePanels();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && posicionElemento.childCount <= 0)
        {
            canvasInfo.SetActive(!CallCheck());
            MensajesPanel(grabInfoImg);
            if (!noMovable && (!isGrabbed || !activarPanel.pressQ) && !CallCheck())
            {
                activ = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && (!isGrabbed && !activarPanel.pressQ))
        {
            activ = false;
            canvasInfo.SetActive(false);
        }
    }
}