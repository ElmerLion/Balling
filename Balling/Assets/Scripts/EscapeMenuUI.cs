using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapeMenuUI : MonoBehaviour {

    public static EscapeMenuUI Instance { get; private set; }

    [SerializeField] private Transform optionsMenu;
    [SerializeField] private Transform resumeButton;
    [SerializeField] private Transform mainMenuButton;
    [SerializeField] private Transform optionsButton;

    private bool isOpen;

    private void Awake() {
        Instance = this;
        transform.gameObject.SetActive(false);

        mainMenuButton.GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenuScene");
        });
        resumeButton.GetComponent<Button>().onClick.AddListener(() => {
            Hide();
        });
        optionsButton.GetComponent<Button>().onClick.AddListener(() => {
            Hide();
            OptionsMenuUI.Instance.Show();
        });
    }

    public void Show() {
        transform.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Hide() {
        transform.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

}
