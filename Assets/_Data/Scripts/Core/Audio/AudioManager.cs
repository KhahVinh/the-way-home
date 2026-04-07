using UnityEngine;

public class AudioManager : Manager<AudioManager>
{
    [SerializeField]
    public AudioSource _audioSFX;

    public void PlaySound(AudioClip clip)
    {
        _audioSFX.PlayOneShot(clip);
    }
}
