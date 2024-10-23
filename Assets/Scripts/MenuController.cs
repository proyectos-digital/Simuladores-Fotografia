using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

//Script para todo lo relevante con el UI y resolución de los simuladores
public class MenuController : MonoBehaviour
{
    private bool isMenuActive = false;
    private bool isInfoActive = true;
    private char[] delimiterChars = { 'x', ' '};
    private string screenResolutionSelected;
    //Objetos activadores/desactivadores de pantalla completa y cesped
    public Toggle fullScreenToggle;
    public Toggle grassEnabled;
    //Objetos Paneles
    public GameObject mainMenuPanel;
    public GameObject mainOptionsPanel;
    public GameObject controlOptionsPanel;
    public GameObject infoMenuPanel;
    public TMP_Dropdown screenSizeDropdown;
    public List<string> resString = new List<string>();

    void Start()
    {
        //Carga los valores de resolución disponibles
        SetNativeResolutions();
        screenSizeDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(screenSizeDropdown);
        });
        if (Screen.fullScreen)
        {
            fullScreenToggle.isOn = true;
        }
    }

    void Update()
    {
        //Se revisa si no hay algun menú o panel abierto al oprimir Esc para abrir el menú opciones
        if ((!isMenuActive) && Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = isMenuActive ? CursorLockMode.None : CursorLockMode.Confined;
            mainMenuPanel.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = isMenuActive ? 1 : 0;
        }
        //Carga el panel donde muestra los controles del simulador.
        if (isInfoActive)
        {
            infoMenuPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
    //Función para mostrar las diferentes resoluciones en un selector del UI
    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        screenResolutionSelected = dropdown.options[dropdown.value].text;
        string[] words = screenResolutionSelected.Split(delimiterChars);
        foreach (var word in words)
        {
            Debug.Log(word);
        }
        int resx = Int32.Parse(words[0]);
        int resy = Int32.Parse(words[1]);
        if (fullScreenToggle.isOn)
        {
            Screen.SetResolution(resx, resy, true);
        }
        else
        {
            Screen.SetResolution(resx, resy, false);
        }
    }
    //Desactiva u oculta el panel de opciones
    public void ActivateMenu(bool activeMenu)
    {
        Cursor.lockState = activeMenu ? CursorLockMode.None : CursorLockMode.Confined;
        mainMenuPanel.SetActive(false);
        Cursor.visible = isMenuActive;
        Time.timeScale = isMenuActive ? 0 : 1;
    }
    //Desactiva el panel de controles e inicia el simulador
    public void DisableinfoPanel()
    {
        isInfoActive = false;
        infoMenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }
    //Muestra u oculta el panel de cambiar controles
    public void EnabledControlPanel(bool showControlPanel)
    {
        if (showControlPanel)
        {
            mainOptionsPanel.SetActive(false);
            controlOptionsPanel.SetActive(true);
        }
        else
        {
            mainOptionsPanel.SetActive(true);
            controlOptionsPanel.SetActive(false);
        }
    }
    //Cambia a modo de pantalla completa
    public void SetFullScreen(Toggle fsToggle)
    {
        if (fsToggle.isOn)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }
    //Función para cerrar el simulador
    public void QuitApplication()
    {
        Application.Quit();
    }
    //Funcion para cargar las resoluciones disponibles y agregarlas a la lista
    private void SetNativeResolutions()
    {
        var resolutions = Screen.resolutions.Where(resolution => resolution.refreshRateRatio.value == 60.0f );
        
        foreach (var res in resolutions)
        {
            resString.Add(res.width.ToString() + "x" + res.height.ToString() +" "+ res.refreshRateRatio.value.ToString("F0") + "hz");
        }
        screenSizeDropdown.ClearOptions();
        screenSizeDropdown.AddOptions(resString);
    }
    //Funcion para activar y desactivar el cesped
    public void EnableGrass(Toggle grassToggle)
    {
        if (grassToggle.isOn)
        {
            Terrain.activeTerrain.detailObjectDistance = 80;
        }
        else
        {
            Terrain.activeTerrain.detailObjectDistance = 0;
        }
        
    }

}
