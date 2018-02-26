using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SpiderScript : MonoBehaviour {
    private NavMeshAgent agent;
    private Animator anim;

    private float timer;
    private Vector3 nextPosition;
    private bool gazedAt = false;
    private int health = 0;
    private float  inactiveTime = 0.0f;
    private bool isActive = true;
    private SpiderGenerator sp;

    public float travelTime = 5f;
    public float walkSpeed = 2f;
    public float walkableRadius = 5f;
    public bool startRandomPosition = false;
    public int healthDecrement = 1;
    public float timeToReturn = 5.0f;
    public int MaxHealth = 100;
    public UnityEvent addToScore;

    // Use this for initialization
	void Start ()
    {
        this.isActive = true;
        this.agent = GetComponent<NavMeshAgent>();
        this.anim = GetComponent<Animator>();
        SetHealth(MaxHealth);
        this.agent.transform.position = Random.insideUnitSphere * walkableRadius;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer > travelTime)
        {
            if (isActive)
            {
                timer = 0.0f;
                nextPosition = Random.insideUnitSphere * walkableRadius;
                //nextPosition.y = 0f;
                agent.SetDestination(nextPosition);
                anim.SetFloat("walkSpeed", walkSpeed);
            }
        }

        if (gazedAt) {
            incHealth(-healthDecrement);
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

        if (health <= 0) {
            Debug.Log("Spider die");
            isActive = false;
            anim.SetBool("die", true);
            gameObject.SetActive(false); // Desaparece la araña del mapa
            this.sp.addToScore();
        }

        updateInactiveTime();   
    }

    // Activa la bandera cuando el usuario esta viendo al objeto
    public void SetGazedAt(bool gazedAt)
    {
        this.gazedAt = gazedAt;
    }

    /**
     * Modifica la vida de la araña
     */
    public void SetHealth(int value)
    {
        this.health = value;
        if (health <= 0) health = 0;
        this.anim.SetInteger("health", health); // Envía el valor de la vida a la animación
    }


    public void incHealth(int value = -1)
    {
        health += value;
        if (health <= 0) health = 0;
        this.anim.SetInteger("health", health);
    }

    /**
     * Actualiza el tiempo que la araña ha estado inactiva (¡¡Muerta!! :O)
     */
    void updateInactiveTime()
    {

        if (!isActive)
        {
            Debug.Log("Time " + inactiveTime);
            inactiveTime += Time.deltaTime;
        }
    }

    /**
     * Vuelve a mostrar en escena a la araña y ajusta sus atributos
     */
    public void reactivate()
    {
        if (inactiveTime > timeToReturn)
        {
            Debug.Log("Reactivando " + inactiveTime);
            this.isActive = true;
            inactiveTime = 0.0f;
            timer = 0.0f;
            SetHealth(MaxHealth);
            gameObject.SetActive(true);
            Debug.Log("Restaurando araña" + this.ToString());
        }
    }

    public SpiderGenerator getGenerator()
    {
        return this.sp;
    }

    public void setGenerator(SpiderGenerator sp)
    {
        this.sp = sp;
    }
}
