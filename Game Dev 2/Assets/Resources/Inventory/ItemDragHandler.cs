using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    private Transform originalParent;
    public GameObject imageParentObject;
    private bool isDragging;

    Transform player;

    private void Awake()
    {
        imageParentObject = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    } 

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Inventory.Instance.currentInventoryItems[imageParentObject.GetComponent<Slot>().slotIndex] != null)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                print("Down!");
                isDragging = true;
                originalParent = transform.parent;
                transform.SetParent(transform.parent.parent.parent);
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            } else if (eventData.button == PointerEventData.InputButton.Right)
            {
                Vector3 playerPosition = player.transform.position;
                Vector3 playerDirection = player.transform.forward;
                Quaternion playerRotation = player.transform.rotation;
                Vector3 playerPos = playerPosition + playerDirection * 3;
                Inventory.Instance.currentInventoryItems[imageParentObject.GetComponent<Slot>().slotIndex].transform.position = playerPos;
                Inventory.Instance.currentInventoryItems[imageParentObject.GetComponent<Slot>().slotIndex].transform.rotation = playerRotation;
                Inventory.Instance.currentInventoryItems[imageParentObject.GetComponent<Slot>().slotIndex].SetActive(true);
                Inventory.Instance.currentInventoryItems[imageParentObject.GetComponent<Slot>().slotIndex] = null;
                Inventory.Instance.UpdateSlotUI();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(Inventory.Instance.currentInventoryItems[imageParentObject.GetComponent<Slot>().slotIndex] != null && eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        } 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = false;
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
    
}
