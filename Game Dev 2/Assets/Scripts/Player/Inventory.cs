using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory Instance { get; protected set; }

    public bool[] isFull;
    public GameObject[] slots;
    public List<GameObject> currentInventoryItems = new List<GameObject>();

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
}
