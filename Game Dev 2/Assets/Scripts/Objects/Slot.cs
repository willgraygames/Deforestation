using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

    public int slotIndex;

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            Inventory.Instance.isFull[slotIndex] = false;
        }
    }

	public void DropItem ()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().DropItem();
            Destroy(child.gameObject);
        }
    }
}
