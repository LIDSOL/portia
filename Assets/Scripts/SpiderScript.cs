using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderScript : MonoBehaviour {
    private NavMeshAgent agent;
    private Animator anim;

    private float timer;
    private Vector3 nextPosition;
    private bool gazedAt = false;
    private int health = 100;

    public float travelTime = 5f;
    public float walkSpeed = 2f;
    public float walkableRadius = 5f;
    public bool startRandomPosition = false;

    // Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SetHealth(health);
        agent.transform.position = Random.insideUnitSphere * walkableRadius;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer > travelTime)
        {
            if (health > 0)
            {
                timer = 0.0f;
                nextPosition = Random.insideUnitSphere * walkableRadius;
                //nextPosition.y = 0f;
                agent.SetDestination(nextPosition);
                anim.SetFloat("walkSpeed", walkSpeed);
            }
        }

        if (gazedAt) {
            incHealth(-1);
        }

        if (Mathf.Abs(agent.velocity.y) > 0.2f)
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }

        if (agent.remainingDistance == 0f || (Mathf.Abs(agent.velocity.x) < 0.1f && Mathf.Abs(agent.velocity.z) < 0.1f))
        {
            anim.SetBool("onMove", false);
        }
        else
        {
            anim.SetBool("onMove", true);
        }

        if (health == 0) {
            Destroy(gameObject, 1.5f);
        }
    }

    public void SetGazedAt(bool gazedAt)
    {
        this.gazedAt = gazedAt;
    }

    public void SetHealth(int value)
    {
        health = value;
        if (health <= 0) health = 0;
        this.anim.SetInteger("health", health);
    }

    public void incHealth(int value = 1)
    {
        health += value;
        if (health <= 0) health = 0;
        this.anim.SetInteger("health", health);
    }

}
