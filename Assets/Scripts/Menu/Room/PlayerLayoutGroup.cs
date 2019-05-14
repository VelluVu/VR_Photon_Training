using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayoutGroup : MonoBehaviour
{

    public GameObject _playerListingPrefab;

    public List<PlayerListing> _playerListings = new List<PlayerListing> ( );

    //Photon callback
    private void OnMasterCLientSwitched(PhotonPlayer newMasterCLient)
    {
        //only if want
        //PhotonNetwork.LeaveRoom ( );
    }

    //PHOTON CALLBACK
    private void OnJoinedRoom()
    {

        foreach ( Transform child  in  transform )
        {
            Destroy ( child.gameObject );
        }

        Debug.Log ( "Joined Room" );

        PhotonPlayer [ ] photonPlayers = PhotonNetwork.playerList;

        for ( int i = 0 ; i < photonPlayers.Length ; i++ )
        {
            PlayerJoinedRoom ( photonPlayers [ i ] );
        }
    }

    //Photon callback
    private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer)
    {
        PlayerJoinedRoom ( photonPlayer );
    }

    //PHOTON CALLBACK
    private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
    {
        PlayerLeftRoom ( photonPlayer );
    }

    private void PlayerJoinedRoom(PhotonPlayer photonPlayer)
    {
        if(photonPlayer == null)
        {
            return;
        }

        PlayerLeftRoom ( photonPlayer );

        GameObject playerListingObj = Instantiate ( _playerListingPrefab );
        playerListingObj.transform.SetParent ( transform, false );

        PlayerListing playerListing = playerListingObj.GetComponent<PlayerListing> ( );
        playerListing.ApplyPhotonPlayer ( photonPlayer );

        _playerListings.Add ( playerListing );
    }

    private void PlayerLeftRoom(PhotonPlayer photonPlayer)
    {
        int index = _playerListings.FindIndex ( x => x._photonPlayer == photonPlayer );

        if (index != -1)
        {
            Destroy ( _playerListings [ index ].gameObject );
            _playerListings.RemoveAt ( index );
        }
    }

    public void RemovePlayer ( PhotonPlayer photonPlayer)
    {
        if (photonPlayer.IsMasterClient)
        {
            return;
        }

        int index = _playerListings.FindIndex ( x => x._photonPlayer == photonPlayer );

        if ( index != -1 )
        {
            Destroy ( _playerListings [ index ].gameObject );
            _playerListings.RemoveAt ( index );
        }
    }

}
