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
    public float animationWalkSpeed = 2f;
    public float walkableRadius = 5f;
    public bool startRandomPosition = false;
    public int healthDecrement = 1;
    public float timeToReturn = 5.0f;
    public int MaxHealth = 100;
    public UnityEvent addToScore;

	void Start ()
    {
        this.isActive = true;
        this.agent = GetComponent<NavMeshAgent>();
        this.anim = GetComponent<Animator>();
        this.setHealth(MaxHealth);
        this.agent.transform.position = Random.insideUnitSphere * walkableRadius;
        this.timer = travelTime; // Para que inicie con las arañas moviendose
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.timer += Time.deltaTime;
        if (this.timer > this.travelTime) {
            if (this.isActive) {
                this.setNewPosition();
            }
        }

        if (gazedAt)
        {
            decrementHealth();
        }
        if (agent.remainingDistance == 0f || (Mathf.Abs(agent.velocity.x) < 0.1f && Mathf.Abs(agent.velocity.z) < 0.1f))
        {
            anim.SetBool("onMove", false);
            this.setNewPosition();
        } else
        {
            anim.SetBool("onMove", true);
        }
        if (health <= 0)
        {
            isActive = false;
            anim.SetTrigger("die");
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("die")) {
                this.sp.addToScore();
                gameObject.SetActive(false); // Desaparece la araña del mapa
            }
        }
    }

    /**
     * Activa la bandera cuando el usuario esta viendo al objeto
     */
    public void SetGazedAt(bool gazedAt)
    {
        this.gazedAt = gazedAt;
    }

    /**
     * Establece un  nuevo punto del mapa al cual se va a mover
     */ 
    void setNewPosition()
    {
        this.timer = 0.0f;
        this.nextPosition = Random.insideUnitSphere * this.walkableRadius;
        this.agent.SetDestination(nextPosition);
        this.anim.SetFloat("walkSpeed", animationWalkSpeed);
        // Debug.Log("Moving spider to " + this.nextPosition.ToString());
    }

    /**
     * Modifica la vida de la araña
     */
    public void setHealth(int value)
    {
        this.health = value;
        if (health <= 0) health = 0;
        // this.anim.SetInteger("health", health); // Envía el valor de la vida a la animación
    }

    /**
     * Decrementa la vida con el límite definido en el editor
     */
    public void decrementHealth()
    {
        this.health -= this.healthDecrement;
        this.anim.SetInteger("health", health);
    }

    /**
     * Actualiza el tiempo que la araña ha estado inactiva (¡¡Muerta!! :O)
     */
    void updateInactiveTime()
    {

        if (!isActive)
        {
            // Debug.Log("Inactive time " + inactiveTime.ToString());
            inactiveTime += Time.deltaTime;
        }
    }

    /**
     * Vuelve a mostrar en escena a la araña y ajusta sus atributos
     */
    public void reactivate()
    {
        if (this.inactiveTime > this.timeToReturn)
        {
            this.isActive = true;
            this.inactiveTime = 0.0f;
            this.timer = 0.0f;
            setHealth(MaxHealth);
            gameObject.SetActive(true);
            // Debug.Log("Restaurando araña" + this.ToString());
        } else
        {
            this.updateInactiveTime();
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
