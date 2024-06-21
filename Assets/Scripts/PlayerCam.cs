using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    private float sensX;
    private float sensY;
    public Slider sensitivtySloder;

    public Transform orientation;

    float xRotation;
    float yRotation;

    [SerializeField] bool lockMouse = false;
    [Header("SOLO PARA TV")]
    [SerializeField] bool isTv = false;
    [SerializeField] TvController tvController;

    void Update(){
        //Mostrar u ocultar el Mouse por tecla P para grabar o Q para mover objetos, pensar en I para inventario ¬¬
        if (isTv)
        {
            if (Input.GetKeyUp(KeyCode.P) && !tvController.isOpenInventory)
            {
                tvController.PanelCentral();
                MouseLocked();
            }
            if (Input.GetKeyUp(KeyCode.I) && !tvController.isOpenGeneral)
            {
                tvController.PanelInventory();
                MouseLocked();
            }

        }
        if (!lockMouse)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivtySloder.value;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivtySloder.value;

            //Debug.Log(sensitivtySloder.value);
            yRotation += mouseX;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }

    }
    public void MouseLocked()
    {
        lockMouse = !lockMouse;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = lockMouse;
    }
}
