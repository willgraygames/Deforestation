using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IDropHandler {

    public int slotIndex;

    public GameObject myImageObject;
    public Image myImage;
    public Sprite mySprite;
    public bool myInventory;
    public GameObject myMachine;

    private void Awake()
    {
        myImageObject = transform.GetChild(0).gameObject;
        myImage = transform.GetChild(0).gameObject.GetComponent<Image>();
        myImageObject.SetActive(false);
    }

    private void Update()
    {
        for (int i = 0; i < Inventory.Instance.slots.Length; i++)
        {
            if (Inventory.Instance.slots[i] == this.gameObject)
            {
                myInventory = true;
            }
        }
    }

    public void UpdateSlotUI ()
    {
        if (myInventory == true)
        {
            if (Inventory.Instance.currentInventoryItems[slotIndex] != null)
            {
                myImage.sprite = Inventory.Instance.currentInventoryItems[slotIndex].GetComponent<Item>().icon;
                myImageObject.SetActive(true);
            }
            else
            {
                myImage.sprite = null;
                myImageObject.SetActive(false);
            }
        } else
        {
            if (myMachine != null)
            {
                if (myMachine.GetComponent<Machine>().myInventory[slotIndex] != null)
                {
                    myImage.sprite = Inventory.Instance.activeMachine.GetComponent<Machine>().myInventory[slotIndex].GetComponent<Item>().icon;
                    myImageObject.SetActive(true);
                }
                else
                {
                    myImage.sprite = null;
                    myImageObject.SetActive(false);
                }
            }

        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (myInventory == true)
        {
            GameObject droppedItem = Inventory.Instance.currentInventoryItems[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetComponent<Slot>().slotIndex];
            if (eventData.pointerDrag.transform.parent.name == gameObject.name)
            {
                return;
            }
            if (Inventory.Instance.currentInventoryItems[slotIndex] == null)
            {
                Inventory.Instance.currentInventoryItems[slotIndex] = droppedItem;
                Inventory.Instance.currentInventoryItems[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetComponent<Slot>().slotIndex] = null;
                Inventory.Instance.UpdateSlotUI();
            }
            else
            {
                GameObject tempItem = Inventory.Instance.currentInventoryItems[slotIndex];
                Inventory.Instance.currentInventoryItems[slotIndex] = droppedItem;
                Inventory.Instance.currentInventoryItems[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetComponent<Slot>().slotIndex] = tempItem;
                Inventory.Instance.UpdateSlotUI();
            }
        } else
        {
            print("This one at least");
            //This is where other windows come in
            GameObject droppedItem = Inventory.Instance.currentInventoryItems[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetComponent<Slot>().slotIndex];
            /*if (eventData.pointerDrag.transform.parent.name == gameObject.name)
            {
                return;
            }*/
            print(Inventory.Instance.activeMachine.name);
            if (Inventory.Instance.activeMachine.GetComponent<Machine>().myInventory[slotIndex] == null)
            {
                print("This is procking");
                Inventory.Instance.activeMachine.GetComponent<Machine>().myInventory[slotIndex] = droppedItem;
                Inventory.Instance.currentInventoryItems[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetComponent<Slot>().slotIndex] = null;
                Inventory.Instance.UpdateSlotUI();
            }
            else
            {
                GameObject tempItem = Inventory.Instance.activeMachine.GetComponent<Machine>().myInventory[slotIndex];
                Inventory.Instance.activeMachine.GetComponent<Machine>().myInventory[slotIndex] = droppedItem;
                Inventory.Instance.currentInventoryItems[eventData.pointerDrag.GetComponent<ItemDragHandler>().transform.parent.GetComponent<Slot>().slotIndex] = tempItem;
                Inventory.Instance.UpdateSlotUI();
            }
        }


    }
}
