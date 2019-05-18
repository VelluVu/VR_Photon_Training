using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviour
{

    [HideInInspector] public static NetworkController instance;
    public string userName;
    int playersInGame = 0;
    PhotonView _photonView;
    
    private void Awake ( )
    {
        if ( instance == null )
        {
            instance = this;
        }
        else if ( instance != null )
        {
            Destroy ( gameObject );
        }

        userName = "User" + Random.Range ( 1000, 9999 );
        _photonView = GetComponent<PhotonView> ( );

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnLeftRoom ( )
    {
        Debug.Log ( "Left Room" );
    }

    private void OnConnectedToPhoton ( )
    {
        Debug.Log ( "Connected to Photon" );
    }

    private void OnPhotonInstantiate ( PhotonMessageInfo info )
    {
        Debug.Log(info.ToString() );
    }

    private void OnGUI()
    {
        GUILayout.Label ( PhotonNetwork.connectionStateDetailed.ToString ( ) );
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if ( scene.name == "GameScene")
        {
            if (PhotonNetwork.isMasterClient)
            {
                MasterLoadedScene ( );
            }
            else
            {
                NonMasterLoadedScene ( );
            }
        }
    }

    private void MasterLoadedScene()
    {
        playersInGame = 1;
        _photonView.RPC ( "RPC_LoadGameOthers", PhotonTargets.Others );
    
    }

    private void NonMasterLoadedScene()
    {
        _photonView.RPC ( "RPC_LoadedGameScene", PhotonTargets.MasterClient );
    }
   
    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel ( 1 );
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playersInGame++;

        if (playersInGame == PhotonNetwork.playerList.Length)
        {
            Debug.Log ( "All players are in the game scene" );
        }
    }

}
