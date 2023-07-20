using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using static UnityEditor.Experimental.GraphView.GraphView;


public class CubeHolder : MonoBehaviour
{
    public static event EventHandler OnAddCubeHolder;

    private const string TAG_PICKUP = "Pickup";
    private const string TAG_UNTAGGED = "Untagged";
    private const string PREFABS_PLATFORM = "TrackGround";

    [SerializeField] private TrailRenderer trailRenderer;
   
    private void Update()
    {
        MovetCubeHolder();   
    }
    private void MovetCubeHolder()
    {
        if (enabled == true)
            transform.position = new Vector3(Player.Instance.gameObject.transform.position.x,transform.position.y, Player.Instance.gameObject.transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {     
        if (other.tag == TAG_PICKUP)
        {
            other.gameObject.tag = TAG_UNTAGGED;
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Player.Instance.gameObject.transform.position = new Vector3(Player.Instance.gameObject.transform.position.x, Player.Instance.gameObject.transform.position.y + 1, Player.Instance.gameObject.transform.position.z);
            other.gameObject.transform.position = new Vector3(Player.Instance.gameObject.transform.position.x, Player.Instance.gameObject.transform.position.y - 0.5f, Player.Instance.gameObject.transform.position.z);
            other.GetComponent<CubeHolder>().enabled = true;
            OnAddCubeHolder?.Invoke(this, EventArgs.Empty);         
        }   
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.name == PREFABS_PLATFORM)
        {
            trailRenderer.enabled = true;
        } 
    }
}
