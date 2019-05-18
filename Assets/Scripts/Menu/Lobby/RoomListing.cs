using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{

    public TextMeshProUGUI _roomNameText;
    public GameObject lobbyCanvasObj;
    Button button;
    public bool updated;

    public delegate void RoomListingDelegate ( TextMeshProUGUI roomName );
    public static event RoomListingDelegate roomListingEvent;

    private void Start ( )
    { 
        if(lobbyCanvasObj == null)
        {
            return;
        }
        _roomNameText = gameObject.GetComponent<TextMeshProUGUI> ( );
        LobbyScript lobbyScript = lobbyCanvasObj.GetComponent<LobbyScript> ( );
        if (roomListingEvent != null)
        {
            roomListingEvent ( _roomNameText );
        }

        Debug.Log ( _roomNameText );

        button = GetComponent<Button> ( );
        button.onClick.AddListener ( () => SelectThis (lobbyScript ) );

    }

    public void SelectThis (LobbyScript lobby )
    {

        lobby.SelectRoom ( this );

    }

    private void OnDestroy ( )
    {
        button.onClick.RemoveAllListeners ( );
    }


}
