using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance { get; private set; }

    private const string PREFS_HIGHSCORE = "HighScore";
    private const string PREFS_MAXCOMBO = "MaxCombo";

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboStreakText;
    [SerializeField] private List<BallTypesSO> ballTypeComboBreakers;
    [SerializeField] private Volume postProcessingVolume;

    private Bloom bloom;

    private float currentScore;
    private float highScore;
    private int currentComboStreak;
    private float highestComboStreak;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        currentScore = 0;
        highScore = PlayerPrefs.GetFloat(PREFS_HIGHSCORE);
        if (postProcessingVolume.profile.TryGet(out Bloom bloomEffect)) {
            bloom = bloomEffect;
        }
    }

    public void AddScore(int amount) {
        currentScore += amount * currentComboStreak;
        currentScore = Mathf.Clamp(currentScore, 0,  9999999999999999999);
        scoreText.text = "Score: " + currentScore;

        if (currentScore > highScore) {
            highScore = currentScore;
            PlayerPrefs.SetFloat(PREFS_HIGHSCORE, highScore);
        }
    }

    public float GetCurrentScore() {
        return currentScore;
    }

    public void CheckComboOnHit(BallTypesSO ballTypeSO) {
        if (!ballTypeComboBreakers.Contains(ballTypeSO)){
            currentComboStreak++;
            bloom.intensity.value = 0.9f + (0.05f * currentComboStreak);
            if (currentComboStreak > PlayerPrefs.GetFloat(PREFS_MAXCOMBO)) {
                PlayerPrefs.SetFloat(PREFS_MAXCOMBO, currentComboStreak);
            }
        } else {
            currentComboStreak = 1;
            bloom.intensity.value = 0.9f;
        }

        if (currentComboStreak > 1) {
            ComboStreakDisplay.Instance.ShowStreak(currentComboStreak);
        }
        
    }
    
}
