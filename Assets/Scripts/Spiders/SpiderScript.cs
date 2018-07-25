using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SpiderScript : MonoBehaviour {
    private NavMeshAgent Agent;
    private Animator AnimatorController;
    private float Timer;
    private Vector3 NextPosition;
    private float  InactiveTime = 0.0f;
    private bool IsActive = true;
    private SpiderGenerator Generator;

    public float TravelTime = 5f;
    public float AnimationWalkSpeed = 2f;
    public float WalkableRadius = 5f;
    public float TimeToReactivate = 5.0f;

    private int HashAnimationDie = Animator.StringToHash("Die");
    private int HashAnimationOnMove = Animator.StringToHash("OnMove");
    private int HashAnimationWalkSpeed = Animator.StringToHash("WalkSpeed");

	void Start ()
    {
        this.IsActive = true;
        this.Agent = GetComponent<NavMeshAgent>();
        this.AnimatorController = GetComponent<Animator>();
        this.Agent.transform.position = Random.insideUnitSphere * WalkableRadius;
        this.Timer = TravelTime; // Para que inicie moviendose
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (this.IsActive)
        {
            this.Timer += Time.deltaTime;
            if (this.Timer > this.TravelTime)
            {
                if (this.IsActive) {
                    this.SetNewPosition();
                }
            }

            if (this.Agent.remainingDistance == 0.0f || (Mathf.Abs(Agent.velocity.x) < 0.1f && Mathf.Abs(Agent.velocity.z) < 0.1f))
            {
                this.AnimatorController.SetBool(HashAnimationOnMove, false);
                this.SetNewPosition();
            } else
            {
                this.AnimatorController.SetBool(HashAnimationOnMove, true);
            }
        }
        else
        {
            this.UpdateInactiveTime();
        }
    }
    
    public void OnClick()
    {
        this.Agent.isStopped = true;
        this.AnimatorController.SetBool(HashAnimationDie, true);
    }

    /*
     * Se llama después de terminar la animación [die]
     */
    public void Hide()
    {
        this.IsActive = false;
        this.Generator.AddToScore();
        gameObject.SetActive(false);
    }

    /**
     * Establece un  nuevo punto del mapa al cual se va a mover
     */ 
    void SetNewPosition()
    {
        this.Timer = 0.0f;
        this.NextPosition = Random.insideUnitSphere * this.WalkableRadius;
        this.Agent.SetDestination(NextPosition);
        this.AnimatorController.SetFloat(HashAnimationWalkSpeed, AnimationWalkSpeed);
    }

    /**
     * Actualiza el tiempo que la araña ha estado inactiva (¡¡Muerta!! :O)
     */
    void UpdateInactiveTime()
    {
        if (!this.IsActive)
        {
            this.InactiveTime += Time.deltaTime;
        }
    }

    /**
     * Vuelve a mostrar en escena a la araña y ajusta sus atributos
     */
    public void Reactivate()
    {
        if (this.InactiveTime > this.TimeToReactivate)
        {
            Vector3 pos;
            while (!RandomPointOnNavmesh(Vector3.zero, WalkableRadius, out pos)) Debug.Log("llamada");
            gameObject.transform.position = pos;
            this.IsActive = true;
            this.InactiveTime = 0.0f;
            this.Timer = 0.0f;
            gameObject.SetActive(true);
        } else
        {
            this.UpdateInactiveTime();
        }
    }

    /* 
     * Obtiene un punto aleatorio en el navmesh 
     */
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

    // Getters and Setters

    public SpiderGenerator GetGenerator()
    {
        return this.Generator;
    }

    public void SetGenerator(SpiderGenerator sg)
    {
        this.Generator = sg;
    }


}
