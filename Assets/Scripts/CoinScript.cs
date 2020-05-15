using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        isCollected = true;
    }
}
