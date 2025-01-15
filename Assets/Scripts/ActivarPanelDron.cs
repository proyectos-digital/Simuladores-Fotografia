using System.Collections;
using UnityEngine;

//SCRIPT PARA LOS SIMULADORES DE TV DRON
public class ActivarPanelDron : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject canvasDronOff;
    public GameObject canvasDronStart;
    [SerializeField] GameObject panelRender;

    //private TomaElementos tomaElementos;
    //private InstanciarElementos inAccesorios;
    private PlayerMovement playerMovement;
    public bool active = false;
    public bool pressQ = false;
    PlayerCam playerCam;
    public DroneCtrl droneCtrl;//

    //Inicialización de variables
    void Start()
    {
        canvasDronOff.SetActive(false);
        //inAccesorios = GameObject.FindWithTag("btnInstancia").GetComponent<InstanciarElementos>();
        //tomaElementos = this.GetComponent<TomaElementos>();
        //droneCtrl = this.GetComponent<DroneCtrl>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerCam = GameObject.FindWithTag("MainCamera").GetComponent<PlayerCam>();
    }
    private void Update()
    {
        //Se inicia el Dron y se bloquea el movimiento del personaje con tecla Q
        if ((playerMovement.isMove && active) && Input.GetKeyUp(KeyCode.Q)) //&& !tomaElementos.isGrabbed
        {
            canvasDronStart.SetActive(false);
            canvasDronOff.SetActive(true);
            //tomaElementos.BloquearPaneles(1);
            playerMovement.MoveAllow();
            Cursor.visible = true;
            droneCtrl.Despegue();
            panelRender.SetActive(true);
        }
        // Si el dron está en uso y se presiona Q, se sale del y el personaje vuelve a moverse libremente
        else if(!playerMovement.isMove && Input.GetKeyUp(KeyCode.Q)) //&& !tomaElementos.isGrabbed
        {
            active = false;
            canvasDronStart.SetActive(false);
            canvasDronOff.SetActive(false);
            playerMovement.MoveAllow();
            //playerCam.MouseLocked();
            droneCtrl.Aterrizaje();
            panelRender.SetActive(false);
        }
    }

    // Si el jugador entra en el trigger, se activa el modo edición si no hay un objeto agarrado
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))// && (!tomaElementos.CallCheck()) && !tomaElementos.isGrabbed)
        {
            active = true;
            canvasDronStart.SetActive(active);
        }
    }

    // Si el jugador sale del trigger, se desactiva el modo edición si no hay un objeto agarrado
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))// && !tomaElementos.isGrabbed)
        {
            active = false;
            canvasDronStart.SetActive(active);
        }
    }
}
