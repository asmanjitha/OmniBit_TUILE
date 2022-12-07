using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LauncherManager : MonoBehaviour
{
    public Sprite openSliderIcon;
    public Sprite closeSliderIcon;
    public Image sliderButtonIcon;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Slider Menu Button").GetComponent<Button>().onClick.AddListener(showHideSlider);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadAR()
    {
        SceneManager.LoadSceneAsync("ARScene");
    }

    public void LoadGallery()
    {
        SceneManager.LoadSceneAsync("Gallery");
    }

    public void LoadStore()
    {
        SceneManager.LoadSceneAsync("Store");
    }

    public void LoadMap()
    {
        SceneManager.LoadSceneAsync("Map");
    }

    public void showHideSlider()
    {
        GameObject panel = GameObject.Find("Slider Menu Panel").transform.GetChild(0).gameObject;
        bool active = panel.activeInHierarchy;
        if (active)
        {
            sliderButtonIcon.sprite = openSliderIcon;
        }
        else
        {
            sliderButtonIcon.sprite = closeSliderIcon;
        }
        panel.SetActive(!active);
        Animator anim = GameObject.Find("Slider Menu Panel").GetComponent<Animator>();
        bool show = anim.GetBool("Show");
        anim.SetBool("Show", !show);
    }

    public void LoadAbout()
    {
        SceneManager.LoadSceneAsync("About");
    }
}
