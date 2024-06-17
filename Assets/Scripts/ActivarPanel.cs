using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivarPanel : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject canvasEditarElm;
    public TMP_Text txtMensajePanel;
    private TomaElementos tomaElementos;

    private InstanciarElementos inAccesorios;
    private PlayerMovement playerMovement;
    private bool active= false;
    public bool pressQ = false;


    PlayerCam playerCam;

    void Start()
    {
        canvasEditarElm.SetActive(false);
        inAccesorios = GameObject.FindWithTag("btnInstancia").GetComponent<InstanciarElementos>();
        tomaElementos = this.GetComponent<TomaElementos>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerCam = GameObject.FindWithTag("MainCamera").GetComponent<PlayerCam>();
        txtMensajePanel.text = "-PRESIONA <b><size=22>Q</size></b> ABRIR CONFIGURACIÓN\n \n- PRESIONA <b><size=22>T</size></b> AGARRAR OBJETO";
    }
    private void Update()
    {
        //Si el objeto está en modo edición y se presiona Q se sale del modo edición y vuelve
        //el personaje a moverse libremente
        if ((canvasEditarElm.activeSelf && !tomaElementos.isGrabbed) && Input.GetKeyUp(KeyCode.Q))
        {
            pressQ = false;
            active = false;
            canvasEditarElm.SetActive(false);
            tomaElementos.DesactivarInfo();
            playerMovement.MoveAllow();
            playerCam.MouseLocked();
            txtMensajePanel.text = "-PRESIONA <b><size=22>Q</size></b> ABRIR CONFIGURACIÓN\n \n- PRESIONA <b><size=22>T</size></b> AGARRAR OBJETO";
        }
        //Se activa el modo edición del objeto y se bloquea el movimiento del personaje con tecla Q
        else if ((active && !tomaElementos.isGrabbed) && Input.GetKeyUp(KeyCode.Q))
        {
            pressQ = true;
            canvasEditarElm.SetActive(true);
            playerMovement.MoveAllow();
            playerCam.MouseLocked();
            txtMensajePanel.text = "Desliza los valores\n de la izquierda.";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !tomaElementos.isGrabbed)
        {
            active = true;
        }
    }

    private void OnTriggerExit(Collider other)
    { 
        if (other.CompareTag("Player") && !tomaElementos.isGrabbed)
        {
            active = false;
        }
    }

    //Elimina los objetos de las escenas segun el valor del botón
    // 0 - Luminaria
    // 1 - Aperture 300
    // 2 - Microfono Sennheiser
    // 3 - Neewer 660
    // 4 - Godox
    public void EliminarAccesorio(int accesorioElim)
    {
        switch (accesorioElim)
        {
            case 0:
                Destroy(gameObject);
                inAccesorios.cantLuminaria++;
                inAccesorios.txtCantLuminaria.text = inAccesorios.cantLuminaria + "/2"; ;
                inAccesorios.btnLuminaria1.SetActive(true);
                playerMovement.MoveAllow();
                playerCam.MouseLocked();
                break;

            case 1:
                Destroy(gameObject);
                inAccesorios.cantAperture300++;
                inAccesorios.txtCantAperture300.text = inAccesorios.cantAperture300 + "/2"; ;
                inAccesorios.btnAperture300.SetActive(true);
                playerMovement.MoveAllow();
                playerCam.MouseLocked();
                break;

            case 2:
                Destroy(gameObject);
                inAccesorios.cantSennheiser++;
                inAccesorios.txtCantSennheiser.text = inAccesorios.cantSennheiser + "/1"; ;
                inAccesorios.btnSennheiser.SetActive(true);
                playerMovement.MoveAllow();
                playerCam.MouseLocked();
                break;

            case 3:
                Destroy(gameObject);
                inAccesorios.cantNeewer660++;
                inAccesorios.txtCantNeewer660.text = inAccesorios.cantNeewer660 + "/2";
                inAccesorios.btnNeewer660.SetActive(true);
                playerMovement.MoveAllow();
                playerCam.MouseLocked();
                break;

            case 4:
                Destroy(gameObject);
                inAccesorios.cantGodox++;
                inAccesorios.txtCantGodox.text = inAccesorios.cantGodox + "/2";
                inAccesorios.btnGodox.SetActive(true);
                playerMovement.MoveAllow();
                playerCam.MouseLocked();
                break;
        }
    }
}
