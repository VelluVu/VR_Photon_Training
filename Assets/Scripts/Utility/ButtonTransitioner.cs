using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{
    public Color32 m_normalColor = Color.white;
    public Color32 m_hoverColor = Color.grey;
    public Color32 m_downColor = Color.white;

    private Image m_image = null;

    private void Awake ( )
    {
        m_image = GetComponent<Image> ( );
    }

    /// <summary>
    /// Loses Track of input system when laser is deactivated so this event turns the color back to normal...
    /// </summary>
    private void OnEnable ( )
    {
        GameController.laserIsOffEvent += BackToNormalColor;
    }

    private void OnDisable ( )
    {
        GameController.laserIsOffEvent -= BackToNormalColor;
    }

    public void BackToNormalColor()
    {
        m_image.color = m_normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_image.color = m_hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {     
        m_image.color = m_normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_image.color = m_downColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log ( "up" );
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_image.color = m_hoverColor;
        m_image.color = m_normalColor;
    }

}
