using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioClip[] musicClips;

    private const string PREFS_MUSIC_VOLUME = "MusicVolume";

    private AudioSource audioSource;
    private float volume = .3f;

    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(PREFS_MUSIC_VOLUME, .3f);
        audioSource.volume = volume;
    }

    private void Update() {
        if (!audioSource.isPlaying) {
            PlayMusic();
        }
    }

    public void PlayMusic() {
        int randomIndex = Random.Range(0, musicClips.Length);
        audioSource.clip = musicClips[randomIndex];
        audioSource.Play();
    }

    public void IncreaseVolume() {
        volume += .1f;
        volume = Mathf.Clamp01(volume);
        UpdateVolume();
    }
    public void DecreaseVolume() {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
        UpdateVolume();
    }

    private void UpdateVolume() {
        PlayerPrefs.SetFloat(PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
        audioSource.volume = volume;
    }

    public float GetVolume() {
        return volume;
    }

}
