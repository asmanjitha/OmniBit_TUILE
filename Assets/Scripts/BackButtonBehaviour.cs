using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButtonBehaviour : MonoBehaviour
{
    [SerializeField] private string previousScene;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(GoBack);
    }
    public void GoBack(){
        SceneManager.LoadSceneAsync(previousScene);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
            
            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape)) {
                
                SceneManager.LoadSceneAsync(previousScene);

            }
        }
    }
}
