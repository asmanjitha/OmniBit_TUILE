using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ImageDetailsPanelBehaviour : MonoBehaviour
{
    private static string imageName;
    private static string imagePath;
    private static Texture image;
    [SerializeField] private GameObject deleteConfirmationPanel;
    
    public void SetImageName(string name){
        imageName = name;
        gameObject.transform.Find("Name Text (TMP)").GetComponent<TMP_Text>().text = imageName;
    }

    public void SetImage(Texture img){
        image = img;
        gameObject.transform.Find("RawImage").GetComponent<RawImage>().texture = image;
    }

    public void SetImagePath(string path){
        imagePath = path;
    }

    public static void Reset(){
        imageName = null;
        image = null;
    }

    public void ClosePanel(){
        Reset();
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void Share(){
        StartCoroutine(ShareImage());
    }

    private IEnumerator ShareImage()    //Method to share image 
    {
        yield return new WaitForEndOfFrame();
        new NativeShare().AddFile( imagePath )
            .SetSubject( "AR Tile Shop" ).SetText( "Check My New AR Experience." )
            .SetCallback( ( result, shareTarget ) => ShareCallback(result.ToString(), shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }

    private void ShareCallback(string result, string shareTarget){
        ToastMessage.ShowToastMessage("Image shared in " + shareTarget);
    }

    public void Delete(){
        StartCoroutine(DeleteImage());
    }
    IEnumerator DeleteImage()    //Method to delete image 
    {
        File.Delete (imagePath.ToString());
        yield return new WaitForEndOfFrame();
        GameObject.Find("Gallery Manager").GetComponent<GalleryManager>().ReloadGallery();
        deleteConfirmationPanel.transform.GetChild(0).Find("Image Name text").gameObject.GetComponent<Text>().text = null;
        deleteConfirmationPanel.SetActive(false);
        ClosePanel();
    }

    public void ActivateDeleteConfirmation(){
        deleteConfirmationPanel.SetActive(true);
        deleteConfirmationPanel.transform.GetChild(0).transform.Find("Image Name text").gameObject.GetComponent<Text>().text = imageName;
    }

    public void DontDelete(){
        deleteConfirmationPanel.transform.GetChild(0).Find("Image Name text").gameObject.GetComponent<Text>().text = null;
        deleteConfirmationPanel.SetActive(false);
    }
}
