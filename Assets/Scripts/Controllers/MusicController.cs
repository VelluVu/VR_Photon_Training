using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [HideInInspector] public MusicController instance;

    private void Awake ( )
    {
        if ( instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy ( gameObject );
        }
    }
}
