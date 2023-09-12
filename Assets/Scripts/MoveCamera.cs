using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform studyPosition;
    public PlayerMovement playerMov;
    [SerializeField] bool isStudy;
    [SerializeField] CameraManager camManager;

    void Start() {
        camManager = GameObject.FindGameObjectWithTag("CamManager").GetComponent<CameraManager>();
        camManager.panelStudy += MoveCam;
    }

    void Update() {
        if (!isStudy) {
            transform.position = cameraPosition.position;
        } else {
            transform.position = studyPosition.position;
        }
    }
    void MoveCam() {
        isStudy = !isStudy;
        transform.position = studyPosition.position;
    }
}
