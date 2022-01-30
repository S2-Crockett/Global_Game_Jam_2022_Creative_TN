using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Colours")] 
    public Color defaultColour;
    public Color hoverColour;
        
    private Text myText;

    private void OnDisable()
    {
        myText.color = defaultColour;
    }

    void Start (){
        myText = GetComponentInChildren<Text>();
    }
 
    public void OnPointerEnter (PointerEventData eventData)
    {
        myText.color = hoverColour;
    }
 
    public void OnPointerExit (PointerEventData eventData)
    {
        myText.color = defaultColour;
    }
}
