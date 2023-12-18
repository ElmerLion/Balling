using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }

    private const string PREFS_SOUND_EFFECTS_VOLUME = "SoundsEffectsVolume";

    [SerializeField] private AudioClip onGoodBallHitSound;
    [SerializeField] private AudioClip onBadBallHitSound;

    private float volume = 1f;

    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start() {
        PlayerHitDetection.Instance.OnGoodBallHit += SoundManager_OnGoodBallHit;
        PlayerHitDetection.Instance.OnBadBallHit += SoundManager_OnBadBallHit;
    }

    private void SoundManager_OnBadBallHit(object sender, System.EventArgs e) {
        PlaySound(onBadBallHitSound, Player.Instance.transform.position);
    }

    private void SoundManager_OnGoodBallHit(object sender, System.EventArgs e) {
        PlaySound(onGoodBallHitSound, Player.Instance.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayDamageSound() {
        PlaySound(onBadBallHitSound, Player.Instance.transform.position);
    }

    public void IncreaseVolume() {
        volume += .1f;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public void DecreaseVolume() {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}
