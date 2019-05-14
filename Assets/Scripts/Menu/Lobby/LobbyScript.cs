using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScript : MonoBehaviour
{
    public Button [ ] buttons;
    public TMP_InputField inputField;
    public GameObject menu;
    public GameObject lobby;
    public GameObject room;
    public RoomListing selectedRoom;

    public static string version = "v0.1";

    private void Awake ( )
    {
       
        buttons [ 0 ].onClick.AddListener ( ( ) => CreateRoom ( ) );
        buttons [ 1 ].onClick.AddListener ( ( ) => JoinRoom ( ) );
        buttons [ 2 ].onClick.AddListener ( ( ) => Back ( ) );
    }

    private void OnEnable ( )
    {
        RoomListing.newListingEvent += InitNewListing;
    }

    private void OnDisable ( )
    {
        RoomListing.newListingEvent -= InitNewListing;
    }

    public GameObject InitNewListing(GameObject listing)
    {

        if ( inputField.text.Length - 1 <= 0 )
        {
            listing.GetComponent<RoomListing> ( )._roomNameText.text = PhotonNetwork.player.NickName + "s.room";
        }
        else
        {
            listing.GetComponent<RoomListing> ( )._roomNameText.text = inputField.text;
        }

        return lobby;
    }

    private void Start ( )
    {
        Debug.Log ( "Connecting to server..." );

        PhotonNetwork.ConnectUsingSettings ( version );
    }

    //PHOTON CALLBACK
    private void OnConnectedToMaster()
    {
        Debug.Log ( "Connected to master" );
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.playerName = NetworkController.instance.userName;
    }

    //PHOTON CALLBACK
    private void OnJoinedLobby ( )
    {
        
        Debug.Log ( "Joined Lobby" );

        Debug.Log(PhotonNetwork.GetRoomList ( ).Length);

    }

    void Back ( )
    {
        menu.SetActive ( true );
        lobby.SetActive ( false );
        PhotonNetwork.LeaveLobby ( );
    }

    void CreateRoom ( )
    {

        RoomOptions roomOptions = new RoomOptions ( );
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = 5;

        if (inputField.text.Length <= 1)
        {
            if(PhotonNetwork.CreateRoom ( PhotonNetwork.player.NickName + "s_room", roomOptions, TypedLobby.Default ))
            {
                room.SetActive ( true );
                lobby.SetActive ( false );
                Debug.Log ( "Create room successful " + PhotonNetwork.player.NickName + "s_room" );
            }
            else
            {
                Debug.Log ( "Create room failed" );
            }
        }
        else
        {
            if ( PhotonNetwork.CreateRoom ( inputField.text, roomOptions, TypedLobby.Default ) )
            {
                room.SetActive ( true );
                lobby.SetActive ( false );
                Debug.Log ( "Create room successful " + PhotonNetwork.player.NickName + "s_room" );            
            }
            else
            {
                Debug.Log ( "Create room failed" );
            }
        }
       
    }

    //PHOTON CALLBACK
    private void OnPhotonCreateRoomFailed ( object [ ] codeAndMsg )
    {
        Debug.Log ( "Create room failed: " + codeAndMsg [ 1 ] );
    }

    private void OnCreatedRoom ( )
    {

       
        Debug.Log ( "Created Room" );

    }

   

    //PHOTON CALLBACK
 
    void JoinRoom ( )
    {
        if ( selectedRoom != null )
        {

            if ( PhotonNetwork.JoinRoom ( selectedRoom._roomNameText.text ) )
            {

            }
            else
            {
                Debug.Log ( "Join room failed" );
            }

        }
        else
        {

            Debug.Log ( "No room selected" );
        }
    }

    public void SelectRoom ( RoomListing selection )
    {
        selectedRoom = selection;
    }

}
