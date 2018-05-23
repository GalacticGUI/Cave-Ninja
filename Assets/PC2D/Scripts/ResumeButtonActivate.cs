using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeButtonActivate : MonoBehaviour {

    public GameObject resumeButton;

    // Use this for initialization
    void Start () {
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    // Update is called once per frame
    void Update () {
        var selectedObject = EventSystem.current.currentSelectedGameObject;
        if (selectedObject == null) {
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }
    }

    private void OnDisable() {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
