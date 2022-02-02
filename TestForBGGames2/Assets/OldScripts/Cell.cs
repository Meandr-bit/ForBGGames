using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject WallLeft, WallRight, WallTop, WallBottom, FinishZone, DeathCubePrefab;
    public Vector2Int coord = new Vector2Int();
    public bool FinishCell = false;

    public bool GenerateDeathCubes()
    {
        if (transform.position.x < 4.6f && transform.position.z < 4.3f && Random.value > 0.85f && MazeSpawner.instance.deathCubesSpawned < MazeSpawner.instance.maxDeathCubes && coord.x > 2 && coord.y > 2 && !FinishCell)
        {
            //GameObject b = Instantiate(DeathCubePrefab, transform.position, Quaternion.identity, transform);
            //b.transform.localPosition = new Vector3(0.514f, 0.521f, 1.203f);
            //b.transform.localEulerAngles = Vector3.zero;
            DeathCubePrefab.SetActive(true);
            return true;
        }
        return false;
    }
}
