using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : MonoBehaviour
{
    public GameObject _magicArrow;

    public float Damage;

    public float MagicRadius = 5;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
