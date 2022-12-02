using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrolViewContainerBehaviour : MonoBehaviour
{
    private GameObject content;
    public enum Constraint{ Columns, Raws };
    public Constraint constrint;
    public int constrintVal;
    public int cellHeight;
    public int cellWidth;
    public int spacingX;
    public int spacingY;
    // Start is called before the first frame update
    void Start()
    {
        // ResizeItemSlot();
        Invoke("ResizeItemSlot", 0.2f);
    }

    // Update is called once per frame
    void OnEnable()
    {
        Invoke("ResizeItemSlot", 0.2f);
    }

    public void ResizeItemSlot(){
        content = gameObject;
        GridLayoutGroup layoutGroup  = gameObject.GetComponent<GridLayoutGroup>();
        spacingX = (int)layoutGroup.spacing.x;
        spacingY = (int)layoutGroup.spacing.y;

        cellHeight = (int)layoutGroup.cellSize.y;
        cellWidth = (int)layoutGroup.cellSize.x;

        constrintVal = layoutGroup.constraintCount;

        int children = content.transform.childCount;
        if(constrint == Constraint.Columns){
            // float val = content.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
            float height = (cellHeight + spacingY) * ((children/constrintVal) + children%constrintVal) + spacingY ;
            RectTransform rt = content.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);
            rt.anchoredPosition = new Vector2(0, -1 * height/2);
        }if(constrint == Constraint.Raws){
            float length = (cellWidth + spacingX) * ((children/constrintVal) + children%constrintVal) + spacingX ;
            RectTransform rt = content.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(length, rt.sizeDelta.y);
            rt.anchoredPosition = new Vector2(length/2, 0);
        }
        
    }


}
