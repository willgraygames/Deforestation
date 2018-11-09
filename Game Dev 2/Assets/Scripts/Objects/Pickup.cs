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
	
	public void PickupObject ()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                inventory.currentInventoryItems.Add(gameObject);
                gameObject.SetActive(false);
                break;
            }
        }
    }
}
