using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory Instance { get; protected set; }

    public GameObject playerInventoryPanel;
    public GameObject[] slots;
    public GameObject[] currentInventoryItems;

    private void Awake()
    {
        if (Instance != null)
        {
            print("there should only be one inventory");
        } else
        {
            Instance = this;
        }
    }

    public void UpdateSlotUI ()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Slot>().UpdateSlotUI();
        }
    }
}
