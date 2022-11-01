using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class togglecamera : MonoBehaviour
{
    //declear toggle component
    [SerializeField] private Toggle m_toggle1;
    [SerializeField] private Toggle m_toggle2;

    //���Ƶ�ͼ�����
    [SerializeField] private RawImage m_image;
    [SerializeField] private GameObject obj;


    void Start()
    {
        //��ÿ��Toggle����϶�Ӧ��ʵ�ֺ���
        m_toggle1.onValueChanged.AddListener(DoChangeImageColor1);
        m_toggle2.onValueChanged.AddListener(DoChangeImageColor2);
       
    }

    //����ʵ�ֺ����Ķ���
    public void DoChangeImageColor1(bool Value)
    {
        if (Value == true)
        {

            m_image.gameObject.SetActive(true);

        }
        else {
            m_image.gameObject.SetActive(false);
        }
    }
    public void DoChangeImageColor2(bool Value)
    {
        if (Value == true)
        {

            obj.gameObject.SetActive(true);

        }
        else
        {
            obj.gameObject.SetActive(false);
        }
    }
   

}