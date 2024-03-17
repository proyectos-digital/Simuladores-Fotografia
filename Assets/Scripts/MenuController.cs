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

    public Toggle fullScreenToggle;
    public GameObject mainMenuPanel;
    public TMP_Dropdown screenSizeDropdown;
    //public string[] resString;
    public List<string> resString = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        SetNativeResolutions();
        /*foreach (var res in resolutions)
        {
            Debug.Log(res.width + "x" + res.height);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if ((!isMenuActive) && Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = isMenuActive ? CursorLockMode.None : CursorLockMode.Confined;
            mainMenuPanel.SetActive(true);
            Time.timeScale = isMenuActive ? 1 : 0;
        }
    }

    public void ActivateMenu(bool activeMenu)
    {
        //Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = activeMenu ? CursorLockMode.None : CursorLockMode.Confined;
        mainMenuPanel.SetActive(false);
        Time.timeScale = isMenuActive ? 0 : 1;
        //Debug.Log(Time.timeScale);
    }
    public bool SetFullScreen()
    {
        // Screen.fullScreen = !Screen.fullScreen;
        return false;
    }
    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ChangeResolution()
    {

    }

    private void SetNativeResolutions()
    {
        Resolution[] resolutions = Screen.resolutions;
        
        foreach (var res in resolutions)
        {
            resString.Add(res.width.ToString() + "x" + res.height.ToString());
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


}
