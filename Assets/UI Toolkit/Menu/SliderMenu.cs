using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMenu : MonoBehaviour
{
    [SerializeField] public GameObject panelMenu;

    public void ShowHideMenu()
    {
        if(panelMenu != null)
        {
            Animator animator = panelMenu.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
}