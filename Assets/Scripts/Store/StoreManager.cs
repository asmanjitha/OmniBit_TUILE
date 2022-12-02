using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GotoAR View Button").GetComponent<Button>().onClick.AddListener(GoToAR);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GoToAR(){
        SceneManager.LoadSceneAsync("ARScene");
    }
}
