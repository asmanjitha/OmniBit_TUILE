using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialButtonBehaviour : MonoBehaviour
{
    public Material material;
    public Texture materialTex;
    public string materialName;
    private Vector2 materialData = new Vector2(1, 1);

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        InitiateButton();
    }
    void OnClick()
    {
        StartCoroutine("UpdateMaterialOnSelectedPlane");
    }

    IEnumerator UpdateMaterialOnSelectedPlane()
    {
        Debug.Log("Changing Mat");
        material.mainTextureScale = materialData;
        GameObject.Find("GeneratedPlane").GetComponent<MeshRenderer>().material = material;
        yield return null;
    }

    public void InitiateButton()
    {
        gameObject.transform.SetParent(GameObject.Find("Material Buttons").transform);
        if (materialTex != null && materialName != null)
        {
            gameObject.GetComponentInChildren<RawImage>().texture = materialTex;
            gameObject.GetComponentInChildren<Text>().text = materialName;
        }
    }
}
