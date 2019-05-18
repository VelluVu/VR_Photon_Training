using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MovableSnapPointDetection : MonoBehaviour
{

    public float radius;
    public MovableType movableType;
    public LayerMask layerMask;
    public LayerMask whatIsGround;
    GameObject clone;
    MeshRenderer meshRenderer;
    Collider snapPointCollider;
    Rigidbody rb;
    Collider _col;
    Vector3 _objBounds;
    public bool snapS;

    private void OnDrawGizmos ( )
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere ( transform.position, radius );
    }

    private void Start ( )
    {
        rb = gameObject.GetComponent<Rigidbody> ( );
        _col = gameObject.GetComponent<Collider> ( );
        _objBounds = new Vector3 ( _col.bounds.size.x, _col.bounds.size.y, _col.bounds.size.z );
        meshRenderer = gameObject.GetComponent<MeshRenderer> ( );
        clone = transform.GetChild ( 0 ).gameObject;
        clone.transform.SetParent ( null );

        foreach ( MeshRenderer mesh in clone.GetComponents<MeshRenderer> ( ) )
        {
            foreach ( var material in mesh.materials )
            {
                material.color = Color.green;
            }
        }
        clone.SetActive ( false );

    }

    private void FixedUpdate ( )
    {

        if ( snapS && snapPointCollider != null )
        {
            Debug.Log ( snapPointCollider.transform.gameObject.GetComponent<SnapPoint> ( ).GetSnapPointHasObject ());

            if ( !snapPointCollider.transform.gameObject.GetComponent<SnapPoint> ( ).GetSnapPointHasObject() )
                AutoSnap ( snapPointCollider );

        }
        if ( !snapS && snapPointCollider != null )
        {


            Debug.Log ( snapPointCollider.transform.gameObject.GetComponent<SnapPoint> ( ).GetSnapPointHasObject ( ) );
            snapPointCollider.transform.gameObject.GetComponent<HingeJoint> ( ).connectedBody = null;


        }

        CheckNearbySnapPoints ( );

    }

    public Vector3 GetSnapPosFromCollider ( Collider col )
    {

        return col.transform.position;

    }

    public Transform GetSnapPointTransform ( )
    {

        if ( snapPointCollider != null )
        {
            return snapPointCollider.transform;
        }
        else
        {
            return transform;
        }

    }

    public void CheckNearbySnapPoints ( )
    {

        Collider [ ] possibleSnapPoints = Physics.OverlapSphere ( transform.position, radius, layerMask );

        if ( possibleSnapPoints != null )
        {
            snapPointCollider = GetClosestSnapCollider ( transform.position, possibleSnapPoints );
            if ( snapPointCollider != null )
            {
                clone.SetActive ( true );
                clone.transform.position = snapPointCollider.transform.position;
                clone.transform.rotation = transform.rotation;

            }
        }
    }

    public float CalculateSnapPointOffset ( Collider collider )
    {
        bool hits = Physics.Raycast ( new Vector3 ( transform.position.x, transform.position.y - _objBounds.y / 2, transform.position.z ), -transform.up, out RaycastHit hit, 10f, whatIsGround );
        float distanceFromGround = 0;

        //LASKU JÄI KESKEN
        if ( hits )
        {
            distanceFromGround = _objBounds.y * hit.distance;
            return distanceFromGround;
        }

        return distanceFromGround;
    }

    void AutoSnap ( Collider snapPointCollider )
    {

        snapPointCollider.transform.gameObject.GetComponent<HingeJoint> ( ).connectedBody = rb;

    }

    Collider GetClosestSnapCollider ( Vector3 pos, Collider [ ] colliders )
    {

        float closestDistance = 99999.9f;
        Collider closestCollider = null;


        foreach ( Collider col in colliders )
        {
            float distance = Vector3.Distance ( pos, col.transform.position );

            //Debug.Log ( closestDistance );
            //Debug.Log ( distance );

            if ( closestDistance > distance )
            {
                closestDistance = distance;
                closestCollider = col;
            }

        }

        return closestCollider;
    }
}

public enum MovableType
{
    Buildable,
    Movable,
}
