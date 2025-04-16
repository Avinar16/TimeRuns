using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public event Action<string> OnMusicEvent;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string trackName)
    {
        OnMusicEvent?.Invoke(trackName);
    }

    private void HandleMusicEvent(string trackName)
    {
        AudioClip clip = Resources.Load<AudioClip>(trackName);
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void OnEnable() => OnMusicEvent += HandleMusicEvent;
    private void OnDisable() => OnMusicEvent -= HandleMusicEvent;
}