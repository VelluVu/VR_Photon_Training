using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerListing : MonoBehaviour
{

    public PhotonPlayer _photonPlayer;

    public TextMeshProUGUI playerName;

    RoomControl roomControl;
    Button button;

    public delegate void NewPlayerListedDelegate ( );
    public static event NewPlayerListedDelegate newPlayerListedEvent;

    public void ApplyPhotonPlayer ( PhotonPlayer photonPlayer )
    {
        _photonPlayer = photonPlayer;
        playerName.text = photonPlayer.NickName;
    
        if ( newPlayerListedEvent != null )
        {
            newPlayerListedEvent ( );
        }

    }

    private void Start ( )
    {
        roomControl = FindObjectOfType<RoomControl>();
        button = gameObject.GetComponent<Button> ( );
        button.onClick.AddListener ( ( ) => SelectedPlayer ( ) );
    }

    public void SelectedPlayer ( )
    {
        roomControl.SetSelectedPlayer ( _photonPlayer );
    }

}
