using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID { get; private set; }
    public string Name { get; private set; }

    public Item(int id, string name)
    {
        ID = id;
        Name = name;
    }
}
