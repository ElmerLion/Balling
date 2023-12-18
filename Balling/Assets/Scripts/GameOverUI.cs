using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    public static GameOverUI Instance { get; private set; }

    [SerializeField] Transform retryButton;
    [SerializeField] Transform mainMenuButton;
    [SerializeField] TextMeshProUGUI roundScore;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] TextMeshProUGUI highestStreak;

    private void Awake() {
        Instance = this;

        retryButton.GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        });

        mainMenuButton.GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        });

        Hide();
    }



    public void Show() {
        gameObject.SetActive(true);

        roundScore.SetText("Score: " + ScoreManager.Instance.GetCurrentScore());
        highScore.SetText("Highscore: " + PlayerPrefs.GetFloat("HighScore"));
        highestStreak.SetText("Highest Streak: " + PlayerPrefs.GetFloat("MaxCombo"));
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

}
