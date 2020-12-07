using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    int currentNode;
    int previousNode;
    public int enemyNum;

    public enum EnemyState
    {
        patrol,
        chase
    };

    EnemyState enemyState = EnemyState.patrol;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(enemyNum == 1)
        {
            currentNode = Random.Range(0, GameManager.gm.nodes.Length);
        }
        else if (enemyNum == 2)
        {
            currentNode = Random.Range(0, GameManager.gm.nodes2.Length);
        }
        previousNode = currentNode;
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyState)
        {
            case EnemyState.patrol: Patrol();  break;
            case EnemyState.chase: Chase();  break;
            default: break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "node")
        {
            if (enemyNum == 1)
            {
            currentNode = Random.Range(0, GameManager.gm.nodes.Length);
            while (currentNode == previousNode)
            {
                currentNode = Random.Range(0, GameManager.gm.nodes.Length);
            }
            previousNode = currentNode;
            }
            else if (enemyNum == 2)
            {
                currentNode = Random.Range(0, GameManager.gm.nodes2.Length);
                while (currentNode == previousNode)
                {
                    currentNode = Random.Range(0, GameManager.gm.nodes2.Length);
                }
                previousNode = currentNode;
            }
        }

        if(other.tag == "Player")
        {
            enemyState = EnemyState.chase;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyState = EnemyState.patrol;
        }
    }

    void Patrol()
    {
        if(enemyNum == 1)
        {
           agent.destination = GameManager.gm.nodes[currentNode].position;
        }
        else if (enemyNum == 2)
        {
           agent.destination = GameManager.gm.nodes2[currentNode].position;
        }
    }

    void Chase()
    {
        agent.destination = GameManager.gm.player.transform.position;
    }
}
