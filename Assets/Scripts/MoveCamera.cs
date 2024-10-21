using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform studyPosition;
    public PlayerMovement playerMov;
    [SerializeField] bool isStudy;
    [SerializeField] bool isTV = false;
    [SerializeField] CameraManager camManager;

    void Start() {
        //Si no estamos en simulador Tv, busca asignamos la variable de correspondiente para el simulador fotograf�a
        if (!isTV)
        {
            camManager = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManager>();
            camManager.panelStudy += MoveCam;
        }
    }

    void Update() {
        //En escenas de Studio y Studio People cambiaremos la posici�n de la camara al entrar en modo c�mara de esas escenas
        if (!isStudy) {
            transform.position = cameraPosition.position;
        } else {
            transform.position = studyPosition.position;
        }
    }
    //Funci�n para cambiar estado de variable y mover objeto al indicado.
    void MoveCam() {
        isStudy = !isStudy;
        transform.position = studyPosition.position;
    }
}
