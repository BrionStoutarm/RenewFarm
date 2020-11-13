using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : MonoBehaviour
{
    public int m_food = 100;
    public int m_sleep = 100;
    public int m_water = 100;

    // Start is called before the first frame update

    public Transform goal;

    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    // Tell the villager what they need to do
    void FixedUpdate()
    {
        
    }
}
