using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

//Script encargado de de tomar capturas de la pantalla y almacenarlas en el disco duro como fotografías
public class Screenshot : MonoBehaviour
{
    [SerializeField] int resWidth = 0;
    [SerializeField] int resHeight = 0;
    [SerializeField] Camera mainCamera;
    public AudioSource cameraSound;
    public NotificationController nc;
    private bool takeHiResShot = false, isHorizontal = true, isFlashing = false;
    private string notificationText;

    [SerializeField] GameObject luzFlash;
    //Formato de la imagen, idealmente se usa jpg
    public enum ImageFormat {
        jpg,
        png
    }
    //Escalador de la foto
    [SerializeField] int screenshotUpscale = 1;

    //Función para nombrar el archivo que se generará y almacenará
    public static string ScreenShotName(int width, int height){
        if (!Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"/Screenshots")) {
            Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Screenshots");
        }
        return string.Format("{0}/Screenshots/screen_{1}x{2}_{3}.png",
            System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            width, height, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
    //Funcion obsoleta
    public void TakeHiResShot(){
        takeHiResShot = true;
    }
    //Función para activar la luz del flash de la cámara
    public void FlashOn(Toggle tgl) {
        isFlashing = tgl.isOn;
    }
    //Función para tomar el pantallazo con o sin flash, reproducir sonido, almacenarla en local y enviar notificación al sistema encargado.
    public void GetScreenshot() {
        if (isFlashing) StartCoroutine("FlashOff");//Iluminar luz con flash
        cameraSound.Play();
        notificationText = "Fotografia Almacenada en el Directorio Screenshots";
        nc.SendNotification(notificationText);
        if (!takeHiResShot) StartCoroutine(TakeScreenshot(ImageFormat.jpg, screenshotUpscale));
    }
    //Corrutina para obtener la data en pixeles y poder transformarla en un archivo de formato imagen.
    IEnumerator TakeScreenshot(ImageFormat imageFormat, int screenshotUpscale) {
        takeHiResShot = true;
        yield return new WaitForEndOfFrame();
        try {
            RenderTexture rt = new RenderTexture(isHorizontal ? resWidth : resHeight, isHorizontal ? resHeight : resWidth, 24);
            mainCamera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(isHorizontal? resWidth: resHeight, isHorizontal? resHeight:resWidth, TextureFormat.RGB24, false);
            mainCamera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, isHorizontal? resWidth : resHeight, isHorizontal? resHeight:resWidth), 0, 0);
            
            mainCamera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            string fileName = ScreenShotName(resWidth, resHeight);
            System.IO.File.WriteAllBytes(fileName, bytes);
            takeHiResShot = false;
            Destroy(screenShot);
        } catch (System.Exception e) {
            Debug.Log("Original error: " + e.Message);
        }
        takeHiResShot = false;
    }
    //Corrutina para crear el efecto de flash de las cámaras
    IEnumerator FlashOff() {
        luzFlash.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        luzFlash.SetActive(false);
    }
}
