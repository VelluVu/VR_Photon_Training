using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{

    public Camera m_camera;
    public SteamVR_Input_Sources m_targetSource;
    public SteamVR_Action_Boolean m_clickAction;

    GameObject m_currentObject = null;
    PointerEventData m_data = null;

    protected override void Awake ( )
    {
        base.Awake ( );
       
        m_data = new PointerEventData ( eventSystem );
    }

    public void SetEventCamera ( Camera cam )
    {
        m_camera = cam;
    }

    public override void Process()
    {
        
        m_data.Reset ( );
        m_data.position = new Vector2 ( m_camera.pixelWidth / 2, m_camera.pixelHeight / 2 );
        
        eventSystem.RaycastAll ( m_data, m_RaycastResultCache );
        m_data.pointerCurrentRaycast = FindFirstRaycast ( m_RaycastResultCache );
        m_currentObject = m_data.pointerCurrentRaycast.gameObject;
        
        m_RaycastResultCache.Clear ( );

        HandlePointerExitAndEnter ( m_data, m_currentObject );

        if ( m_clickAction.GetStateDown(m_targetSource))
        {
            ProcessPress ( m_data );
        }

        if ( m_clickAction.GetStateUp(m_targetSource))
        {
            ProcessRelease ( m_data );
        }
    }

    public PointerEventData GetData()
    {
        return m_data;
    }

    private void ProcessPress(PointerEventData data)
    {
        Debug.Log ( "press" );

        data.pointerPressRaycast = data.pointerCurrentRaycast;

        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy ( m_currentObject, data, ExecuteEvents.pointerDownHandler );

        if(newPointerPress == null)
        {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler> ( m_currentObject );
        }

        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_currentObject;
    }

    void ProcessRelease(PointerEventData data)
    {
        Debug.Log ( "release" );
        ExecuteEvents.Execute ( data.pointerPress, data, ExecuteEvents.pointerUpHandler );

        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler> ( m_currentObject );

        if (data.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute ( data.pointerPress, data, ExecuteEvents.pointerClickHandler );

        }

        eventSystem.SetSelectedGameObject ( null );
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;

    }

}
