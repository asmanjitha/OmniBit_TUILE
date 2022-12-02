﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialButtonBehaviour : MonoBehaviour
{
    public Material material;
    public Texture materialTex;
    public string materialName;
    // Start is called before the first frame update
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        InitiateButton();
    }
    void OnClick(){
        StartCoroutine("UpdateMaterialOnSelectedPlane");
    }

    IEnumerator UpdateMaterialOnSelectedPlane(){
        Debug.Log("Changing Mat");
        GameObject.Find("Tile Checker").GetComponent<SurfaceSelector>().changeMaterial(material);
        yield return null;
    }

    public void InitiateButton(){
        gameObject.transform.SetParent(GameObject.Find("Material Buttons").transform);
        if(materialTex != null && materialName != null){
            gameObject.GetComponentInChildren<RawImage>().texture = materialTex;
            gameObject.GetComponentInChildren<Text>().text = materialName;
        }
    }
}
