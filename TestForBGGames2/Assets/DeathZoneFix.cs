using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneFix : MonoBehaviour
{
    public int priority;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeathZone")
        {
            if(other.GetComponent<DeathZoneFix>().priority > priority)
            {
                Debug.Log("DeathZone turned off");
                MazeSpawner.instance.deathCubesSpawned--;
                gameObject.SetActive(false);
            }
        }
    }
}
