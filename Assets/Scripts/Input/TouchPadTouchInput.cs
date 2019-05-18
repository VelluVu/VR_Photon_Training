using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TouchPadTouchInput : MonoBehaviour
{

    public SteamVR_ActionSet _actionSet;


    public SteamVR_Action_Single squeezeAction;
    public SteamVR_Action_Vector2 touchPadAction;

    //First way
    void Update ( )
    {
        if (SteamVR_Actions._default.Teleport.GetStateUp(SteamVR_Input_Sources.Any))
        {
            //Debug.Log ( "TELEPORT STATE UP" );
        }

        if ( SteamVR_Actions._default.GrabPinch.GetStateUp ( SteamVR_Input_Sources.Any ) )
        {
            //Debug.Log ( "GRABPINCH STATE UP" );
        }

        float triggerValue = squeezeAction.GetAxis ( SteamVR_Input_Sources.Any );
        if ( triggerValue >= 0 )
        {
            //Debug.Log ( triggerValue );
        }

        Vector2 touchPadValue = touchPadAction.GetAxis ( SteamVR_Input_Sources.Any );
        if ( touchPadValue != Vector2.zero )
        {
            //Debug.Log ( touchPadValue );
        }

    }
}
