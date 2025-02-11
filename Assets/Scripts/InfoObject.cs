using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class InfoObject : MonoBehaviour
{
    [SerializeField] private GameObject btnSeeDescription;
    bool isShowing = false;
    [SerializeField] Text infoText;
    [TextArea(4, 18)]
    [SerializeField] string contentText;
    [SerializeField] GameObject panelInfoObject;
    [SerializeField] Button btnCloseInfoPanel;
    TvController tvController;
    PlayerMovement playerMovement;
    PlayerCam playerCam;

    void Start()
    {
        infoText.supportRichText = true;
        tvController = GameObject.FindWithTag("Tv").GetComponent<TvController>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerCam = GameObject.FindWithTag("MainCamera").GetComponent<PlayerCam>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowing && Input.GetMouseButtonUp(0))
        {
            infoText.text = contentText;
            panelInfoObject.SetActive(true);
            btnSeeDescription.SetActive(false);
            tvController.isOpenInfo = true;
            isShowing = false;
            playerMovement.MoveAllow();
            playerCam.MouseLocked();
            btnCloseInfoPanel.onClick.AddListener(ClosePanelInfoObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isShowing = true;
            btnSeeDescription.SetActive(isShowing);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isShowing = false;
            btnSeeDescription.SetActive(isShowing);
        }
    }
    void ClosePanelInfoObject()
    {
        infoText.text = "";
        isShowing = false;
        btnSeeDescription.SetActive(isShowing);
        panelInfoObject.SetActive(isShowing);
        tvController.isOpenInfo = false;
        playerMovement.MoveAllow();
        playerCam.MouseLocked();
        btnCloseInfoPanel.onClick.RemoveAllListeners();
    }
}
