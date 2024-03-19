using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingRotation : MonoBehaviour
{

    public float rotationSpeed = 2.0f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
