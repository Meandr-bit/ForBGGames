using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float explosionForce, upwardsMod, lifetimeOfCubes, radius;
    public List<Rigidbody> rbOfCubes = new List<Rigidbody>();

    void Start()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        foreach (Rigidbody rb in rbOfCubes)
        {
            rb.AddExplosionForce(explosionForce, transform.position, radius, upwardsMod);
        }
        yield return new WaitForSeconds(lifetimeOfCubes);
        foreach (Rigidbody rb in rbOfCubes)
        {
            Destroy(rb.gameObject);
        }
        Destroy(gameObject);
    }
}
