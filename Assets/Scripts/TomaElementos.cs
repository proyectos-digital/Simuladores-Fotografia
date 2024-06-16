using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TomaElementos : MonoBehaviour
{
    [Header("Toma de elementos")]
    public TMP_Text txtAviso;
    public TMP_Text txtMensajePanel;
    public GameObject elementos;            //El elemento que tomaré
    public GameObject canvasInfo;
    public bool isGrabbed;
    private Transform posicionElemento;     //Mano
    private bool activ;                     //Para saber cuando estoy dentro o fuera de la zona del objeto
    private ActivarPanel activarPanel;

    private void Start()
    {
        GameObject objetoMano = GameObject.FindWithTag("Mano");
        activarPanel = this.GetComponent<ActivarPanel>();
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
    public void DesactivarInfo()
    {
        canvasInfo.SetActive(false);
        activ = false;
        isGrabbed = false;
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
                txtMensajePanel.text = "Presiona E\n para soltarlo.";
            }
            else if (Input.GetKeyDown(KeyCode.T) && posicionElemento.childCount > 0)
            {
                StartCoroutine(TextoAviso());
            }
        }
        //Suelta el elemento
        if (Input.GetKeyDown(KeyCode.E) && posicionElemento.childCount > 0)
        {
            elementos.transform.SetParent(null);
            activ = false;
            txtMensajePanel.text = "-PRESIONA Q CONFIGURACIÓN\n- PRESIONA T AGARRAR\n- PRESIONA E SOLTAR";
            isGrabbed = false;
            DesactivarInfo();
        }
    }

    IEnumerator TextoAviso()
    {
        txtAviso.text = "Toma de a un objeto!";
        yield return new WaitForSeconds(2f);
        txtAviso.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && (!isGrabbed && !activarPanel.pressQ))
        {
            activ = true;
            canvasInfo.SetActive(true);
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