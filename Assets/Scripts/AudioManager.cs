using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("The AudioManager is Null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    [SerializeField]
    private AudioSource _effectSource;
    [SerializeField]
    private AudioSource _musicSource;
    [SerializeField]
    private AudioSource _dialogueSource;
    [SerializeField]
    private AudioClip[] _dialogueClips;
    [SerializeField]
    private AudioClip[] _musicClips;
    [SerializeField]
    private AudioClip[] _effectClips;

    public void LoopEffect(AudioClip clip)
    {
        
        _effectSource.clip = clip;
        _effectSource.loop = true;
        _effectSource.Play();
    }

    public void StopEffectSource()
    {
        _effectSource.Stop();
    }

    public void PlayEffectSource()
    {
        _effectSource.Play();
    }
    public float GetDialogueTime()
    {
        return _dialogueSource.time;
    }
  

    public void PlayEffect(int effect, float volume)
    {
        _effectSource.PlayOneShot(_effectClips[effect],volume);
    }

    public void OnButtonHighlight()
    {
        _effectSource.PlayOneShot(_effectClips[0], 1);
    }

    public void OnButtonSelect()
    {
        _effectSource.PlayOneShot(_effectClips[1], 1);
    }



    public void StopMusic()
    {
        _musicSource.Stop();
    }
    public void PlayMusic(int MusicClip)
    {
        _musicSource.Stop();
        _musicSource.clip = _musicClips[MusicClip];
        _musicSource.Play();
    }
}
