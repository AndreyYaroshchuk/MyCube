using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameOver : MonoBehaviour
{
    private const string TAG_WALL = "Wall";

    private float stopGameTimer = 0f;

    public event EventHandler OnGameOver;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TAG_WALL)
        {
            Handheld.Vibrate();
            OnGameOver?.Invoke(this, EventArgs.Empty);          
        }
    }  
}
