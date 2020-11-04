using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIData", menuName = "Miniclip/UI/UIData")]
public class UIData : ScriptableObject
{
    [Header("Title Texts")]
    public string pauseTitleText = "PAUSE";

    [Header("Buttom Texts")]
    public string restartBtnText = "Restart";
    public string mainMenuBtnText = "Main Menu";

    [Space(15)]
    [Tooltip("A fake factor to make the height bigger and players happier :D ")]
    public int heightMultiplier;
    public Sprite liveSprite;
}