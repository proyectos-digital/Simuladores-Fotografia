using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotacionAltura : MonoBehaviour
{
    [Header("Objeto a rotar")]
    public GameObject[] objetos;
    public Slider rotationSlider;
    [Header("0=rot X| 1=rot Y| 2=lightTemp")]
    public int tipoRotacion;

    [SerializeField] private Light ambientLight;

    private void Start()
    {
        rotationSlider.onValueChanged.AddListener(EdicionObjeto);
        if (objetos != null)
        {
            rotationSlider.value = objetos[0].transform.eulerAngles.y / 360f;
        }
    }

    public void EdicionObjeto(float value)
    {
        float valorRotacionX = value * 360f;
        float valorRotacionY = value * 360f;
        
        switch (tipoRotacion)
        {
            case 0:
                for (int i = 0; i < objetos.Length; i++)
                {
                    objetos[i].transform.rotation = Quaternion.Euler(valorRotacionX, 0f, 0f);
                }
                break;

            case 1:
                for (int i = 0; i < objetos.Length; i++)
                {
                    objetos[i].transform.rotation = Quaternion.Euler(0f, valorRotacionY, 0f);
                }
                break;

            case 2:
                ambientLight.colorTemperature = value;
                break;
        }
    }
}
