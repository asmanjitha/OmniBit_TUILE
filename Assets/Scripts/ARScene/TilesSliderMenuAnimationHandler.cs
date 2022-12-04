using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilesSliderMenuAnimationHandler : MonoBehaviour
{
    public GameObject sliderMenu;
    public Sprite forewardButtonSprite;
    public Sprite backwardButtonSprite;
    public Image sliderButtonIcon;

    public void ShowHideMenu()
    {
        if (sliderMenu != null)
        {
            Animator animator = sliderMenu.GetComponent<Animator>();

            if (animator != null)
            {
                bool showHide = animator.GetBool("Show");
                animator.SetBool("Show", !showHide);

                if (showHide)
                {
                    sliderButtonIcon.GetComponent<Image>().sprite = forewardButtonSprite;
                }
                else
                {
                    sliderButtonIcon.GetComponent<Image>().sprite = backwardButtonSprite;
                }

            }
        }
    }


}
