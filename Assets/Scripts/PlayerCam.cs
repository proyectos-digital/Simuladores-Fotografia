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

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update(){
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivtySloder.value;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivtySloder.value;

        Debug.Log(sensitivtySloder.value);
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
