using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{

    public Button [ ] buttons;
    public GameObject settings;
    public GameObject menu;
    public GameObject lobby;
    Canvas canvas;

    private void Awake ( )
    {
        canvas = gameObject.GetComponent<Canvas> ( );
        buttons [ 0 ].onClick.AddListener ( ( ) => Lobby ( ) );
        buttons [ 1 ].onClick.AddListener ( ( ) => Settings ( ) );
        buttons [ 2 ].onClick.AddListener ( ( ) => Application.Quit ( ) );
    }

    public void SetEventCamera ( Camera cam )
    {
        canvas.worldCamera = cam;
    }

    public void Settings ( )
    {
        settings.SetActive ( true );
        menu.SetActive ( false );
    }

    public void Lobby ( )
    {
        lobby.SetActive ( true );
        PhotonNetwork.JoinLobby ( );
        menu.SetActive ( false );
    }

}
