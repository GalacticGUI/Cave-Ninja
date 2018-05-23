using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public GameObject backButton;

    private void Start() {
        EventSystem.current.SetSelectedGameObject(backButton);

        resolutions = Screen.resolutions;                                           // store the currently available resolutions
        resolutionDropdown.ClearOptions();                                          // clear the hardcoded resolutions from the dropdown
        List<string> options = new List<string>();                                  // create a dynamic list of strings
        int currentResolutionIndex = 0;                                             // create an int to store an index for current screen resolution

        for (int i = 0; i < resolutions.Length; i++) {                              // loop through the resolutions array and...
            string option = resolutions[i].width + " x " + resolutions[i].height;   // build strings with the resolution and...
            options.Add(option);                                                    // add them to the options list
            if (resolutions[i].width == Screen.currentResolution.width &&           // compare current indexed resolution...
                resolutions[i].height == Screen.currentResolution.height) {         // to the resolution of the screen...
                currentResolutionIndex = i;                                         // and if it matches, save the index
            }
        }
        resolutionDropdown.AddOptions(options);                                     // add the list of options to the dropdown
        resolutionDropdown.value = currentResolutionIndex;                          // ensure the current screen resolution is selected
        resolutionDropdown.RefreshShownValue();                                     // refresh the UI element to ensure proper display
    }

    private void Update() {
        var selectedObject = EventSystem.current.currentSelectedGameObject;
        if (selectedObject == null)
        {
            EventSystem.current.SetSelectedGameObject(backButton);
        }
    }

    private void OnDisable() {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume) {
        FindObjectOfType<AudioManager>().SetVolume(volume);
    }

    public void SetGraphicsQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

}
