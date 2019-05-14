using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviour
{

    public GameObject _roomListingPrefab;

    public List<RoomListing> _roomListingButtons = new List<RoomListing> ( );

    private void OnReceivedRoomUpdate ( )
    {
        RoomInfo [ ] rooms = PhotonNetwork.GetRoomList ( );

        foreach ( var room in rooms )
        {
            RoomReceived ( room );
        }

        RemoveOldRoom ( );

    }

    private void RoomReceived ( RoomInfo room )
    {
        Debug.Log( room.Name );
        
        int index = _roomListingButtons.FindIndex ( x => x._roomNameText.text == room.Name );

        if ( index == -1)
        {
            if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                GameObject roomListingObj = Instantiate ( _roomListingPrefab );
                roomListingObj.transform.SetParent ( transform, false );

                RoomListing roomListing = roomListingObj.GetComponent<RoomListing> ( );
                _roomListingButtons.Add ( roomListing );

                index = ( _roomListingButtons.Count - 1 );
            }
        }

        if (index != -1 )
        {
            RoomListing roomListing = _roomListingButtons [ index ];
            roomListing._roomNameText.text = room.Name;
            roomListing.updated = true;
        }
    }

    private void RemoveOldRoom ( )
    {
        List<RoomListing> removeRooms = new List<RoomListing> ( );

        foreach ( RoomListing roomListing in _roomListingButtons  )
        {
            if ( !roomListing.updated )
            {
                removeRooms.Add ( roomListing );
            }
            else
            {
                roomListing.updated = false;
            }
        }

        foreach ( RoomListing roomListing in removeRooms )
        {
            GameObject roomListingObj = roomListing.gameObject;
            _roomListingButtons.Remove ( roomListing );
            Destroy ( roomListingObj );
        }
       
    }
}
