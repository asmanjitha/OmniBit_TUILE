using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ImageCardBehaviour : MonoBehaviour
{

    private string imageName;
    private Texture image;
    private string imagePath;
    
    void OnClick(){
        Debug.Log("Onclick running");
        GalleryManager.ActivateImageDetailsPanel(this);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => { GalleryManager.ActivateImageDetailsPanel(this);});
    }

    public void SetImageName(string name){
        imageName = name;
        gameObject.transform.Find("ImageNameTMPro").GetComponent<TMP_Text>().text = imageName;
    }

    public void SetImage(Texture img){
        image = img;
        gameObject.transform.Find("RawImage").GetComponent<RawImage>().texture = image;
    }

    public void SetImagePath(string path){
        imagePath = path;
    }

    public Texture GetImage(){
        return image;
    }

    public string GetImageName(){
        return imageName;
    }

    public string GetImagePath(){
        return imagePath;
    }
}
