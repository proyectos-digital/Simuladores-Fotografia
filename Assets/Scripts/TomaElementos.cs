using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TomaElementos : MonoBehaviour
{
    [Header("Toma de elementos")]
    public TMP_Text txtAviso;
    public GameObject elementos;            //El elemento que tomaré
    private Transform posicionElemento;     //Mano
    private bool activ;                     //Para saber cuando estoy dentro o fuera de la zona del objeto

    private void Start()
    {
        GameObject objetoMano = GameObject.FindWithTag("Mano");

        if (objetoMano != null)
        {
            posicionElemento = objetoMano.transform;
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con la etiqueta Mano.");
        }
    }
    void Update()
    {
        TomaElemento();
    }

    public void TomaElemento()
    {
        if (activ == true)
        {
            //Toma el elemento
            if (Input.GetKeyDown(KeyCode.T) && posicionElemento.childCount == 0)
            {
                elementos.transform.SetParent(posicionElemento);
                elementos.transform.position = posicionElemento.position;
                elementos.transform.rotation = posicionElemento.rotation;
            }
            else if (Input.GetKeyDown(KeyCode.T) && posicionElemento.childCount > 0)
            {
                StartCoroutine(TextoAviso());
            }

            //Suelta el elemento
            if (Input.GetKeyDown(KeyCode.E) && posicionElemento.childCount > 0)
            {
                elementos.transform.SetParent(null);
            }
        }
    }

    IEnumerator TextoAviso()
    {
        txtAviso.text = "Toma de a un objeto!";
        yield return new WaitForSeconds(2f);
        txtAviso.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            activ = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activ = false;
        }
    }
}