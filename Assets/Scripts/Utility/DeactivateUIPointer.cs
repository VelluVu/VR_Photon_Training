using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateUIPointer : MonoBehaviour
{

    Camera eyes;
    GameObject laser;

    private void Start ( )
    {
        eyes = gameObject.GetComponent<Camera> ( );
    }

    private void Update ( )
    {

        RayCastToPointFromCamera ( );

    }

    public void RayCastToPointFromCamera ( )
    {

        if ( Physics.Raycast ( eyes.transform.position, eyes.transform.forward, out RaycastHit hit, 5f ) )
        {
            Debug.DrawRay ( eyes.transform.position, eyes.transform.forward, Color.red, 1f );

            if ( hit.transform.gameObject.layer == LayerMask.NameToLayer ( "UI" ) )
            {
                //Debug.Log ( "HIT UI" );
                GameController.instance.ActivateLaser ( );
                GameController.instance.ActivateLaserInput ( );
            }

            if ( hit.transform.gameObject.layer == LayerMask.NameToLayer ( "Ground" ) )
            {
                //Debug.Log ( "HITTING GROUND" );
                GameController.instance.DeactivateLaser ( );
                GameController.instance.DeactivateLaserInput ( );
            }

        }
        else
        {

            //Debug.Log ( "NOT HITTING ANYTHING" );
            GameController.instance.DeactivateLaser ( );
            GameController.instance.DeactivateLaserInput ( );

        }


    }

}
