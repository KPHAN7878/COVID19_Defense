using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderAnim : MonoBehaviour
{
    public GameObject PanelMenu;
    
    // function to hide and show the sidebar panel when a button is clicked
    public void ShoworHideMenu()
    {
        if(PanelMenu != null)
        {
            Animator animator = PanelMenu.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
}
