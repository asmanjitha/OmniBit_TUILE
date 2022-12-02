using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GalleryManager : MonoBehaviour
{
    [SerializeField] private GameObject imageCard;
    private Transform container;
    [SerializeField] private static GameObject imageDetailsPanel;

    // Start is called before the first frame update
    void Start()
    {
        container = GameObject.Find("Image Panel Container").transform;
        StartCoroutine(LoadImages());
        imageDetailsPanel = GameObject.Find("ImageDetailsPanel");
        if(imageDetailsPanel != null){
            imageDetailsPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator LoadImages(){
        string myPath = Application.persistentDataPath + "/Screenshots/";
        Debug.Log(Application.persistentDataPath);
        DirectoryInfo dir = new DirectoryInfo(myPath);
        FileInfo[] fileInfos = dir.GetFiles("*.png");
        foreach (FileInfo f in fileInfos){

            GameObject image = Instantiate(imageCard);
            image.transform.SetParent(container);

            string filename = f.ToString();
            image.GetComponent<ImageCardBehaviour>().SetImagePath(filename);
            image.GetComponent<ImageCardBehaviour>().SetImageName(filename.Replace(Application.persistentDataPath + "/Screenshots/", ""));

            byte[] byteArray = File.ReadAllBytes(filename);
            Texture2D texTmp = new Texture2D(400, 650);
            texTmp.LoadImage(byteArray);
            image.GetComponent<ImageCardBehaviour>().SetImage(texTmp);          

        }
        yield return new WaitForSeconds(1);
    }

    public static void ActivateImageDetailsPanel(ImageCardBehaviour imageCard){
        GameObject ui = imageDetailsPanel.transform.Find("ImageDetails").gameObject;
        ui.GetComponent<ImageDetailsPanelBehaviour>().SetImage(imageCard.GetImage());
        ui.GetComponent<ImageDetailsPanelBehaviour>().SetImageName(imageCard.GetImageName());
        ui.GetComponent<ImageDetailsPanelBehaviour>().SetImagePath(imageCard.GetImagePath());
        imageDetailsPanel.SetActive(true);
    }

    public void ReloadGallery(){
        // foreach(GameObject child in gameObject.transform)
        // {
        //     Destroy(child);
        // }
        // StartCoroutine(LoadImages());
        SceneManager.LoadSceneAsync("Gallery");
    }
}
