using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using VRUiKits.Utils;

public class GameController : MonoBehaviour
{
    [HideInInspector] public static GameController instance;

    public static bool gameOn;

    public GameObject teleportingPrefab;
    //ref to prefabs
    public GameObject rmainPlayer;
    //public GameObject rvrInput;
    public GameObject uIlaser;
    public GameObject rmenuC;

    GameObject mainPlayer;
    //GameObject vrInput;
    GameObject UIlaser;
    GameObject menuC;

    public GameObject [ ] netWorkPlayers;
    public Transform [ ] spawnPositions = new Transform[5];

    public bool DEVELOPERMODE = false;

    public delegate void LaserIsOffDelegate ( );
    public static event LaserIsOffDelegate laserIsOffEvent;

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

        for (int i = 0 ; i < spawnPositions.Length ; i++ )
        {
            spawnPositions[i] = transform.parent.transform.GetChild ( 4 ).GetChild ( i ).transform;
        }

        
  
    }

    private void OnEnable ( )
    {
        SceneManager.sceneLoaded += CheckLoadedScene;
    }

    private void OnDisable ( )
    {
        SceneManager.sceneLoaded -= CheckLoadedScene;
    }

    public void CheckLoadedScene (Scene scene, LoadSceneMode loadSceneMode )
    {
        if(scene.name == "LobbyScene")
        {
            SpawnPlayer ( );
        }
        else
        {

            if(DEVELOPERMODE) 
            {

            SpawnPlayer();
  
            }

            StartGame ( );

        }
    }

    public void SpawnPlayer()
    {
        mainPlayer = Instantiate ( rmainPlayer, Vector3.zero, Quaternion.identity );
        UIlaser = Instantiate ( uIlaser, Vector3.zero, Quaternion.identity );
        menuC = Instantiate ( rmenuC, new Vector3 ( 0, 2, 2 ), Quaternion.identity );
        Instantiate ( teleportingPrefab );
    }

    public void StartGame()
    {
    
        SpawnNetworkPlayers ( );
        Instantiate ( teleportingPrefab );
        DeactivateLaser ( );

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

    public void DeactivateLaser()
    {
        UIlaser.SetActive ( false );
    }

    public void ActivateLaser ( )
    {
        UIlaser.SetActive ( true );
    }

    public void DeactivateLaserInput()
    {
        mainPlayer.GetComponentInChildren<LaserInputModule> ( ).activeLaser = false ;
        //says everyone that now laser is off no interaction with ui is going to happen...
        if(laserIsOffEvent != null)
        {
            laserIsOffEvent ( );
        }
       
    }

    public void ActivateLaserInput()
    {
        mainPlayer.GetComponentInChildren<LaserInputModule> ( ).activeLaser = true;
        
    }

}
