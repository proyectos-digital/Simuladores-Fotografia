using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstanciarElementos : MonoBehaviour
{
    [SerializeField] PlayerCam playerCam;
    [Header ("Accesorios")]
    public GameObject luminaria1;       //Luz
    public GameObject aperture300;      //Luz
    public GameObject sennheiser;       //Micrófono
    public GameObject neewer660;        //Luz
    public GameObject godox_SL60W;      //Luz

    [Header("Cantidad de objetos")]
    public int cantLuminaria;
    public int cantAperture300;
    public int cantSennheiser;
    public int cantNeewer660;
    public int cantGodox;

    [Header("Textos de cantidades")]
    public TMP_Text txtCantLuminaria;
    public TMP_Text txtCantAperture300;
    public TMP_Text txtCantSennheiser;
    public TMP_Text txtCantNeewer660;
    public TMP_Text txtCantGodox;

    [Header ("Referencia al jugador")]
    public Transform manoJugador;

    [Header("Luces superiores")]
    public GameObject pnlLuces;
    //public MenuElementos pnlElementos;

    [Header("Accesorios")]
    public GameObject btnLuminaria1;
    public GameObject btnAperture300;
    public GameObject btnSennheiser;
    public GameObject btnNeewer660;
    public GameObject btnGodox;


    void Start()
    {
        txtCantLuminaria.text = cantLuminaria + "/2";
        txtCantAperture300.text = cantAperture300 + "/2";
        txtCantSennheiser.text = cantSennheiser + "/1";
        txtCantNeewer660.text = cantNeewer660 + "/2";
        txtCantGodox.text = cantGodox + "/2";
    }
    //Instancia objetos según el valor del botón correspondiente
    // 0 - Luz Luminaria
    // 1 - Luz Aperture 300
    // 2 - Microfono Sennheiser
    // 3 - Luz Neewer 660
    // 4 - Luz Godox
    public void NuevoAccersorio(int accesorio)
    {
        
        playerCam.MouseLocked();
        switch(accesorio)
        {
            case 0:
                if (cantLuminaria > 0)
                {
                    Instantiate(luminaria1, manoJugador.transform.position, manoJugador.transform.rotation);
                    cantLuminaria--;
                    txtCantLuminaria.text = cantLuminaria + "/2";
                    //pnlElementos.ActivarUI(0);  
                    if (cantLuminaria == 0) btnLuminaria1.SetActive(false);
                }
                break;

            case 1:
                if (cantAperture300 > 0)
                {
                    Instantiate(aperture300, manoJugador.transform.position, manoJugador.transform.rotation);
                    cantAperture300--;
                    txtCantAperture300.text = cantAperture300 + "/2";
                    if (cantAperture300 == 0) btnAperture300.SetActive(false);
                }
                break;

            case 2:
                if (cantSennheiser > 0)
                {
                    Instantiate(sennheiser, manoJugador.transform.position, manoJugador.transform.rotation);
                    cantSennheiser--;
                    txtCantSennheiser.text = cantSennheiser + "/1";
                    if (cantSennheiser == 0) btnSennheiser.SetActive(false);
                }
                break;

            case 3:
                if (cantNeewer660 > 0)
                {
                    Instantiate(neewer660, manoJugador.transform.position, manoJugador.transform.rotation);
                    cantNeewer660--;
                    txtCantNeewer660.text = "" + cantNeewer660;
                }
                break;

            case 4:
                if (cantGodox > 0)
                {
                    Instantiate(godox_SL60W, manoJugador.transform.position, manoJugador.transform.rotation);
                    cantGodox--;
                    txtCantGodox.text = cantGodox + "/2";
                }
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            pnlLuces.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pnlLuces.SetActive(true);
        }
    }
}
