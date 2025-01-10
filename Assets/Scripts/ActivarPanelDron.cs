using UnityEngine;

//SCRIPT PARA LOS SIMULADORES DE TV DRON
public class ActivarPanelDron : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject canvasDronOff;
    public GameObject canvasDronStart;

    //private TomaElementos tomaElementos;
    //private InstanciarElementos inAccesorios;
    private PlayerMovement playerMovement;
    public bool active = false;
    public bool pressQ = false;
    PlayerCam playerCam;
    DroneCtrl droneCtrl;

    //Inicialización de variables
    void Start()
    {
        canvasDronOff.SetActive(false);
        //inAccesorios = GameObject.FindWithTag("btnInstancia").GetComponent<InstanciarElementos>();
        //tomaElementos = this.GetComponent<TomaElementos>();
        droneCtrl = this.GetComponent<DroneCtrl>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerCam = GameObject.FindWithTag("MainCamera").GetComponent<PlayerCam>();
    }
    private void Update()
    {
        //Se inicia el Dron y se bloquea el movimiento del personaje con tecla R
        if (active && Input.GetKeyUp(KeyCode.Q)) //&& !tomaElementos.isGrabbed
        {
            canvasDronStart.SetActive(false);
            canvasDronOff.SetActive(true);
            //tomaElementos.BloquearPaneles(1);
            playerMovement.MoveAllow();
            Cursor.visible = true;
            droneCtrl.Despegue();
        }
        // Si el dron está en uso y se presiona R, se sale del y el personaje vuelve a moverse libremente
        else if(!playerMovement.isMove && Input.GetKeyUp(KeyCode.Q)) //&& !tomaElementos.isGrabbed
        {
            active = false;
            canvasDronOff.SetActive(false);
            canvasDronStart.SetActive(false);
            playerMovement.MoveAllow();
            //playerCam.MouseLocked();
            droneCtrl.Aterrizaje();
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
