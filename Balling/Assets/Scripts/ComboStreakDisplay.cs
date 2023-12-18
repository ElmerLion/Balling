using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboStreakDisplay : MonoBehaviour
{
    public static ComboStreakDisplay Instance { get; private set; }

    private TextMeshProUGUI comboStreakText;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        comboStreakText = transform.GetComponent<TextMeshProUGUI>();
        comboStreakText.enabled = false;
    }

    public void ShowStreak(int streak) {
        //transform.position = transform.position * Random.Range(10, 100);
        StartCoroutine(AnimateStreak(streak));
    }

    private IEnumerator AnimateStreak(int streak) {
        comboStreakText.text = streak + "x";
        comboStreakText.enabled = true;

        float fadeDuration = .5f;
        float fadeDelay = .3f;

        // Fade in
        float timer = 0f;
        while (timer < fadeDuration) {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            comboStreakText.color = new Color(comboStreakText.color.r, comboStreakText.color.g, comboStreakText.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(fadeDelay);

        // Fade out
        timer = 0f;
        while (timer < fadeDuration) {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            comboStreakText.color = new Color(comboStreakText.color.r, comboStreakText.color.g, comboStreakText.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        comboStreakText.enabled = false;
    }
}
