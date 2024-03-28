using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalManager : MonoBehaviour
{
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnerPos;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    Instantiate(portalPrefab, hit.point, transform.rotation);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(ballPrefab, ballSpawnerPos.position, transform.rotation);

        }
    }
}