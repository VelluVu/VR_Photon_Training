using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Buildable : Throwable
{

    public MovableSnapPointDetection snappingLogic;
    MovableType _movableType;

    public override void GetReleaseVelocities ( Hand hand, out Vector3 velocity, out Vector3 angularVelocity )
    {
        base.GetReleaseVelocities ( hand, out velocity, out angularVelocity );
    }

    protected override void Awake ( )
    {
        base.Awake ( );

        if (gameObject.GetComponent<MovableSnapPointDetection>() != null )
        {
            snappingLogic = gameObject.GetComponent<MovableSnapPointDetection> ( );
            _movableType = snappingLogic.movableType; 
        }
        else
        {
            _movableType = MovableType.Movable;
        }
    }

    protected override void HandAttachedUpdate ( Hand hand )
    {
        base.HandAttachedUpdate ( hand );
    }

    protected override void HandHoverUpdate ( Hand hand )
    {
        base.HandHoverUpdate ( hand );
    }

    protected override IEnumerator LateDetach ( Hand hand )
    {
        return base.LateDetach ( hand );
    }

    protected override void OnAttachedToHand ( Hand hand )
    {
        base.OnAttachedToHand ( hand );

        if ( _movableType == MovableType.Buildable)
        {
            snappingLogic.snapS = false;
        }
    }

    protected override void OnDetachedFromHand ( Hand hand )
    {
        base.OnDetachedFromHand ( hand );

        if ( _movableType == MovableType.Buildable )
        {
            snappingLogic.snapS = true;
        }
    }

    protected override void OnHandFocusAcquired ( Hand hand )
    {
        base.OnHandFocusAcquired ( hand );
    }

    protected override void OnHandFocusLost ( Hand hand )
    {
        base.OnHandFocusLost ( hand );
    }

    protected override void OnHandHoverBegin ( Hand hand )
    {
        base.OnHandHoverBegin ( hand );
    }

    protected override void OnHandHoverEnd ( Hand hand )
    {
        base.OnHandHoverEnd ( hand );
    }
}
