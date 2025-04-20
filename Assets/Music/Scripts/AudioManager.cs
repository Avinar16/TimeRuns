using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private List<SoundEffect> _soundEffects = new List<SoundEffect>();
    private AudioSource _sfxSource;

    private float _lastFootstepTime;
    private const float FOOTSTEP_DELAY = 1f;

    [System.Serializable]
    public class SoundEffect
    {
        public string name;
        public AudioClip clip;
        public string eventName;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _sfxSource = gameObject.AddComponent<AudioSource>();
            InitializeEventSubscriptions();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeEventSubscriptions()
    {
        if (Player.instance != null)
        {
            Player.instance.OnDamageTaken += () => PlayEventSFX("PlayerDamage");
            Player.instance.OnDeath += () => PlayEventSFX("PlayerDeath");
            Player.instance.OnMove += (direction) =>
            {
                if (direction.magnitude > 0.1f && CanPlayFootstep())
                {
                    PlayEventSFX("Footstep");
                    _lastFootstepTime = Time.time;
                }
            };
        }
    }

    private bool CanPlayFootstep()
    {
        return Time.time - _lastFootstepTime >= FOOTSTEP_DELAY;
    }

    public void SubscribeEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath += () => PlayEventSFX("EnemyDeath");
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
            Debug.LogWarning($"Sound {soundName} not found!");
        }
    }

    private void PlayEventSFX(string eventName)
    {
        var sound = _soundEffects.Find(s => s.eventName == eventName);
        if (sound != null)
        {
            _sfxSource.PlayOneShot(sound.clip);
        }
    }
}