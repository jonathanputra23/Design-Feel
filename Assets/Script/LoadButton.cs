using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    [SerializeField]
    private Image myIcon;

    public void SetIcon(Sprite mySprite)
    {
        myIcon.sprite = mySprite;
    }
}