using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combinator : Machine {

    public GameObject currentEggOne;
    public GameObject currentEggTwo;
    bool combine = true;
    public Transform spawnPoint;

    private void Update()
    {
        if (myInventory[0] != null)
        {
            currentEggOne = myInventory[0];
        } else
        {
            currentEggOne = null;
        }
        if (myInventory[1] != null)
        {
            currentEggTwo = myInventory[1];
        } else
        {
            currentEggTwo = null;
        }
        if (currentEggOne != null && currentEggTwo != null && combine == true)
        {
            combine = false;
            StartCoroutine(Combine());
        }
    }

    public void CombineTrigger()
    {
        combine = true;
    }

    IEnumerator Combine()
    {
        yield return new WaitForSeconds(0);
        GameObject newEgg = DetermineEgg(currentEggOne.GetComponent<Egg>(), currentEggTwo.GetComponent<Egg>());
        yield return new WaitForSeconds(5);
        newEgg.transform.position = spawnPoint.position;
        newEgg.SetActive(true);
        myInventory[0] = null;
        myInventory[1] = null;
        currentEggOne = null;
        currentEggTwo = null;
        combine = true;
    }

    GameObject DetermineEgg(Egg eggOne, Egg eggTwo)
    {
        GameObject newEgg;
        if(eggOne.mySpecies != CreatureSpecies.Baseboi && eggTwo.mySpecies == CreatureSpecies.Baseboi)
        {
            newEgg = Instantiate(eggOne.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            newEgg.SetActive(false);
        } else if (eggTwo.mySpecies != CreatureSpecies.Baseboi && eggOne.mySpecies == CreatureSpecies.Baseboi)
        {
            newEgg = Instantiate(eggTwo.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            newEgg.SetActive(false);
        } else
        {
            newEgg = Instantiate(eggOne.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            newEgg.SetActive(false);
        }
        return newEgg;
    }
}
