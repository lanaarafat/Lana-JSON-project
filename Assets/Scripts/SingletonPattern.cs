using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPattern : MonoBehaviour
{
    private static SingletonPattern instance;

    public static SingletonPattern GetSingleton()
    {
        return instance;
    }

    void SetSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }

        instance = this;
    }
    private void Awake()
    {
        SetSingleton();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
