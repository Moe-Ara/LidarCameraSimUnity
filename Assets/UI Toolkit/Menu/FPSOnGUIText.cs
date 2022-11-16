using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FPS ��ʾ��OnGUI 
/// </summary>
public class FPSOnGUIText : MonoBehaviour
{

    float updateInterval = 1.0f;           //��ǰʱ����
    private float accumulated = 0.0f;      //�ڴ��ڼ��ۻ�  
    private float frames = 0;              //�ڼ���ڻ��Ƶ�֡  
    private float timeRemaining;           //��ǰ�����ʣ��ʱ��
    private float fps = 15.0f;             //��ǰ֡ Current FPS
    private float lastSample;

    public float FPS
    {
        get => fps;
        set => fps = value;
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject); //�����ٴ���Ϸ�������ĸ�������������ʾ��������Ҫ��ע��
        timeRemaining = updateInterval;
        lastSample = Time.realtimeSinceStartup; //ʵʱ������
    }

    void Update()
    {
        ++frames;
        float newSample = Time.realtimeSinceStartup;
        float deltaTime = newSample - lastSample;
        lastSample = newSample;
        timeRemaining -= deltaTime;
        accumulated += 1.0f / deltaTime;

        if (timeRemaining <= 0.0f)
        {
            fps = accumulated / frames;
            timeRemaining = updateInterval;
            accumulated = 0.0f;
            frames = 0;
        }
    }

    // void OnGUI()
    // {
    //     GUIStyle style = new GUIStyle
    //     {
    //         border = new RectOffset(10, 10, 10, 10),
    //         fontSize = 50,
    //         fontStyle = FontStyle.BoldAndItalic,
    //     };
    //     //�Զ����� ���߶ȴ�С ��ɫ��style
    //     GUI.Label(new Rect(Screen.width / 2 + 620, Screen.height - 1000, 200, 200), "<color=#FFFFFF><size=30>" + "FPS:" + fps.ToString("f2") + " s </size></color>", style);
    // }
}

