using UnityEngine;
using UnityEngine.UI;

public class RoomControl : MonoBehaviour
{

    public GameObject room;
    public GameObject lobby;

    public Button[] buttons;
    public PhotonPlayer selectedPlayer;

    private void Awake ( )
    {
        buttons [ 0 ].onClick.AddListener ( ( ) => GOGO ( ) );
        buttons [ 1 ].onClick.AddListener ( ( ) => KickPlayer ( ) );
        buttons [ 2 ].onClick.AddListener ( ( ) => Back ( ) );
        buttons [ 3 ].onClick.AddListener ( ( ) => Sync ( ) );
        buttons [ 4 ].onClick.AddListener ( ( ) => Delay ( ) );
    }

    void GOGO()
    {
        PhotonNetwork.LoadLevel ( 1 );
    } 

    void Sync()
    {
        PhotonNetwork.LoadLevel ( 1 );
    }

    void Delay()
    {
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.LoadLevel ( 1 );
    }

    void KickPlayer()
    {
        room.GetComponentInChildren<PlayerLayoutGroup> ( ).RemovePlayer ( selectedPlayer );
    }

    void Back()
    {
        PhotonNetwork.LeaveRoom ( );
        lobby.SetActive ( true );
        room.SetActive ( false );      
    }

    public void SetSelectedPlayer(PhotonPlayer photonPlayer)
    {
        selectedPlayer = photonPlayer;
    }

}
