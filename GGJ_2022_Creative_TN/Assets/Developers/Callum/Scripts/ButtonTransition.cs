using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTransition : MonoBehaviour
{
    public Color wantedcolour;
    public Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeButtonColour()
    {
        buttonText.color = wantedcolour;
    }
}