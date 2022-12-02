using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] UIs;
    [SerializeField] private GameObject trackingUI;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject ui in UIs){
            ui.SetActive(false);
        }
        trackingUI.SetActive(true);
    }

    public void close(){
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_ANDROID) 
            Application.Quit();
        #elif (UNITYIOS)
            Application.Quit();
        #endif
    }

    public void ReloadARScene(){
        foreach(GameObject ui in UIs){
            ui.SetActive(false);
        }
        SceneManager.LoadSceneAsync("ARScene");
    }

    public void LoadMaterialButtons(){
        Debug.Log("Loading Buttons");
        foreach(string material in Data.getMaterials()){
            GameObject button = Resources.Load<GameObject>("MaterialButton/MaterialButton");
            GameObject newButton = Instantiate(button);
            MaterialButtonBehaviour buttonBehaviour = newButton.GetComponent<MaterialButtonBehaviour>();
            buttonBehaviour.materialName = material;
            buttonBehaviour.materialTex = Resources.Load<Texture>("Tile Materials/" + material + "/" + material);
            buttonBehaviour.material = Resources.Load<Material>("Tile Materials/" + material + "/" + material + "_MAT");
            buttonBehaviour.InitiateButton();
        }
    }

    public void LoadGallery(){
        SceneManager.LoadSceneAsync("Gallery");
    }
}
