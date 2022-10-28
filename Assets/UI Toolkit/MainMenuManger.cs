using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;
using DropdownField = UnityEngine.UIElements.DropdownField;
using Toggle = UnityEngine.UIElements.Toggle;

public class MainMenuManger : MonoBehaviour
{
    private VisualElement rootVisualElement;

    private TextField frequencyLidarTextField;
    private TextField frequencyCameraTextField;

    private Button resetCarButton;
    private Button changeTrackButton;
    private Button updateSettingsButton;
    private Button showCamera;
    public  Dropdown switchtrack;

    private RadioButtonGroup radioButtonGroupMode;

    public GameObject changeTrackDocument;


    private void UpdateSettings()
    {

        var lidarFrequency = Convert.ToInt32(frequencyCameraTextField.text);
        var cameraFrequency = Convert.ToInt32(frequencyLidarTextField.text);

        var mode = (DrivingMode)radioButtonGroupMode.value;

        var output = string.Format("lidar: {0}, camera {1}, drivingMode: {2}", lidarFrequency, cameraFrequency, mode);

        Debug.Log(output);
    }

    private void ChangeTrack()
    {
        changeTrackDocument.SetActive(true);
    }

    private void ResetCar()
    { 
        GameObject car = GameObject.Find("Car");
        car.transform.position = new Vector3(0, 0, 0);
    }


    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        Querries();
        AddListeners();
        // How to set the values, when it comes e.g. from a configuration
        SetFrequencyLidar(42);
        SetFrequencyCamera(69);
        SetDrivingMode(DrivingMode.Autonomous);
    }

    

    private void AddListeners()
    {
        resetCarButton.clicked += ResetCar;
        changeTrackButton.clicked += ChangeTrack;
        updateSettingsButton.clicked += UpdateSettings;
        switchtrack.ClearOptions();
        List<string> options = new  List<string>();
        options.Add("acceleration");
        options.Add("endurance");
        options.Add("skidpad");
        options.Add("customTrack");
        switchtrack.AddOptions(options);
        switchtrack.value = 0;
        switchtrack.onValueChanged.AddListener(index =>
       {
           Debug.Log("Switch Track" + index);
           switch (index)
           {
               case 1:
                   trackManager.SetTrack("endurance");
                   break;
               case 2:
                   trackManager.SetTrack("skidpad");
                   break;
               case 3:
                   trackManager.SetTrack("customTrack");
                   break;
               default:
                   trackManager.SetTrack("acceleration");
                   break;
           }
       });

    }
    public TrackManager trackManager;
  

    /// <summary>
    /// A Method for assigning all the Buttons of the UI Elements
    /// </summary>
    private void Querries()
    {
        resetCarButton = rootVisualElement.Q<Button>("ButtonResetCar");
        changeTrackButton = rootVisualElement.Q<Button>("ButtonSwitchTrack");
        updateSettingsButton = rootVisualElement.Q<Button>("ButtonUpdateSettings");

        frequencyLidarTextField = rootVisualElement.Q<TextField>("FrequencyLidar");
        frequencyCameraTextField = rootVisualElement.Q<TextField>("FrequencyCamera");
        //switchtrack = rootVisualElement.Q<Dropdown>("DropDownFIeld");
        radioButtonGroupMode = rootVisualElement.Q<RadioButtonGroup>("RadioButtonGroupMode");
    }


    public void SetFrequencyLidar(int freq)
    {
        frequencyLidarTextField.value = freq.ToString();
    }

    public void SetFrequencyCamera(int freq)
    {
        frequencyCameraTextField.value = freq.ToString();
    }

    public void SetDrivingMode(DrivingMode mode)
    {
        radioButtonGroupMode.value = (int)mode;
    }
}

public enum DrivingMode
{
    Auto,
    Keyboard,
    Autonomous
}