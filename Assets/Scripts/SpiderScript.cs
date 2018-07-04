using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SpiderScript : MonoBehaviour {
    private NavMeshAgent agent;
    private Animator animator;
    private float timer;
    private Vector3 nextPosition;
    private float  inactiveTime = 0.0f;
    private bool isActive = true;
    private SpiderGenerator spiderGenerator;

    public float travelTime = 5f;
    public float animationWalkSpeed = 2f;
    public float walkableRadius = 5f;
    public float timeToReturn = 5.0f;

	void Start ()
    {
        this.isActive = true;
        this.agent = GetComponent<NavMeshAgent>();
        this.animator = GetComponent<Animator>();
        this.agent.transform.position = Random.insideUnitSphere * walkableRadius;
        this.timer = travelTime; // Para que inicie con las arañas moviendose
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isActive)
        {
            this.timer += Time.deltaTime;
            if (this.timer > this.travelTime) {
                if (this.isActive) {
                    this.SetNewPosition();
                }
            }

            if (agent.remainingDistance == 0f || (Mathf.Abs(agent.velocity.x) < 0.1f && Mathf.Abs(agent.velocity.z) < 0.1f))
            {
                animator.SetBool("onMove", false);
                this.SetNewPosition();
            } else
            {
                animator.SetBool("onMove", true);
            }
        }
        else
        {
            UpdateInactiveTime();
        }
    }


    public void OnClick()
    {
        isActive = false;
        animator.SetTrigger("die");
        spiderGenerator.AddToScore();
        gameObject.SetActive(false); // Desaparece la araña del mapa
    }


    /**
     * Establece un  nuevo punto del mapa al cual se va a mover
     */ 
    void SetNewPosition()
    {
        this.timer = 0.0f;
        this.nextPosition = Random.insideUnitSphere * this.walkableRadius;
        this.agent.SetDestination(nextPosition);
        this.animator.SetFloat("walkSpeed", animationWalkSpeed);
    }

    /**
     * Actualiza el tiempo que la araña ha estado inactiva (¡¡Muerta!! :O)
     */
    void UpdateInactiveTime()
    {
        if (!isActive)
        {
            inactiveTime += Time.deltaTime;
        }
    }

    /**
     * Vuelve a mostrar en escena a la araña y ajusta sus atributos
     */
    public void Reactivate()
    {
        if (this.inactiveTime > this.timeToReturn)
        {
            Vector3 pos;
            while (!RandomPointOnNavmesh(Vector3.zero, walkableRadius, out pos)) Debug.Log("llamada");
            gameObject.transform.position = pos;
            this.isActive = true;
            this.inactiveTime = 0.0f;
            this.timer = 0.0f;
            gameObject.SetActive(true);
        } else
        {
            this.UpdateInactiveTime();
        }
    }

    public SpiderGenerator GetGenerator()
    {
        return this.spiderGenerator;
    }

    public void SetGenerator(SpiderGenerator sg)
    {
        this.spiderGenerator = sg;
    }

    /* obtiene un punto aleatorio en el navmesh */
    bool RandomPointOnNavmesh(Vector3 center, float range, out Vector3 result)
    {
        NavMeshHit hit;
        Vector3 randomPoint;
        for (int i = 0; i < 30; i++)
        {
            randomPoint = center + Random.insideUnitSphere * range;
            if (NavMesh.SamplePosition(randomPoint, out hit, 0.3f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
