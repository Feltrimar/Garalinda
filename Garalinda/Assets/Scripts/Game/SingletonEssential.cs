using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonEssential : MonoBehaviour
{
    private static SingletonEssential _instance;

    public static SingletonEssential Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
}

