using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [HideInInspector] public SoundController instance;

    private void Awake ( )
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy ( gameObject );
        }
    }
}
