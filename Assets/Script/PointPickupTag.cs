using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPickupTag : MonoBehaviour
{
    private const string TAG_WALL = "Wall";

    private float timeDestroyCubeHolder = 5f;

    [SerializeField] private CubeHolder cubeHolder;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TAG_WALL)
        {
            cubeHolder.enabled = false;
            cubeHolder.GetComponent<Rigidbody>().drag = 2;
            Destroy(cubeHolder.gameObject, timeDestroyCubeHolder);   
        }
    }
    
}
