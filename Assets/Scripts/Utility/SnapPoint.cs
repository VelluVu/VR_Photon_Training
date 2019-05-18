using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SnapPoint : MonoBehaviour
{
    public List<GameObject> snappableObjects = new List<GameObject> ();
    public HingeJoint joint;
    public float radius;
    public LayerMask planeLayer;
    GameObject myObj;
    public bool holdingObj;
   
    private void Awake ( )
    {
        joint = GetComponent<HingeJoint> ( );
    }

    public bool GetSnapPointHasObject()
    {
        
        if (joint.connectedBody != null)
        {
            return holdingObj = true;
        }
        else
        {
            return holdingObj = false;
        }

        /// JOS ON OBJECTI NIIN SIIRRÄ SE KOHILLEEN
    }
   
    public void SetPositionY(float newPos)
    {
        transform.position = new Vector3 ( transform.position.x, newPos, transform.position.z);
    }

    private void OnDrawGizmos ( )
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere ( transform.position, radius );
    }
}
