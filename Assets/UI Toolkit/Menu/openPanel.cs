using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openPanel : MonoBehaviour
{
    public GameObject Panel;

    public void Open()
    {
        if (Panel != null)
        {
            Panel.SetActive(true);
        }
    }
    public void Close()
    {
        if (Panel)
        {
            Panel.SetActive(false);
        }
    }
    public void Save()
    {
        if (Panel)
        {
            Panel.SetActive(false);
        }
    }
}
