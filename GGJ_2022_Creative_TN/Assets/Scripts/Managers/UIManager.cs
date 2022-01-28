using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Game HUD")] 
    public UITime timeUI;
    public UIScore scoreUI;
    public UIHealth healthUI;
}
