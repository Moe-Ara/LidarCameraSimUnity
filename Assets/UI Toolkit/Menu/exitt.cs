using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitt : MonoBehaviour
{
    // Start is called before the first frame update

    public void Click()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�༭״̬���˳�
#else
        Application.Quit();//���������˳�
#endif

    }
}
