using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class WebGLDownload : MonoBehaviour
{
    public PhotoInventory photoInventory;
    public Camera mainCamera;
    public string fieldName;//string fieldName;
    public string serverUrl;
    public int resWidth = 0;
    public int resHeight = 0;

    public GameObject luzFlash;
    public enum ImageFormat
    {
        jpg,
        png
    }
    private bool _isRecording = false;
    [DllImport("__Internal")]
    private static extern void DownloadFileJsLib(byte[] byteArray, int byteLength, string fileName);

    /// <summary>
    /// ___
    /// <para>bytes -> The bytes to be downloaded</para>
    /// <para>fileName -> The downloaded file name (without extension)</para>
    /// <para>fileExtension -> WebGLDownload.FileExtension.jpg/png/zip/</para>
    /// </summary>
    public void DownloadFile(byte[] bytes, string fileName, string fileExtension)
    {
        if (fileName == "") fileName = "UnnamedFile";
#if UNITY_EDITOR
        string path = UnityEditor.EditorUtility.SaveFilePanel("Save file...", "", fileName, fileExtension);
        System.IO.File.WriteAllBytes(path, bytes);
        Debug.Log("File saved: " + path);
#elif UNITY_WEBGL
        DownloadFileJsLib(bytes, bytes.Length, fileName + "." + fileExtension);
#endif
    }

    /// <summary>
    /// ___
    /// <para>imageFormat -> WebGLDownload.ImageFormat.jpg/png</para>
    /// <para>screenshotUpscale -> Upscale the frame. default = 1</para>
    /// <para>fileName -> Optional filename. Empty filename creates a name texture.width x texture.height in pixel + current datetime</para>
    /// </summary>
    public void GetScreenshot(ImageFormat imageFormat, int screenshotUpscale, string fileName = "")
    {
        //Iluminar luz con flash
        luzFlash.SetActive(true);
        if (!_isRecording) StartCoroutine(RecordUpscaledFrame(imageFormat, screenshotUpscale, fileName));
    }

    IEnumerator RecordUpscaledFrame(ImageFormat imageFormat, int screenshotUpscale, string fileName)
    {
        _isRecording = true;
        yield return new WaitForEndOfFrame();
        try
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            mainCamera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            mainCamera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            mainCamera.targetTexture = null;
            //RenderTexture.active = null;
            //Destroy(rt);
            //byte[] bytes = screenShot.EncodeToJPG();
            string dateFormat = "yyyy-MM-dd-HH-mm-ss";
            fileName = resWidth.ToString() + "x" + resHeight.ToString() + "px_" + System.DateTime.Now.ToString(dateFormat);
            photoInventory.GetTexture(rt);
            RenderTexture.active = null;
            //Destroy(rt);
            if (fileName == ""){
                int resWidth = mainCamera.pixelWidth * screenshotUpscale;
                int resHeight = mainCamera.pixelHeight * screenshotUpscale;
            }
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
            Object.Destroy(screenShot);
        }
        catch (System.Exception e)
        {
            Debug.Log("Original error: " + e.Message);
        }
        _isRecording = false;
        StartCoroutine("FlashOff");
    }
    IEnumerator FlashOff() {
        yield return new WaitForSeconds(0.5f);
        luzFlash.SetActive(false);
    }
}