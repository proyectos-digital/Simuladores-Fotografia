using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvController : MonoBehaviour
{
    public bool isOpenGeneral = false;
    public bool isOpenInventory = false;

    [Header("Cámaras")]
    public Camera camPrincipal;
    public Camera camAuxiliar;
    public Camera camJugador;

    [Header("UI")]
    //Modificar con tecla P e I para grabar e inventario respectivamente // Bloquear Mouse para acceder a la UI
    [SerializeField] GameObject panelGeneral;
    [SerializeField] GameObject panelInventory;

    //FUNCIONALES
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
    //Funcion booleana que se ejecuta las funciones de TomaElementos y ActivarPanel
    //Para los objetos instanciados y camaras
    public bool CheckActivePanels()
    {
        if (isOpenGeneral || isOpenInventory)
        {
            return true;
        }
        return false;
    }
}