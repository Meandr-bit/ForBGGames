using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject WallLeft, WallRight, WallTop, WallBottom, FinishZone, DeathCubePrefab;
    private void Start()
    {
        //GenerateDeathCubes();
    }

    public void GenerateDeathCubes()
    {
        if (transform.position.x < 4.6f && transform.position.z < 4.3f && Random.value > 0.85f)
        {
            GameObject b = Instantiate(DeathCubePrefab, transform.position, Quaternion.identity, transform);
            b.transform.localPosition = new Vector3(0.514f, 0.521f, 1.203f);
            b.transform.localEulerAngles = Vector3.zero;
        }
    }
}
