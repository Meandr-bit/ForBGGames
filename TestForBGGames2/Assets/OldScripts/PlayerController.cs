using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject Finish, Cube, Explosion, ConfettiParticles;
    public bool ShiledIsActive = false;

    public Color defaultColor, shieldedColor;
    

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(WaitAndGo());
    }

    public void EnableShield()
    {
        ShiledIsActive = true;
        Cube.GetComponent<MeshRenderer>().material.color = shieldedColor;
        Cube.GetComponent<BoxCollider>().enabled = false;
    }
    public void DisableShield()
    {
        ShiledIsActive = false;
        Cube.GetComponent<MeshRenderer>().material.color = defaultColor;
        Cube.GetComponent<BoxCollider>().enabled = true;
    }

    public IEnumerator DeathAndRespawn()
    {
        //отключаем куб
        Cube.GetComponent<BoxCollider>().enabled = false;
        Cube.GetComponent<MeshRenderer>().enabled = false;
        //GetComponent<NavMeshAgent>().speed = 0;
        //GetComponent<NavMeshAgent>().enabled = false;
        //запускаем анимацию
        Instantiate(Explosion, transform.position, transform.rotation);
        //ждем пару сек
        yield return new WaitForSeconds(2f);
        //"спавним" игрока на старте
        //transform.position = new Vector3(-3.5f, 0.08333629f, -3.5f);
        GetComponent<NavMeshAgent>().Warp(new Vector3(-3.5f, 0.08333629f, -3.5f));
        transform.rotation = Quaternion.Euler(0, 90, 0);
        Cube.GetComponent<BoxCollider>().enabled = true;
        Cube.GetComponent<MeshRenderer>().enabled = true;
        //GetComponent<NavMeshAgent>().speed = 1.5f;
        yield return new WaitForSeconds(2f);
        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = Finish.transform.position;
    }

    IEnumerator WaitAndGo()
    {
        yield return new WaitForSeconds(2f);
        //GetComponent<UnityEngine.AI.NavMeshAgent>().destination = Finish.transform.position;
        GetComponent<AIPath>().destination = Finish.transform.position;
    }

    public void PlayConfetti()
    {
        StartCoroutine(Confetti());
    }

    public IEnumerator Confetti()
    {
        ConfettiParticles.SetActive(true);
        yield return new WaitForSeconds(5f);
        ConfettiParticles.SetActive(false);
    }
}
