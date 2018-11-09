using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public int id;
    public string title;
    public string description;
    public Sprite icon;
    public GameObject prefab;

    public Item (int id, string title, string description, Sprite icon, GameObject prefab)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon = icon;
        this.prefab = prefab;
    }
}
