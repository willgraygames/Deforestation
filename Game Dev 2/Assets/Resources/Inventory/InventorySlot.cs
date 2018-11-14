using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image myImage;
    public Sprite mySprite;

    private void Awake()
    {
        myImage = GetComponent<Image>();
    } 

    public void ChangeSprite (Sprite newSprite)
    {
        myImage.enabled = true;
        mySprite = newSprite;
    }

    public void EmptySprite ()
    {
        mySprite = null;
        myImage.enabled = false;
    }
}
