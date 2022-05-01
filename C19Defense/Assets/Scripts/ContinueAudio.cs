using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueAudio : MonoBehaviour
{
    
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

   
}
