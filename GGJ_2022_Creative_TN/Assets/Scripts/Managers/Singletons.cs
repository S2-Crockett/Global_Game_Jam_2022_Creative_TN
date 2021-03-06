using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference:
// Hijazi, M(2021)  Manager Classes & Singleton Pattern in Unity [Source code].
// https://levelup.gitconnected.com/tip-of-the-day-manager-classes-singleton-pattern-in-unity-1bf3aafe9430
// Reference:

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                }
            }
            
            return _instance;
        }
    }

    protected void Awake()
    {
        if (!_instance)
        {
            _instance = this as T;
                    DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
