using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.Play();
    }
}