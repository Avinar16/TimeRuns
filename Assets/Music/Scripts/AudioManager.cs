using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private List<SoundEffect> _soundEffects = new List<SoundEffect>();
    private AudioSource _sfxSource;

    [System.Serializable]
    public class SoundEffect
    {
        public string name; // Например, "PlayerDamage"
        public AudioClip clip; // Сам звуковой файл
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _sfxSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(string soundName)
    {
        AudioClip clip = _soundEffects.Find(s => s.name == soundName)?.clip;
        if (clip != null)
        {
            _sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Звук {soundName} не найден!");
        }
    }
}