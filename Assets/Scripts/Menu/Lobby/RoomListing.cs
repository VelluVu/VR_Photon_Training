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

    public delegate GameObject NewListingDelegate ( GameObject roomlisting );
    public static event NewListingDelegate newListingEvent;

    private void Start ( )
    {
        if ( newListingEvent != null)
        {
            lobbyCanvasObj = newListingEvent ( gameObject );
        }

        if(lobbyCanvasObj == null)
        {
            return;
        }

        LobbyScript lobbyScript = lobbyCanvasObj.GetComponent<LobbyScript> ( );

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
