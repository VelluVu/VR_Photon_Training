using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerListing : MonoBehaviour
{

    public PhotonPlayer _photonPlayer;

    public TextMeshProUGUI playerName;

    GameObject roomControl;
    Button button;

    public delegate GameObject NewPlayerListedDelegate ( );
    public static event NewPlayerListedDelegate newPlayerListedEvent;

    public void ApplyPhotonPlayer ( PhotonPlayer photonPlayer )
    {
        _photonPlayer = photonPlayer;
        playerName.text = photonPlayer.NickName;
    
        if ( newPlayerListedEvent != null )
        {
            roomControl = newPlayerListedEvent ( );
        }

    }

    private void Start ( )
    {
        button = gameObject.GetComponent<Button> ( );
        button.onClick.AddListener ( ( ) => SelectedPlayer ( ) );
    }

    public void SelectedPlayer ( )
    {
        roomControl.GetComponent<RoomControl> ( ).SetSelectedPlayer ( _photonPlayer );
    }

}
