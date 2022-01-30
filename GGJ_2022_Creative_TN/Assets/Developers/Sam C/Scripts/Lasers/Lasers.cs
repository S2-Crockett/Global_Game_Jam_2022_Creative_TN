using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveStages
{
    Active,
    Cooldown
}
public class Lasers : MonoBehaviour
{
    [SerializeField] private int _laserActivateTime;
    [SerializeField] private int _laserCooldownTime;
    private float _timer;

    public GameObject _detection;
    public ParticleSystem _particleSystem;

    private ActiveStages _activeStages;

    private void Start()
    {
        _timer = _laserActivateTime;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("Laser"))
            {
                _detection = transform.GetChild(i).gameObject;
            }
        }

        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        switch (_activeStages)
        {
            case ActiveStages.Active:
            {
                _detection.SetActive(true);
                _particleSystem.Play();
                
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _activeStages = ActiveStages.Cooldown; 
                    _timer = _laserCooldownTime;
                }
                break;
            }
            case ActiveStages.Cooldown:
            {
                _detection.SetActive(false);
                _particleSystem.Stop();
                
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _activeStages = ActiveStages.Active;
                    _timer = _laserActivateTime;
                }
                break;
            }
        }
    }
}
