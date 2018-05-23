using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class PauseMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public float ButtonThreshold;
    public Selectable Resume;
    public Selectable Menu;
    public Selectable Exit;
    Selectable SelectedButton;
    List<Selectable> buttons;
    int selectedButtonIndex;

    //private void Start()
    //{
    //    buttons = new List<Selectable>();
    //    buttons.Add(Resume);
    //    buttons.Add(Menu);
    //    buttons.Add(Exit);
    //    selectedButtonIndex = 0;
    //    SelectedButton = buttons[selectedButtonIndex];
    //    eventSystem.SetSelectedGameObject(SelectedButton.gameObject, new BaseEventData(eventSystem));
    //}

    void Update() {
        if (Input.GetButtonDown(PC2D.Input.PAUSE)) {
            if (isPaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
        if (isPaused) {
            //float v = Input.GetAxisRaw("Vertical");

            //if (v > ButtonThreshold) {
            //    if (selectedButtonIndex > 0) {
            //        selectedButtonIndex = selectedButtonIndex - 1;                  
            //    }
            //}
            //else if (v < -ButtonThreshold) {
            //    if (selectedButtonIndex < buttons.Count - 1) {
            //        selectedButtonIndex = selectedButtonIndex + 1;                      
            //    }
            //}
            //SelectedButton = buttons[selectedButtonIndex];
            //eventSystem.SetSelectedGameObject(SelectedButton.gameObject, new BaseEventData(eventSystem));


            //var v = Input.GetButtonDown("Vertical"); // Or "Vertical"

            //if (Math.Abs(v) > ButtonThreshold)
            //{
            //    var currentlySelected = eventSystem.currentSelectedGameObject
            //        ? eventSystem.currentSelectedGameObject.GetComponent<Selectable>()
            //        : FindObjectOfType<Selectable>();

            //    Selectable nextToSelect = null;

            //    if (v > ButtonThreshold)
            //    {
            //        nextToSelect = currentlySelected.FindSelectableOnUp();
            //    }
            //    else if (v < -ButtonThreshold)
            //    {
            //        nextToSelect = currentlySelected.FindSelectableOnDown();
            //    }

            //    if (nextToSelect)
            //    {
            //        eventSystem.SetSelectedGameObject(nextToSelect.gameObject);
            //    }
            //}
        }
    }

    public void ResumeGame() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame() {       
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }

    public void QuitGame() {
        Debug.Log("Exiting Game!");
        Application.Quit();
    }
}
