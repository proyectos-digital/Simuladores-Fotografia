using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneCtrl : MonoBehaviour
{
    [Header("Propiedades del Dron")]
    public float velMovimiento;
    public float velRotacion;
    public float alturaMax;
    public float velocidad;

    private bool enDespegue = false;
    private bool enAterrizaje = false;
    private Vector3 posicionInicial;

    [Header("Controladores")]
    public DroneCtrl dronController;
    //Buscar el movimiento del personaje
    //public PlayerController playerController;

    [Header ("Posición de elementos")]
    public Transform puntoDespegueAterrizaje;

    [Header("UI")]
    public GameObject btnCerrar;
    public GameObject panelConfi;
    public GameObject panelPrincipal;
    public GameObject btnMenu;

    void Start()
    {
        dronController.enabled = false;
        posicionInicial = puntoDespegueAterrizaje.position;
    }

    void Update()
    {
        ControlDron();
        ComprobacionVuelo();
    }

    public void Despegue()
    {
        enDespegue = true;
        enAterrizaje = false; 
        dronController.enabled = true;
        //Revisar necesidad
        //playerController.enabled = false;
        btnCerrar.SetActive(false);
    }

    public void Aterrizaje()
    {
        enAterrizaje = true;
        enDespegue = false;
        StartCoroutine("FinVuelo");
    }

    IEnumerator FinVuelo()
    {
        yield return new WaitForSeconds(3f);
        dronController.enabled = false;
        btnCerrar.SetActive(true);
        btnMenu.SetActive(true);
    }

    public void ComprobacionVuelo()
    {
        if (enDespegue)
        {
            transform.Translate(Vector3.up * velocidad * Time.deltaTime);       //Se desplaza hacia arriba hasta la altura máxima
            if (transform.position.y - posicionInicial.y >= alturaMax)           //Verifica si ha alcanzado la altura máxima
            {
                enDespegue = false;
            }
        }

        else if (enAterrizaje)
        {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);     //Desciende hacia el punto inicial
            if (transform.position.y <= posicionInicial.y)                       //Verifica si ha alcanzado o pasado la posición inicial
            {
                enAterrizaje = false;
                transform.position = posicionInicial;
            }
        }
        if (alturaMax == 6f)
        {
            //playerController.enabled = false;
            panelConfi.SetActive(true);
            //playerController.UnlockCursor();
            panelPrincipal.SetActive(true);
            btnMenu.SetActive(false);
        }
    }

    public void ControlDron()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Rotacion del dron
        float valorRotacion = horizontalInput * velRotacion * Time.deltaTime;
        transform.Rotate(Vector3.up, valorRotacion);

        Vector3 redireccion = transform.forward;

        Vector3 movimiento = redireccion * verticalInput * velMovimiento * Time.deltaTime;
        Vector3 nuevaPosicion = transform.position + movimiento;

        transform.position = nuevaPosicion;
    }
}