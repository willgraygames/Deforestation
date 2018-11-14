using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private Inventory inventory;
    public Item myItem;

	// Use this for initialization
	void Start () {
        inventory = Inventory.Instance;
	}
}
