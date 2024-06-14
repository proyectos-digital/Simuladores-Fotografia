using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using TMPro;

public class MenuController : MonoBehaviour
{
    private bool isMenuActive = false;
    private bool isInfoActive = true;
    private char[] delimiterChars = { 'x', ' '};
    private string screenResolutionSelected;
    public Toggle fullScreenToggle;
    public Toggle grassEnabled;
    public GameObject mainMenuPanel;
    public GameObject mainOptionsPanel;
    public GameObject controlOptionsPanel;
    public GameObject infoMenuPanel;
    public TMP_Dropdown screenSizeDropdown;
    //public string[] resString;
    public List<string> resString = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        //ActivateMenuInfo();
        SetNativeResolutions();
        screenSizeDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(screenSizeDropdown);
        });

    }

    // Update is called once per frame
    void Update()
    {
        if ((!isMenuActive) && Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = isMenuActive ? CursorLockMode.None : CursorLockMode.Confined;
            mainMenuPanel.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = isMenuActive ? 1 : 0;
        }

        if (isInfoActive)
        {
            infoMenuPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        screenResolutionSelected = dropdown.options[dropdown.value].text;
        string[] words = screenResolutionSelected.Split(delimiterChars);
        foreach (var word in words)
        {
            Debug.Log(word);
        }
        //Debug.Log(screenResolutionSelected);
        int resx = Int32.Parse(words[0]);
        //Debug.Log(resx);
        int resy = Int32.Parse(words[1]);
        //Debug.Log(resy);
        if (fullScreenToggle.isOn)
        {
            Screen.SetResolution(resx, resy, true);
        }
        else
        {
            Screen.SetResolution(resx, resy, false);
        }
    }
    public void ActivateMenu(bool activeMenu)
    {
        //Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = activeMenu ? CursorLockMode.None : CursorLockMode.Confined;
        mainMenuPanel.SetActive(false);
        Cursor.visible = isMenuActive;
        Time.timeScale = isMenuActive ? 0 : 1;
        //Debug.Log(Time.timeScale);
    }

    public void ActivateMenuInfo()
    {
        
        //Cursor.lockState = CursorLockMode.Confined;
        
    }

    public void DisableinfoPanel()
    {
        isInfoActive = false;
        infoMenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }

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

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ChangeResolution(string resSelected)
    {
        //se necesita resolucion
        //necesita ver si es full screen o no
        
    }

    private void SetNativeResolutions()
    {
        Resolution[] resolutions = Screen.resolutions;
        
        foreach (var res in resolutions)
        {
            resString.Add(res.width.ToString() + "x" + res.height.ToString() +" "+ res.refreshRateRatio.ToString() + "hz");
        }
        screenSizeDropdown.ClearOptions();
        screenSizeDropdown.AddOptions(resString);
        //string[] stringResolutions;
        /*for (int i = 0; i < resolutions.length; i++)
        {
            string newResolution = resolutions[i].width + "x" + resolutions[i].height;
            Debug.Log(newResolution);
        }*/
        //Debug.Log(resolutions[2].width + "x" + resolutions[2].height);
        /*foreach (var res in resolutions)
        {
            Debug.Log(res.width + "x" + res.height);
        }*/
    }

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
