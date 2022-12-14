using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;
using DropdownField = UnityEngine.UIElements.DropdownField;
using Toggle = UnityEngine.UIElements.Toggle;

public class MainMenuManger : MonoBehaviour
{
    public GameObject car;
    public TrackManager trackManager;
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
    private List<string> options;
    

    public void Start()
    {
        options= new List<string>(trackManager.Tracks.Count);
        addTracksToDropDown();
    }

    private void addTracksToDropDown()
    {
        
        options.AddRange(trackManager.Tracks.Select(track => track.name));
        switchtrack.ClearOptions();
        switchtrack.AddOptions(options);
        switchtrack.value = 0;
    }

    public void AddNewTrack(string trackName)
    {
        if(options==null || options.Contains(trackName))return;
        options.Add(trackName);
        switchtrack.AddOptions(new List<string>(){trackName});
    }

    public void DeleteTracks()
    {
        options.Clear();
        switchtrack.ClearOptions();
    }
    
    private void UpdateSettings()
    {

        var lidarFrequency = Convert.ToInt32(frequencyCameraTextField.text);
        var cameraFrequency = Convert.ToInt32(frequencyLidarTextField.text);

        var mode = (DrivingMode)radioButtonGroupMode.value;

        var output = string.Format("lidar: {0}, camera {1}, drivingMode: {2}", lidarFrequency, cameraFrequency, mode);


    }

    private void ChangeTrack()
    {
        changeTrackDocument.SetActive(true);
    }

    private void ResetCar()
    {
        //Work on this (if autonomous or Keyboard)
        car.GetComponent<CarController>().ResetCar();
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
        //This changes tracks 
        switchtrack.onValueChanged.AddListener(value =>
        {
            trackManager.SetTrack(switchtrack.options[value].text);
        });
    }
    
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
        radioButtonGroupMode = rootVisualElement.Q<RadioButtonGroup>("RadioButtonGroupMode");
    }


    private void SetFrequencyLidar(int freq)
    {
        frequencyLidarTextField.value = freq.ToString();
    }

    private void SetFrequencyCamera(int freq)
    {
        frequencyCameraTextField.value = freq.ToString();
    }

    private void SetDrivingMode(DrivingMode mode)
    {
        radioButtonGroupMode.value = (int)mode;
    }
    
    private enum DrivingMode
    {
        Auto,
        Keyboard,
        Autonomous
    }
}

