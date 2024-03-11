using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        this.gameObject.SetActive(false);
    }
}
