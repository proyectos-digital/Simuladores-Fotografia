using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvController : MonoBehaviour
{
    public bool isOpenGeneral = false;
    public bool isOpenInventory = false;

    [Header("C�maras")]
    public Camera camPrincipal;
    public Camera camAuxiliar;
    public Camera camJugador;

    [Header("UI")]
    //Modificar con tecla P e I para grabar e inventario respectivamente // Bloquear Mouse para acceder a la UI
    [SerializeField] GameObject panelGeneral;
    [SerializeField] GameObject panelInventory;

    //NO VA!!!
    //public GameObject panelEdicion;
    //public GameObject panelInfo;
    //Preguntar a Jaime
    public GameObject PanelDia;

    //[Header ("Jugador")]
    //public PlayerController playerController;

    //OBSOLETOSSS!!!!????? O VAN EN OTRO LADO
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PanelDia.SetActive(false);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        PanelDia.SetActive(true);
        //playerController.LockCursor();
        //ManejoCamaras(4);
    }*/

    public void PanelCentral()
    {
        isOpenGeneral = !isOpenGeneral;
        panelGeneral.SetActive(isOpenGeneral);
    }
    public void PanelInventory()
    {
        isOpenInventory = !isOpenInventory;
        panelInventory.SetActive(isOpenInventory);
    }
    public void ManejoCamaras(int caso)
    {
        //REVISAR LOS CASOS SI SON NECESARIOS Y MEJORARLOS DADO CASO
        switch (caso)
        {
            case 0: //Cam Principal
                camPrincipal.targetDisplay = 0;
                camAuxiliar.targetDisplay = 1;
                camJugador.targetDisplay = 1;
                //Ahora ser�a panelGeneral revisar si es necesario
                //panelCentral.SetActive(false);
                //panelInfo.SetActive(false);
                camPrincipal.targetTexture = null;
                camAuxiliar.targetTexture = null;
                camJugador.targetTexture = null;

                break;

            case 1: //Cam Auxiliar
                camPrincipal.targetDisplay = 1;
                camAuxiliar.targetDisplay = 0;
                camJugador.targetDisplay = 1;
                //panelInfo.SetActive(false);
                //Ahora ser�a panelGeneral revisar si es necesario
                //panelCentral.SetActive(false);
                camPrincipal.targetTexture = null;
                camAuxiliar.targetTexture = null;
                camJugador.targetTexture = null;
                break;

            case 2: //Volver a c�mara del jugador
                camPrincipal.targetDisplay = 1;
                camAuxiliar.targetDisplay = 1;
                camJugador.targetDisplay = 0;
                //playerController.enabled = true;
                //Ahora ser�a panelGeneral revisar si es necesario
                //panelCentral.SetActive(true);
                camPrincipal.targetTexture = null;
                camAuxiliar.targetTexture = null;
                camJugador.targetTexture = null;
                break;

            //POSIBLEMENTE OBSOLETOS
            /*
            case 3: //Abrir la c�mara
                panelEdicion.SetActive(true);
                panelInfo.SetActive(false);
                //playerController.enabled = false;
                break;

            case 4: //Volver al panel
                panelEdicion.SetActive(false);
                panelInfo.SetActive(true);
                //playerController.enabled = true;
                break;
            */
        }
    }
}