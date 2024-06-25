using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TomaElementos : MonoBehaviour
{
    [Header("Toma de elementos")]
    public TMP_Text txtMensajePanel;
    public GameObject elementos;            //El elemento que tomaré
    public GameObject canvasInfo;
    [SerializeField] bool noMovable = false;
    public bool isGrabbed;
    private Transform posicionElemento;     //Mano
    private bool activ;                     //Para saber cuando estoy dentro o fuera de la zona del objeto
    private ActivarPanel activarPanel;
    TvController tvController;
    public NotificationController nc;
    public string message = "Toma de a un objeto!";

    private void Start()
    {
        GameObject objetoMano = GameObject.FindWithTag("Mano");
        activarPanel = this.GetComponent<ActivarPanel>();
        tvController = GameObject.FindWithTag("Tv").GetComponent<TvController>();
        nc = GameObject.FindWithTag("Notification").GetComponent<NotificationController>();
        MensajesPanel(noMovable ? "-<b><size=22>Q</size></b>\n\nABRIR CONFIGURACIÓN" : null);
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
    public void BloquearPaneles()
    {
        tvController.isOpenGeneral = true;
        tvController.isOpenInventory = true;
        MensajesPanel(isGrabbed ? "PRESIONA <b><size=22>E</size></b>\n SOLTAR OBJETO." : 
            "-Configura los valores de la izquierda.\n\n-<b><size=22>Q</size></b> SALIR.");
    }
    public void DesactivarInfo()
    {
        canvasInfo.SetActive(false);
        activ = false;
        activarPanel.active = false;
        isGrabbed = false;
        tvController.isOpenGeneral = false;
        tvController.isOpenInventory = false;
        MensajesPanel(noMovable ? "-<b><size=22>Q</size></b>\n\nABRIR CONFIGURACIÓN" : null);
    }

    public void TomaElemento()
    {
        if (activ && !activarPanel.pressQ)
        {
            //Toma el elemento
            if (Input.GetKeyDown(KeyCode.T) && posicionElemento.childCount == 0)
            {
                isGrabbed = true;
                elementos.transform.SetParent(posicionElemento);
                elementos.transform.position = posicionElemento.position;
                elementos.transform.rotation = posicionElemento.rotation;
                BloquearPaneles();
            }
            else if (Input.GetKeyDown(KeyCode.T) && !isGrabbed && posicionElemento.childCount > 0)
            {
                message = "Toma de a un objeto!";
                nc.SendNotification(message);
            }
        }
        //Suelta el elemento
        if (Input.GetKeyDown(KeyCode.E) && posicionElemento.childCount > 0)
        {
            elementos.transform.SetParent(null);
            DesactivarInfo();
        }
    }
    public void MensajesPanel(string text)
    {
        if(text == null)
        {
            text = "-<b><size=22>Q</size></b> ABRIR CONFIGURACIÓN\n \n-<b><size=22>T</size></b> AGARRAR OBJETO";
        }
        message = text;
        txtMensajePanel.text = text;
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