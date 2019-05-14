using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [HideInInspector] public static GameController instance;

    public bool gameOn;

    //ref to prefabs
    public GameObject rmainPlayer;
    public GameObject rvrInput;
    public GameObject rmenuC;

    GameObject mainPlayer;
    GameObject vrInput;
    GameObject menuC;

    public GameObject [ ] netWorkPlayers;
    public Transform [ ] spawnPositions;

    private void Awake ( )
    {

        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy ( gameObject );
        }
     
    }

    private void Start ( )
    {
        if ( SceneManager.GetActiveScene().name == "LobbyScene" )
        {
            SpawnPlayer ( );
        }
    }

    public void SpawnPlayer()
    {
        mainPlayer = Instantiate ( rmainPlayer, Vector3.zero, Quaternion.identity );
        vrInput = Instantiate ( rvrInput, Vector3.zero, Quaternion.identity );
        menuC = Instantiate ( rmenuC, new Vector3 ( 0, 2, -2 ), Quaternion.identity );

        mainPlayer.transform.GetChild ( 1 ).transform.GetChild ( 1 ).gameObject.GetComponent<Pointer> ( ).eventCam = mainPlayer.transform.GetChild ( 1 ).gameObject.GetComponentInChildren<Camera> ( );
        mainPlayer.transform.GetChild ( 1 ).transform.GetChild ( 1 ).gameObject.GetComponent<Pointer> ( ).inputModule = vrInput.GetComponent<VRInputModule> ( );

        vrInput.GetComponent<VRInputModule> ( ).SetEventCamera ( mainPlayer.transform.GetChild ( 1 ).gameObject.GetComponentInChildren<Camera> ( ) );
        menuC.GetComponent<Canvas> ( ).worldCamera = mainPlayer.transform.GetChild ( 1 ).gameObject.GetComponentInChildren<Camera> ( );
        menuC.transform.rotation = Quaternion.Euler ( 0, 180, 0 );

        Debug.Log ( mainPlayer.transform.GetChild ( 1 ).transform.GetChild ( 1 ).gameObject.GetComponent<Pointer> ( ).inputModule );
        Debug.Log ( mainPlayer.transform.GetChild ( 1 ).gameObject.GetComponentInChildren<Camera> ( ) );
    }

    public void StartGame()
    {
        gameOn = true;
        SpawnPlayer ( );
        SpawnNetworkPlayers ( ); // <---------- CALL THIS FROM NETWORK CONTROLLER
    }

    public void SpawnNetworkPlayers ( )
    {
        int index = 0;

        foreach ( var netWorkPlayer in netWorkPlayers )
        {
            PhotonNetwork.Instantiate( netWorkPlayer.name, spawnPositions [ index ].position , Quaternion.identity, 0);
            index++;
        }
    }

}
