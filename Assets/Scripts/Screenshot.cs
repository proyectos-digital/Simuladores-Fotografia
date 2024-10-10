using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
//using TMPro;

public class Screenshot : MonoBehaviour
{
    [SerializeField] int resWidth = 0;
    [SerializeField] int resHeight = 0;
    [SerializeField] Camera mainCamera;
    public AudioSource cameraSound;
    public NotificationController nc;
    //bool m_Play;
    private bool takeHiResShot = false, isHorizontal = true, isFlashing = false;
    private string notificationText;

    //public PhotoInventory photoInventory;
    //public string fieldName;
    //public string serverUrl;

    [SerializeField] GameObject luzFlash, imgHorizontal, imgVertical;
    public enum ImageFormat {
        jpg,
        png
    }

    [SerializeField] int screenshotUpscale = 1;

    public delegate void cameraOrientations(bool isHorizontal);
    public event cameraOrientations cameraOrientation;

    public static string ScreenShotName(int width, int height){
        if (!Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"/Screenshots")) {
            Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Screenshots");
        }
        return string.Format("{0}/Screenshots/screen_{1}x{2}_{3}.png",
            System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            width, height, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TakeHiResShot(){
        takeHiResShot = true;
    }

    public void ChangeOrientation() {
        isHorizontal = !isHorizontal;
        imgHorizontal.SetActive(!imgHorizontal.activeSelf);
        imgVertical.SetActive(!imgVertical.activeSelf);
        cameraOrientation(isHorizontal);
    }

    public void FlashOn(Toggle tgl) {
        isFlashing = tgl.isOn;
    }

    public void GetScreenshot() {
        //Iluminar luz con flash
        if (isFlashing) StartCoroutine("FlashOff");
        cameraSound.Play();
        notificationText = "Fotografia Almacenada en el Directorio Screenshots";
        nc.SendNotification(notificationText);
        //_webGLDownload.GetScreenshot(WebGLDownload.ImageFormat.jpg, 1, "");
        if (!takeHiResShot) StartCoroutine(TakeScreenshot(ImageFormat.jpg, screenshotUpscale));
    }

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
            //Debug.Log(string.Format("Took screenshot to: {0}", fileName));
            takeHiResShot = false;
            Destroy(screenShot);
        } catch (System.Exception e) {
            Debug.Log("Original error: " + e.Message);
        }
        takeHiResShot = false;
    }

    IEnumerator FlashOff() {
        luzFlash.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        luzFlash.SetActive(false);
    }
}
