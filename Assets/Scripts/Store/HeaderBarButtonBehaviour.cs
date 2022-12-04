using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderBarButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Obsolete]
    public void ButtonPressed()
    {
        NeutralizedOtherButtons();
        Text text = gameObject.GetComponent<Text>();
        // text.color = new Color32(0,0,0,255);
        text.color = new Color32(255, 255, 255, 255);
        text.fontStyle = FontStyle.Bold;
        // gameObject.transform.FindChild("Underline Panel").gameObject.SetActive(true);
    }

    public void NeutralizedOtherButtons()
    {
        int childCount = gameObject.transform.parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform button = gameObject.transform.parent.GetChild(i);
            Text text = button.GetComponent<Text>();
            text.color = new Color32(255, 255, 255, 255);
            text.fontStyle = FontStyle.Normal;
            // button.GetChild(1).gameObject.SetActive(false);
        }
    }
}
