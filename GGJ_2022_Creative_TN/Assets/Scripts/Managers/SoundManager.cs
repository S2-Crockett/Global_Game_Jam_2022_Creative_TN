using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public static SoundManager _Instance;

    [SerializeField] private AudioSource _MusicSource;
    [SerializeField] private AudioSource _PickupSource;
    [SerializeField] private AudioSource _JumpSource;
    [SerializeField] private AudioSource _HealthSource;
    [SerializeField] private AudioSource _MovementSource;

    [SerializeField] private AudioClip[] _MovementSounds;

    public PlayerMovement pMovement;

    // Start is called before the first frame update
    void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if(!_MusicSource.isPlaying)
        {
            _MusicSource.PlayOneShot(clip);
        }
    }
    public void PickupSounds(AudioClip clip)
    {
        if (!_PickupSource.isPlaying)
        {
            _PickupSource.PlayOneShot(clip);
        }
    }
    public void JumpSounds(AudioClip clip)
    {
        if (!_JumpSource.isPlaying)
        {
            _JumpSource.PlayOneShot(clip);
        }
    }
    public void HealthSounds(AudioClip clip)
    {
        if (!_HealthSource.isPlaying)
        {
            _JumpSource.PlayOneShot(clip);
        }
    }
    public void MovementSounds(AudioClip clip)
    {
        if (!_MovementSource.isPlaying)
        {
            _MovementSource.PlayOneShot(clip);
        }
    }

    public void Update()
    {
        AccessSound();
    }
    public void AccessSound()
    {
        if(pMovement != null)
        {
            if (pMovement.isMoving == true)
            {
                print("Playing Sound");
                _Instance.MovementSounds(_MovementSounds[UnityEngine.Random.Range(0, _MovementSounds.Length)]);
            }
            
        }
    }
}
