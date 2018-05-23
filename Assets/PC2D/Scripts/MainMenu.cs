using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    public GameObject playButton;

    private void Awake() {
        FindObjectOfType<AudioManager>().PlaySound("Music_Menu");
    }

    private void Start() {
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    private void Update() {
        var selectedObject = EventSystem.current.currentSelectedGameObject;
        if (selectedObject == null) {
            EventSystem.current.SetSelectedGameObject(playButton);
        }
    }

    private void OnDisable() {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("Exiting Game!");
        Application.Quit();
    }
}
