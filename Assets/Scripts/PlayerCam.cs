using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    private float sensX;
    private float sensY;
    public Slider sensitivtySloder;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private Vector3 lastMousePosition;


    [SerializeField] bool lockMouse = false;
    [Header("SOLO PARA TV")]
    [SerializeField] bool isTv = false;
    [SerializeField] TvController tvController;

    void Update()
    {
        if (IsPointerOverUIObject())
        {
            return; // No rotar la cámara si el mouse está sobre un objeto UI.
        }
        //Mostrar u ocultar el Mouse con tecla P para grabar e I para inventario
        if (isTv)
        {
            if (Input.GetKeyUp(KeyCode.P) && !tvController.isOpenInventory)
            {
                tvController.PanelCentral();
                MouseLocked();
            }
            //En simulador TV muestra el inventario con la tecla I si no hay otro panel abierto
            if (Input.GetKeyUp(KeyCode.I) && !tvController.isOpenGeneral)
            {
                tvController.PanelInventory();
                MouseLocked();
            }

        }
        //Si el mouse no esta bloqueado rotamos la camara con el movimiento del mouse
        if (!lockMouse)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivtySloder.value;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivtySloder.value;

            yRotation += mouseX;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }else{
            // Mover la cámara solo al arrastrar el mouse.
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivtySloder.value;
                float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivtySloder.value;

                yRotation += mouseX;
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
                orientation.rotation = Quaternion.Euler(0, yRotation, 0);

                lastMousePosition = Input.mousePosition;
            }
        }
    }
    public void MouseLocked()
    {
        lockMouse = !lockMouse;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = lockMouse;
    }
    //Si al arrastrar el Mouse en modo Cámara detecta elemento del UI
    private bool IsPointerOverUIObject()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            return true;
        }

        return false;
    }
}
