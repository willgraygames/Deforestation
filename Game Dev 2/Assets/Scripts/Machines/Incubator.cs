using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incubator : Machine {

    public GameObject currentEgg;
    bool hatch = false;
    public Transform spawnPoint;

    private void Update()
    {
        currentEgg = myInventory[0];
        if (currentEgg != null)
        {
            currentEgg.GetComponent<Egg>().incubateTime -= 1;
            if (currentEgg.GetComponent<Egg>().incubateTime <= 0 && hatch == false)
            {
                hatch = true;
                StartCoroutine(HatchEgg());
            }
        }
    } 

    IEnumerator HatchEgg ()
    {
        if (Inventory.Instance.activeMachine == this.gameObject)
        {
            Instantiate(currentEgg.GetComponent<Egg>().myCreaturePrefab, spawnPoint.position, Quaternion.identity);
            currentEgg = null;
            myInventory[0] = null;
            mySlots[0].UpdateSlotUI();
            hatch = false;
        } else
        {
            for (int i = 0; i < Inventory.Instance.allMachines.Length; i++)
            {
                if(Inventory.Instance.allMachines[i] == this.gameObject)
                {
                    Instantiate(currentEgg.GetComponent<Egg>().myCreaturePrefab, spawnPoint.position, Quaternion.identity);
                    currentEgg = null;
                    myInventory[0] = null;
                    mySlots[0].UpdateSlotUI();
                    hatch = false;
                }
            }
        }
        yield return new WaitForSeconds(0);
    }
}
