using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneCtrl : MonoBehaviour
{
    [Header("Propiedades del Dron")]
    public float velMovimiento;
    public float alturaMax;
    public float velocidad;

    private bool enDespegue = false;
    private bool enAterrizaje = false;
    private bool enUso = false;
    private Vector3 posicionInicial;

    [Header("Controladores")]
    public DroneCtrl dronController;

    [Header("Posición de elementos")]
    public Transform puntoDespegueAterrizaje;
    Rigidbody rb;
    private Vector2 inputDirection;
    public Vector3 rotationSpeed = new Vector3(0, 80, 0);
    [SerializeField] int counter = 0;
    [SerializeField] ElevarCamara elevarCamaraSlider;

    void Start()
    {
        posicionInicial = puntoDespegueAterrizaje.position;
        enUso = false;
        rb = GetComponent<Rigidbody>();
        elevarCamaraSlider.yPositionSlider.minValue = posicionInicial.y;
    }

    void FixedUpdate()
    {
        if (enUso)
        {
            if (!enAterrizaje)
            {
                ControlDron();
            }
            ComprobacionVuelo();
        }
    }

    //Se usa al activar el trigger y presionar Q
    //Revisar como cambiar logica por tecla sin activar trigger y sin afectar el resto del simulador
    public void Despegue()
    {
        enDespegue = true;
        enAterrizaje = false;
        EnUso();
        counter++;
        //dronController.enabled = true;
        //Revisar necesidad
        //playerController.enabled = false;
        //btnCerrar.SetActive(false);
    }

    //Al presionar Q se sale del modo Dron
    public void Aterrizaje()
    {
        enAterrizaje = true;
        enDespegue = false;
        enAterrizaje = false;
        //transform.position = posicionInicial;
        EnUso();
        //StartCoroutine("FinVuelo");
    }
    public void EnUso()
    {
        enUso = !enUso;
    }

    //Funcion antes de volver a "modo Player" 
    IEnumerator FinVuelo()
    {
        yield return new WaitForSeconds(3f);
        //dronController.enabled = false;
        EnUso();
        //btnCerrar.SetActive(true);
        //btnMenu.SetActive(true);
    }

    public void ComprobacionVuelo()
    {
        if (enDespegue && counter <= 1)
        {
            Vector3 tempVect = new Vector3(0, velMovimiento, 0);
            tempVect = tempVect.normalized * velocidad * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + tempVect);
            elevarCamaraSlider.UpdateSliderDronValue(rb.position.y);
            //transform.Translate(Vector3.up * velocidad * Time.deltaTime);       //Se desplaza hacia arriba hasta la altura máxima
            if (transform.position.y - posicionInicial.y >= alturaMax - 0.5f)           //Verifica si ha alcanzado la altura máxima
            {
                rb.MovePosition(new Vector3(rb.position.x, alturaMax, rb.position.z));
                elevarCamaraSlider.yPositionSlider.minValue = elevarCamaraSlider.minValue;
                enDespegue = false;
            }
        }
    }

    public void ControlDron()
    {
        Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputDirection = inputs.normalized;

        Quaternion deltaRotation = Quaternion.Euler(inputDirection.x * rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        rb.MovePosition(rb.position + transform.forward * velMovimiento * inputDirection.y * Time.fixedDeltaTime);
    }
}