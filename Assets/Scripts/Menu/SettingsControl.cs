using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour
{

    public Button back;
    public GameObject menu;
    public GameObject settings;
    public Slider [ ] sliders; //0 == master , 1 == sounds, 2 == music

    private void Awake ( )
    {
        back.onClick.AddListener ( ( ) => Back() );
    }

    public void Back()
    {
        menu.SetActive ( true );
        settings.SetActive ( false );
    }

}
