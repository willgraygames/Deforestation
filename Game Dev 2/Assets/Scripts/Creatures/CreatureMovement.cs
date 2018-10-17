using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class CreatureMovement : MonoBehaviour {

    public float speed;
    public NavMeshAgent myNav;
    public Transform currentTarget;
    CreatureType myCreatureInfo;

    Coroutine activeEatCoroutine;

    public float wanderRadius;
    public float wanderTimer;
    private bool wander;
    private bool isEating;

    public float checkRadius;
    public LayerMask checkLayers;
    private float distanceFromTarget;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    void OnEnable ()
    {
        myCreatureInfo = GetComponent<CreatureType>();
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        wander = true;
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if (!isEating)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
            Array.Sort(colliders, new DistanceCompare(transform));
            CheckForFood(colliders);
        } else
        {
            float lastDist = distanceFromTarget;
            if (currentTarget != null)
            {
                distanceFromTarget = Vector3.Distance(transform.position, currentTarget.position);
                agent.SetDestination(currentTarget.position);
            } else if (currentTarget == null)
            {
                print("Stopped");
                StopCoroutine(activeEatCoroutine);
                isEating = false;
                wander = true;
            }
        }

        if (timer >= wanderTimer && wander == true)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    void CheckForFood (Collider[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Food")
            {
                if (colliders[i].gameObject.GetComponent<Food>().myFoodType == myCreatureInfo.myCreatureFood || myCreatureInfo.myCreatureFood == CreatureFood.Any)
                {
                    if (myCreatureInfo.creatureHunger < myCreatureInfo.maxCreatureHunger)
                    {
                        if (!isEating)
                        {
                            wander = false;
                            isEating = true;
                            currentTarget = colliders[i].gameObject.transform;
                            distanceFromTarget = Vector3.Distance(transform.position, currentTarget.position);
                            activeEatCoroutine = StartCoroutine(Eat(colliders[i].gameObject));
                        }
                    }
                }
            }
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    IEnumerator Eat (GameObject food)
    {
        Food currentFoodTarget = food.gameObject.GetComponent<Food>();
        print("Going to Target");
        while (distanceFromTarget > 0.5f)
        {
            if (currentTarget != null)
            {
                yield return new WaitForSeconds(.03f);
            } else
            {
                yield break;
            }
        }
        print("Made It");
        if (isEating == true)
        {
            print("Is Eating");
            //Throw in Animation for eating
            myCreatureInfo.creatureHunger += currentFoodTarget.foodValue;
            Destroy(food);
            yield return new WaitForSeconds(1);
            isEating = false;
            wander = true;
        }
    }
}
