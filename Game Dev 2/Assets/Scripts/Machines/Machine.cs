using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour {

    public GameObject myPanel;
    public Slot[] mySlots;
    public GameObject[] myInventory;
    public int id;
    public string title;
    public string description;

    private void Start()
    {
        myPanel.SetActive(false);
    } 

    public void OpenInteractWindow ()
    {
        myPanel.SetActive(true);
        Inventory.Instance.activeMachine = this.gameObject;
        for (int i = 0; i < mySlots.Length; i++)
        {
            mySlots[i].UpdateSlotUI();
        }
    }


}
