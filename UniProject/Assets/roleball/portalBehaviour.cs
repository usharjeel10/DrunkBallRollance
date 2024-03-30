using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalBehaviour : MonoBehaviour
{
    [SerializeField] private Transform SecondPortalPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            setNewPostionToEnemy(other.gameObject);
        }
    }

    private void setNewPostionToEnemy(GameObject enemy)
    {
        if (SecondPortalPos != null)
        enemy.transform.position = SecondPortalPos.position;
        enemy.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
    }
}
