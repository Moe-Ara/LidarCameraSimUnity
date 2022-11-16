using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class movepic : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private RawImage img;
    Vector3 offsetPos; //�洢�������ʱ��ͼƬ-���λ�ò�
    void Start()
    {
        img = GetComponent<RawImage>();//��ȡͼƬ����Ϊ����Ҫ��ȡ����RectTransform
    }
    public void OnDrag(PointerEventData eventData)
    {
        //������λ���������ǯ�ƣ�Ȼ�����λ�ò��ٸ�ֵ��ͼƬposition
        img.rectTransform.position = new Vector3(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width), Mathf.Clamp(Input.mousePosition.y, 0, Screen.height), 0) + offsetPos;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        offsetPos = img.rectTransform.position - Input.mousePosition;
    }
}
