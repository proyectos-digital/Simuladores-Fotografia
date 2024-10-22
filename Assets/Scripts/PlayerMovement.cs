using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Velocidad de movimiento
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;// Resistencia en el suelo

    [Header("Ground Check")]
    public float playerHeight; // Altura del jugador
    public LayerMask whatIsGround; // Capas del suelo
    bool grounded; // Indica si el jugador está en el suelo

    public Transform orientation; // Dirección de orientación

    //Los inputs de los ejes X y Y
    float horizontalInput; 
    float verticalInput;

    // Dirección del movimiento
    Vector3 moveDirection;

    Rigidbody rb;
    [SerializeField] CameraManager camManager; // Referencia al script CameraManager
    [SerializeField] bool isTV = false; //Sera false si no esta en escena simulador Tv
    bool isMove = true;

    void Start(){
        //Busca el script CameraManager si no esta del simulador TV
        rb = GetComponent<Rigidbody>();
        if (!isTV)
        {
            camManager = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManager>();
            camManager.panelStudy += MoveAllow;
        }
        rb.freezeRotation = true; // Congela la rotación del Rigidbody
    }
    
    void Update(){
        //Verifica constantemente si el jugador esta tocando suelo
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f +0.2f, whatIsGround);
        
        MyInput(); // Manejar los inputs del jugador
        SpeedControl(); // Controlar la velocidad

        //Ajuste de fisicas segun este tocando el suelo
        if (grounded){
            rb.drag = groundDrag;
        }else{
            rb.drag = 0;
        }
    }
    //Mover al jugador 
    private void FixedUpdate() {
        MovePlayer();
    }
    //Permitirle moverse al jugador
    public void MoveAllow() {
        isMove = !isMove;
    }

    //Asignamos las entradas del jugador con WASD o las teclas de flecha en variables
    private void MyInput(){
        if (!isMove)
            return;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer(){
        //Calcular dirección del movimiento
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized*moveSpeed * 10f, ForceMode.Force);
    }

    //Controlamos la velocidad de este
    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x , 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed){
            Vector3 limitedVel= flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
