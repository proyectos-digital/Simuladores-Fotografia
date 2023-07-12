using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;

public class Screenshot : MonoBehaviour
{
    public int resWidth = 0;
    public int resHeight = 0;
    public Camera mainCamera;

    private bool takeHiResShot = false;

    //public PhotoInventory photoInventory;
    public string fieldName;
    public string serverUrl;

    public GameObject luzFlash;
    public TMP_Text txtItem;
    public enum ImageFormat {
        jpg,
        png
    }

    public int screenshotUpscale = 1;

    public static string ScreenShotName(int width, int height){
        if (!Directory.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"/Screenshots")) {
            Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Screenshots");
        }
        return string.Format("{0}/Screenshots/screen_{1}x{2}_{3}.png",
            System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            width, height,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TakeHiResShot()
    {
        takeHiResShot = true;
    }

    void LateUpdate() {
        //takeHiResShot |= Input.GetKeyUp(KeyCode.P); //Input.GetKeyDown("k");
        if (Input.GetKeyUp(KeyCode.K) || takeHiResShot) {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            mainCamera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            mainCamera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            mainCamera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            string fileName = ScreenShotName(resWidth, resHeight);
            System.IO.File.WriteAllBytes(fileName, bytes);
            txtItem.text = "se tomo la foto: "+fileName;
            Debug.Log(string.Format("Took screenshot to: {0}", fileName));
            takeHiResShot = false;

        }
    }

    public void GetScreenshot() {
        //Iluminar luz con flash
        luzFlash.SetActive(true);
        takeHiResShot = true;

        StartCoroutine("FlashOff");
        //_webGLDownload.GetScreenshot(WebGLDownload.ImageFormat.jpg, 1, "");
        //if (!takeHiResShot) StartCoroutine(RecordUpscaledFrame(ImageFormat.jpg, screenshotUpscale, ""));
    }
    //Funcion sin usar 
    IEnumerator RecordUpscaledFrame(ImageFormat imageFormat, int screenshotUpscale, string fileName) {
        takeHiResShot = true;
        yield return new WaitForEndOfFrame();
        try {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            mainCamera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            mainCamera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            mainCamera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToJPG();
            string dateFormat = "yyyy-MM-dd-HH-mm-ss";
            fileName = resWidth.ToString() + "x" + resHeight.ToString() + "px_" + System.DateTime.Now.ToString(dateFormat);
            //photoInventory.GetTexture(rt);
            if (fileName == "") {
                int resWidth = mainCamera.pixelWidth * screenshotUpscale;
                int resHeight = mainCamera.pixelHeight * screenshotUpscale;
            }
            //Funcion para subir imagen a servidor... SIN USO
            //Texture2D screenShot = ScreenCapture.CaptureScreenshotAsTexture(screenshotUpscale);
            //ImageUploader.Initialize()
            //    .SetUrl(serverUrl)
            //    .SetTexture(screenShot)
            //    //.SetBytes(bytes)
            //    .SetFieldName(fieldName)
            //    .SetFileName(fileName)
            //    .SetType(ImageType.JPG)
            //    .OnError(error => Debug.Log(error))
            //    .OnComplete(text => Debug.Log(text))
            //    .Upload();
            //if (imageFormat == ImageFormat.jpg) DownloadFile({{..EncodeToJPG(), fileName, "jpg");
            //else if (imageFormat == ImageFormat.png) DownloadFile(screenShot.EncodeToPNG(), fileName, "png");
            Destroy(screenShot);
        } catch (System.Exception e) {
            Debug.Log("Original error: " + e.Message);
        }
        takeHiResShot = false;
        StartCoroutine("FlashOff");
    }
    IEnumerator FlashOff() {
        yield return new WaitForSeconds(0.5f);
        luzFlash.SetActive(false);
    }
}
