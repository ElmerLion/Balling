using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsMenuUI : MonoBehaviour {

    public static OptionsMenuUI Instance { get; private set; }

    [Header("General")]
    [SerializeField] private Transform escapeMenu;
    [SerializeField] private Transform backButton;
    [SerializeField] private Transform mainMenuButton;

    [Header("Sound")]
    [SerializeField] private TextMeshProUGUI currentSoundLevel;
    [SerializeField] private Transform increaseSoundButton;
    [SerializeField] private Transform decreaseSoundButton;

    [Header("Music")]
    [SerializeField] private TextMeshProUGUI currentMusicLevel;
    [SerializeField] private Transform increaseMusicButton;
    [SerializeField] private Transform decreaseMusicButton;

    private void Awake() {
        Instance = this;

        Hide();

        backButton.GetComponent<Button>().onClick.AddListener(() => {
            Hide();
            EscapeMenuUI.Instance.Show();

        });

        mainMenuButton.GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenuScene");
        });

        increaseSoundButton.GetComponent<Button>().onClick.AddListener(() => {
            SoundManager.Instance.IncreaseVolume();
            UpdateVisual();
        });
        decreaseSoundButton.GetComponent<Button>().onClick.AddListener(() => {
            SoundManager.Instance.DecreaseVolume();
            UpdateVisual();
        });
        increaseMusicButton.GetComponent<Button>().onClick.AddListener(() => {
            MusicManager.Instance.IncreaseVolume();
            UpdateVisual();
        });
        decreaseMusicButton.GetComponent<Button>().onClick.AddListener(() => {
            MusicManager.Instance.DecreaseVolume();
            UpdateVisual();
        });


    }

    private void Start() {
        UpdateVisual();
    }

    private void UpdateVisual() {
        currentSoundLevel.text = Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        currentMusicLevel.text = Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();
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
