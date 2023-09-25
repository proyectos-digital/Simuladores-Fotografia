using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TomaElementos : MonoBehaviour {
    [Header("Toma de elementos")]
    public TMP_Text txtAviso;
    public GameObject elementos;            //El elemento que tomaré
    private Transform posicionElemento;     //Mano
    private bool activ;                     //Para saber cuando estoy dentro o fuera de la zona del objeto
    Rigidbody rb;

    private void Start() {
        GameObject objetoMano = GameObject.FindWithTag("Mano");
        rb = GetComponent<Rigidbody>();

        if (objetoMano != null) {
            posicionElemento = objetoMano.transform;
        } else {
            Debug.LogError("No se encontró ningún objeto con la etiqueta Mano.");
        }
    }
    void FixedUpdate() {
        TomaElemento();
    }

    public void TomaElemento() {
        if (activ) {
            //Toma el elemento
            if (Input.GetKeyDown(KeyCode.Q) && posicionElemento.childCount == 0) {
                elementos.transform.SetParent(posicionElemento);            //El elemento se colocará dentro del objeto que esta en el player
                //elementos.transform.position = posicionElemento.position;   //Le digo que el elemento debe quedar en la misma posición que el padre

                //elementos.transform.rotation = posicionElemento.rotation;
                rb.isKinematic = false;
                rb.detectCollisions = true;
                rb.MovePosition(posicionElemento.localPosition);
               
            } else if (Input.GetKeyDown(KeyCode.Q) && posicionElemento.childCount > 0) {
                StartCoroutine(TextoAviso());
            }
        }
        //Suelta el elemento
        if (Input.GetKeyDown(KeyCode.E) && posicionElemento.childCount > 0) {
            elementos.transform.SetParent(null);
            rb.isKinematic = true;
            //rb.detectCollisions = false;
        }
    }

    IEnumerator TextoAviso() {
        txtAviso.text = "Toma de a un objeto!";
        yield return new WaitForSeconds(2f);
        txtAviso.text = "";
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Mano") {
            Debug.Log("entro");
            activ = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Mano") {
            activ = false;
            Debug.Log("me sali: "+activ);
        }
    }
}