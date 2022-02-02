using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeathZone")
        {
            Debug.Log("DeathzoneTouched");
            if (!PlayerController.instance.ShiledIsActive)
            {
                StartCoroutine(PlayerController.instance.DeathAndRespawn());
            }
        }
        if (other.tag == "Finish")
        {
            StartCoroutine(PlayerController.instance.Confetti());
            StartCoroutine(UIController.instance.LevelCompleted());
        }
    }
}
