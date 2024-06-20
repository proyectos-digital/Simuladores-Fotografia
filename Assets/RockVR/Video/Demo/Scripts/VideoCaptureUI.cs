﻿using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using TMPro;
using RockVR.Common;
using static RockVR.Video.VideoCaptureBase;
using System.Threading;
using UnityEngine.Playables;
//using UnityEngine.Windows.WebCam;
using System.Collections;

namespace RockVR.Video.Demo
{
    public class VideoCaptureUI : MonoBehaviour
    {
        private bool isPlayVideo = false;

        //Inicio de variables
        //public GameObject[] uiDesact; OBSOLETAS
        //public GameObject[] uiActiv; OBSOLETAS
        //public PlayerController plCtrl; MODIFICAR EN SU MOMENTO
        public Button btnRecord;
        public Image imgRecord;
        public Color32 colorTest;
        [Header("Textos")]
        public TMP_Text txtTiempo;
        public TMP_Text txtProceso;
        private float tiempo;
        private bool tiempoCorriendo;
        [Header("UI OBSOLETO")]
        public GameObject btnAbrirFolder;
        public GameObject btnCerrar;
        [Header("Cámaras")]
        public Camera camPrincipal;
        public Camera camAuxiliar;
        public Camera camJugador;

        [SerializeField]
        private NotificationController nc;
        private string message;

        void Update()
        {
            if (tiempoCorriendo)
            {
                tiempo += Time.deltaTime;
                txtTiempo.text = "Tiempo de la Grabación " + tiempo.ToString("F2");
            }
        }

        private void Awake()
        {
            //Application.runInBackground = true;
            isPlayVideo = false;
        }
        
        public void StartRecord()
        {
            VideoCaptureCtrl.instance.StartCapture();
            tiempoCorriendo = true;
            btnRecord.onClick.RemoveAllListeners();
            btnRecord.onClick.AddListener(StopRecord);
            imgRecord.color = colorTest;
            //POSIBLEMENTE OBSOLETO
            /*for (int i = 0; i < uiActiv.Length; i++)
            {
                uiActiv[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < uiDesact.Length; i++)
            {
                uiDesact[i].gameObject.SetActive(false);
            }*/
            camPrincipal.targetDisplay = 1;
            camAuxiliar.targetDisplay = 1;
            camJugador.targetDisplay = 0;
        }

        public void StopRecord()
        {
            VideoCaptureCtrl.instance.StopCapture();
            btnRecord.onClick.RemoveAllListeners();
            btnRecord.onClick.AddListener(StartRecord);
            imgRecord.color = Color.white;
            if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH)
            {
                if (!isPlayVideo)
                { }
            }
            //OBSOLETOS
            /*for (int i = 0; i < uiActiv.Length; i++)
            {
                uiActiv[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < uiDesact.Length; i++)
            {
                uiDesact[i].gameObject.SetActive(true);
            }*/
            tiempoCorriendo = false;
            tiempo = 0f;                            //Reinicia el tiempo
            txtTiempo.text = "";
            //plCtrl.LockCursor();
            StartCoroutine("processVideo");
        }

        IEnumerator processVideo()
        {
            message = "Procesando video, por favor espere..";
            nc.SendNotification(message);
            //Obsoleto
            //txtProceso.text = "FIN DE LA GRABACIÓN\r\nProcesando video, por favor espere..";
            //btnAbrirFolder.SetActive(false);
            //btnCerrar.SetActive(false);
            yield return new WaitForSeconds(4f);
            message = "VIDEO LISTO\r\nAlmacenado en Documentos/RockVR";
            nc.SendNotification(message);
            //Obsoleto
            //txtProceso.text = "VIDEO LISTO\r\n¿Ver archivo?";
            //btnAbrirFolder.SetActive(true);
            //btnCerrar.SetActive(true);
        }

        public void OpenFolder()
        {
            Process.Start(PathConfig.saveFolder);
        }


        /*
        private void OnGUI()
        {
            if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.NOT_START)
            {
                if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "Start Capture"))
                {
                    VideoCaptureCtrl.instance.StartCapture();
                }
            }
            else if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STARTED)
            {
                if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "Stop Capture"))
                {
                    VideoCaptureCtrl.instance.StopCapture();
                }
                if (GUI.Button(new Rect(180, Screen.height - 60, 150, 50), "Pause Capture"))
                {
                    VideoCaptureCtrl.instance.ToggleCapture();
                }
            }
            else if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.PAUSED)
            {
                if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "Stop Capture"))
                {
                    VideoCaptureCtrl.instance.StopCapture();
                }
                if (GUI.Button(new Rect(180, Screen.height - 60, 150, 50), "Continue Capture"))
                {
                    VideoCaptureCtrl.instance.ToggleCapture();
                }
            }
            else if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STOPPED)
            {
                if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "Processing"))
                {
                    // Waiting processing end.
                }
            }
            else if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH)
            {
                if (!isPlayVideo)
                {
                    if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "View Video"))
                    {
#if UNITY_5_6_OR_NEWER
                        // Set root folder.
                        isPlayVideo = true;
                        VideoPlayer.instance.SetRootFolder();
                        // Play capture video.
                        VideoPlayer.instance.PlayVideo();
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "Next Video"))
                    {
                        // Turn to next video.
                        VideoPlayer.instance.NextVideo();
                        // Play capture video.
                        VideoPlayer.instance.PlayVideo();
#else
                        // Open video save directory.
                        Process.Start(PathConfig.saveFolder);
#endif
                    }
                }
            }
        }*/
    }
}