using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Transform playButton;
    [SerializeField] private Transform quitButton;
    [SerializeField] private Transform tutorialButton;
    [SerializeField] private Transform tutorialUI;
    // update this script
    private void Awake() {
        playButton.GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("GameScene");

        });

        quitButton.GetComponent<Button>().onClick.AddListener(() => {
            Application.Quit();
        });

        tutorialButton.GetComponent<Button>().onClick.AddListener(() => {
            tutorialUI.gameObject.SetActive(true);
        });

        tutorialUI.gameObject.SetActive(false);

    }

    private void Start() {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0)) {
            if (tutorialUI.gameObject.activeSelf) {
                tutorialUI.gameObject.SetActive(false);
            }
        }
    }
}
