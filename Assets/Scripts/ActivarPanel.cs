using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivarPanel : MonoBehaviour
{   
    [Header ("Paneles")]
    public GameObject canvasEditarElm, canvasInfo;
    private InstanciarElementos inAccesorios;
    //REVISAR EN CUANTO SE IMPLEMENTE FULL
    //private PlayerController playerController;
    private bool active;

    [SerializeField] ////REVISAR EN CUANTO SE IMPLEMENTE FULL
    //private CamController camController;

    void Start()
    {
        canvasEditarElm.SetActive(false);
        inAccesorios = GameObject.FindWithTag("btnInstancia").GetComponent<InstanciarElementos>();
        //REVISAR EN CUANTO SE IMPLEMENTE FULL
        //playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //if (this.CompareTag("CamPrincipal"))
        //{
        //    Debug.Log("jplñas mama");
        //    //camController = GetComponent<CamController>();
        //}
    }

    //Para reacomodar
    /*
     void Update()
    {
        if(active && Input.GetKeyUp(KeyCode.Q)) {
            lightObj.enabled = !lightObj.isActiveAndEnabled;
            lightObj.GetComponentInChildren<Renderer>().material = lightObj.enabled ? materialOn : materialOff;
            txtInfo.text = lightObj.enabled ? "Presiona Q para apagar la Luz.": "Presiona Q para encender Luz.";
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            active = true;
            panelInfo.SetActive(true);
            txtInfo.text = lightObj.enabled ? "Presiona Q para apagar la Luz." : "Presiona Q para encender Luz.";
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            active = false;
            txtInfo.text = "";
            panelInfo.SetActive(false);
        }
    }
    */
    private void Update()
    {
        //Revisar condicion camController no puede ir porque los otros elementos se deben poder modificar con Q
        //Más no cambiar modo de camara
        if (active && Input.GetKeyUp(KeyCode.Q))// && camController)
        {
            Debug.Log("Aqui cambiamos el modo");
            canvasEditarElm.SetActive(true);
            //playerController.Move(false);
        }
    }

        private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            active = true;
            //Cambiar por tecla Q para acceder al panel
            //Debug.Log("aqui estamos");
            //canvasEditarElm.SetActive(true);
            canvasInfo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //canvasEditarElm.SetActive(false);
        canvasInfo.SetActive(false);
        //active = false;
    }

    public void EliminarAccesorio(int accesorioElim)
    {
        switch (accesorioElim)
        {
            case 0:
                Destroy(gameObject);
                inAccesorios.cantLuminaria++;
                inAccesorios.txtCantLuminaria.text = "" + inAccesorios.cantLuminaria;
                //playerController.LockCursor();
                break;

            case 1:
                Destroy(gameObject);
                inAccesorios.cantAperture300++;
                inAccesorios.txtCantAperture300.text = "" + inAccesorios.cantAperture300;
                //playerController.LockCursor();
                break;

            case 2:
                Destroy(gameObject);
                inAccesorios.cantSennheiser++;
                inAccesorios.txtCantSennheiser.text = "" + inAccesorios.cantSennheiser;
                //playerController.LockCursor();
                break;

            case 3:
                Destroy(gameObject);
                inAccesorios.cantNeewer660++;
                inAccesorios.txtCantNeewer660.text = "" + inAccesorios.cantNeewer660;
                //playerController.LockCursor();
                break;

            case 4:
                Destroy(gameObject);
                inAccesorios.cantGodox++;
                inAccesorios.txtCantGodox.text = "" + inAccesorios.cantGodox;
                //playerController.LockCursor();
                break;
        }
    }
}
