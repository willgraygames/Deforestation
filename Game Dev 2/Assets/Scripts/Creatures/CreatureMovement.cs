using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class CreatureMovement : MonoBehaviour {

    public float speed;
    public NavMeshAgent myNav;
    public Transform currentTarget;
	public GameObject currentBreedTarget;
	public GameObject offspring;
    CreatureType myCreatureInfo;

    Coroutine activeEatCoroutine;
	Coroutine activeBreedCoroutine;

	public Animator myAnimator;

    public float wanderRadius;
    public float wanderTimer;
    private bool wander;
    private bool isEating;
	private bool isBreeding;

    public float checkRadius;
    public LayerMask checkDropLayer;
	public LayerMask checkCreatureLayer;
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
		myAnimator = GetComponentInChildren<Animator> ();
    }

    void Update ()
    {
		float rand = UnityEngine.Random.Range (0, 3);
		timer += Time.deltaTime * rand;

        if (!isEating)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkDropLayer);
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
		if (!isBreeding && myCreatureInfo.creatureHunger >= (myCreatureInfo.maxCreatureHunger * 0.9f)) {
			Collider[] colliders = Physics.OverlapSphere (transform.position, checkRadius, checkCreatureLayer);
			Array.Sort (colliders, new DistanceCompare (transform));
			CheckForPartner (colliders);
		} else if (isBreeding) {
			float lastDist = distanceFromTarget;
			if (currentTarget != null) {
				distanceFromTarget = Vector3.Distance (transform.position, currentTarget.position);
				agent.SetDestination (currentTarget.position);
			} else if (currentTarget == null) {
				print ("Breed stopped");
				StopCoroutine (activeBreedCoroutine);
				isBreeding = false;
				wander = true;
			}
		}

        if (timer >= wanderTimer && wander == true)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

		if (agent.velocity.x > 0 || agent.velocity.z > 0) {
			myAnimator.SetBool ("isWalking", true);
		} else {
			myAnimator.SetBool ("isWalking", false);
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
						if (!isEating && !isBreeding)
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

	void CheckForPartner (Collider[] colliders) 
	{
		for (int i = 0; i < colliders.Length; i++) 
		{
			if (colliders [i].gameObject.tag == "Creature") 
			{
                for (int a = 0; a < myCreatureInfo.acceptableMates.Length; a++) 
				{
					if (colliders [i].gameObject.GetComponent<CreatureType>().myCreatureSpecies == myCreatureInfo.acceptableMates [a]) 
					{
						if (colliders [i].gameObject.GetComponent<CreatureType>().myCreatureGender != myCreatureInfo.myCreatureGender) 
						{
							if (colliders [i].gameObject.GetComponent<CreatureType> ().creatureHunger >= (colliders [i].gameObject.GetComponent<CreatureType> ().maxCreatureHunger)) 
							{
								currentBreedTarget = colliders [i].gameObject;
								if (colliders [i].gameObject.GetComponent<CreatureMovement> ().currentBreedTarget == null || colliders [i].gameObject.GetComponent<CreatureMovement> ().currentBreedTarget == this.gameObject) {
									colliders [i].gameObject.GetComponent<CreatureMovement> ().currentBreedTarget = this.gameObject;
									if (!isBreeding && !isEating) {
										wander = false;
										isBreeding = true;
										currentTarget = colliders [i].gameObject.transform;
										distanceFromTarget = Vector3.Distance (transform.position, currentTarget.position);
										activeBreedCoroutine = StartCoroutine (Breed (colliders [i].gameObject));
									}
								} else {
									currentBreedTarget = null;
									break;
								}
							}
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

    /*private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }*/

    IEnumerator Eat (GameObject food)
    {
        Food currentFoodTarget = food.gameObject.GetComponent<Food>();
        while (distanceFromTarget > 0.7f)
        {
            if (currentTarget != null)
            {
                yield return new WaitForSeconds(.03f);
            } else
            {
                yield break;
            }
        }

        if (isEating == true)
        {
			myAnimator.SetBool ("isEating", true);
            //Throw in Animation for eating
            myCreatureInfo.creatureHunger += currentFoodTarget.foodValue;
            Destroy(food);
            yield return new WaitForSeconds(3);
			myAnimator.SetBool ("isEating", false);
            isEating = false;
            wander = true;
        }
    }

	IEnumerator Breed (GameObject partner) {
		CreatureType partnerInfo = partner.GetComponent<CreatureType> ();
		while (distanceFromTarget > 1.5f) {
			if (currentTarget != null) {
				yield return new WaitForSeconds (0.3f);
			} else {
				yield break;
			}
		}
		if (isBreeding == true) {
			yield return new WaitForSeconds (2);
			myCreatureInfo.creatureHunger -= 50;
			if (myCreatureInfo.myCreatureGender == CreatureGender.Female) {
				Vector3 difference = (transform.position - currentTarget.position) / 2;
				Vector3 spawnPoint = transform.position + difference;
				Instantiate (offspring, transform.position, Quaternion.identity);
			}
			yield return new WaitForSeconds (2);
			isBreeding = false;
			wander = true;
		}
	}
}
