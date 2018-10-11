using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureMovement : MonoBehaviour {

    public float speed;
    public NavMeshAgent myNav;
    public Transform[] currentFoodTargets;
    CreatureType myCreatureInfo;

    void Start ()
    {
        myCreatureInfo = GetComponent<CreatureType>();
    }

    void Update ()
    {

    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            if (other.gameObject.GetComponent<Food>().myFoodType == myCreatureInfo.myCreatureFood || myCreatureInfo.myCreatureFood == CreatureFood.Any)
            {
                
            }
        }
    }

}
