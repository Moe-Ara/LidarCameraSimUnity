using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeTrack : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private Button buttonBack;

    public TrackManager trackManager;

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        buttonBack = rootVisualElement.Q<Button>("ButtonBack");

  
        var buttonAcceleration = rootVisualElement.Q<Button>("ButtonAcceleration");
        var buttonSkidpad = rootVisualElement.Q<Button>("ButtonSkidpad");
        var buttonEndurance = rootVisualElement.Q<Button>("ButtonEndurance");
        
        var buttonCustom = rootVisualElement.Q<Button>("Buttoncustom");
        buttonCustom.clicked += StartCustomTrack;
        
        buttonAcceleration.clicked += StartAccerleration;
        buttonSkidpad.clicked += StartSkidpad;
        buttonEndurance.clicked += StartEndurance;

        buttonBack.clicked += () => { gameObject.SetActive(false); };
    }

    private void StartEndurance()
    {
        trackManager.SetTrack("endurance");
    }

    private void StartSkidpad()
    {
        trackManager.SetTrack("skidpad");
    }

    private void StartAccerleration()
    {
        trackManager.SetTrack("acceleration");
    }

    private void StartCustomTrack()
    {
        trackManager.SetTrack("customTrack");
    }
}