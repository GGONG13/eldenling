using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEatting : MonoBehaviour
{

    private void Update()
    {
        RaycastHit hit;
        // ĳ������ ��ġ���� �������� ������ �߻�
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;

        float maxDistance = 10f;
        int layerMask = LayerMask.GetMask("Sword");
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerMask))
        {

            Debug.Log("Found an object - distance: " + hit.distance + ", name: " + hit.collider.gameObject.name);
            
            
        }
    }

}
