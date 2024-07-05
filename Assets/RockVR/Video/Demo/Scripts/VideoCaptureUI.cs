using UnityEngine;
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
        //public PlayerController plCtrl; MODIFICAR EN SU MOMENTO
        [SerializeField] Button btnRecord;
        [SerializeField] GameObject imgStopRecord;
        [Header("Textos")]
        [SerializeField] GameObject btnAbrirFolder;
        public TMP_Text txtTiempo;
        private float tiempo;
        private bool tiempoCorriendo;
        [Header("Cámaras")]
        public Camera camPrincipal;
        public Camera camAuxiliar;
        public Camera camJugador;

        [SerializeField] private NotificationController nc;
        [SerializeField] private string message;
        [SerializeField] AIPatrolNosferatu aiPatrolNosferatu;


        private void Awake()
        {
            isPlayVideo = false;
        }
        void Update()
        {
            if (tiempoCorriendo)
            {
                tiempo += Time.deltaTime;
                txtTiempo.text = "Tiempo de la Grabación " + tiempo.ToString("F2");
            }
        }

        public void StartRecord()
        {
            VideoCaptureCtrl.instance.StartCapture();
            tiempoCorriendo = true;
            btnRecord.onClick.RemoveAllListeners();
            btnRecord.onClick.AddListener(StopRecord);
            imgStopRecord.SetActive(true);
        }

        public void StopRecord()
        {
            VideoCaptureCtrl.instance.StopCapture();
            btnRecord.onClick.RemoveAllListeners();
            btnRecord.onClick.AddListener(StartRecord);
            imgStopRecord.SetActive(false);
            if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH)
            {
                if (!isPlayVideo)
                { }
            }
            tiempoCorriendo = false;
            tiempo = 0f;                            //Reinicia el tiempo
            txtTiempo.text = "";
            StartCoroutine("processVideo");
        }

        IEnumerator processVideo()
        {
            message = "<size=30>Procesando video,\n Por favor espere...</size>";
            nc.SendNotification(message);
            yield return new WaitForSeconds(4f);
            message = "<size=30>VIDEO LISTO\nAlmacenado en Documentos/RockVR</size>";
            nc.SendNotification(message);
            if (aiPatrolNosferatu != null)
            {
                aiPatrolNosferatu.ResetAnimation();
            }
        }

        public void OpenFolder()
        {
            Process.Start(PathConfig.saveFolder);
        }
    }
}