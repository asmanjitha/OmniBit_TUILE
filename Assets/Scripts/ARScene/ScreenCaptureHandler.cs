using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenCaptureHandler : MonoBehaviour
{
    public void OnClickScreencaptureButton()
    {
        StartCoroutine(CaptureScreen());
    }
    public IEnumerator CaptureScreen()
    {
        string dirPath = Application.persistentDataPath + "/Screenshots";
        if (Directory.Exists (dirPath) == false) {
             Directory.CreateDirectory(dirPath);
         } 

        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "screenshot" + timeStamp + ".png"; 
        string pathToSave = "Screenshots/" + fileName;
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
    
        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();
    
        // Take screenshot
        ScreenCapture.CaptureScreenshot(pathToSave);
        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        ToastMessage.ShowToastMessage("Screenshot Captured");
    
        // Show UI after we're done
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;

    }

}
