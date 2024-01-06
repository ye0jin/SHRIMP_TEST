using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource audioSource;
    private AudioSource bgmAudioSource;

    private string bgmKey = "BGMVolume";
    private string sfxKey = "SFXVolume";

    [Header("BGM")]
    [Header("Player")]
    [SerializeField] private AudioClip playerSwimSound;
    [SerializeField] private AudioClip playerDashSound;
    [Header("Obstacle")]
    [SerializeField] private AudioClip glassBrokenSound;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Soundmanager error");
        Instance = this;

        // audioSource �޾��ּ���!!^_^
        bgmAudioSource = Camera.main.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        float bgmVolume = PlayerPrefs.GetFloat(bgmKey, 0.1f);
        float sfxVolume = PlayerPrefs.GetFloat(sfxKey, 0.4f);
        bgmAudioSource.volume = bgmVolume;
        audioSource.volume = sfxVolume;
    }

    private void Start()
    {
        bgmAudioSource.Play();
        bgmAudioSource.loop = true;
    }


    // ���� ���� ȣ�� �Լ�
    public void SetBGMVolume(float value)
    {
        bgmAudioSource.volume = value;
        PlayerPrefs.SetFloat(bgmKey, value);
    }
    public void SetSFXVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat(sfxKey, value);
    }

    //SoundManager.Instance.PlaySwimSound(); <- �̷������� ���
    public void PlaySwimSound()
    {
        audioSource.PlayOneShot(playerSwimSound);
    }
    public void PlayDashSound()
    {
        audioSource.PlayOneShot(playerDashSound);
    }
    public void PlayGlassBrokenSound()
    {
        audioSource.PlayOneShot(glassBrokenSound);
    }
}