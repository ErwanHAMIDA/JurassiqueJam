using System.Collections.Generic;
using UnityEngine;

public class S_SFXManager : MonoBehaviour
{
    public static S_SFXManager Instance { get; private set; }
    [SerializeField] private AudioSource _SFXObject;

    private AudioSource _oneShotAudioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        _oneShotAudioSource = Instantiate(_SFXObject);
    }

    public void PlaySFXClipAtPoint(AudioClip audioClip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(_SFXObject, transform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);

    }

    public void PlayRandomClip(List<AudioClip> audioClipList, Transform transform, float volume)
    {
        int random = UnityEngine.Random.Range(0, audioClipList.Count - 1);

        AudioSource audioSource = Instantiate(_SFXObject, transform.position, Quaternion.identity);

        audioSource.clip = audioClipList[random];

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);

    }

    public void PlaySFXClip(AudioClip audioClip)
    {
        AudioSource audioSource = Instantiate(_SFXObject);

        audioSource.clip = audioClip;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);

    }

    public void PlayOneAtATimeSFXClip(AudioClip audioClip)
    {
        if (_oneShotAudioSource.isPlaying) return;
        
        _oneShotAudioSource.PlayOneShot(audioClip);
    }
}