using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; protected set; }

    public GameObject[] eggs;
    void Awake ()
    {
        if (Instance != null)
        {
            print("There should only be one Game Manager");
        } else
        {
            Instance = this;
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

//Enumerations for different creature properties
public enum CreatureElement
{
    Fire,
    Earth,
    Water,
    Air,
}

public enum CreatureFood
{
    Any,
    Meat,
    Vegetables,
    Fruit
}

public enum CreatureSpecies
{
    Fireboi,
    Earthboi,
    Waterboi,
    Airboi,
    Baseboi
}

public enum CreatureGender
{
    Male,
    Female
}
