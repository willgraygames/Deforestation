using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject item;
    Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    } 

    public void DropItem ()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;
        Vector3 playerPos = playerPosition + playerDirection * 3;
        Instantiate(item, playerPos, playerRotation);
    }
}
