using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winingBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            Debug.Log("LEVEL UP");
            Destroy(other.gameObject);
        }
    }
}
