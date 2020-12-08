using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Animal : MonoBehaviour
{
    public AnimalStats animalStats;

    #region State Bool's
    public bool isIdleState;
    public bool isPatrolState;
    public bool isFindWaterState;
    public bool isDrinkState;
    public bool isSleepState;
    public bool isAwakenState;
    public bool isAttackState;
    public bool isPlayState;
    public bool isChaseState;
    public bool isDeathState;
    public bool isTakeDamageState;
    #endregion

    #region ParticleSystems
    public ParticleSystem sleepyFX;
    public ParticleSystem attackFX;
    public ParticleSystem playFX;
    public ParticleSystem attack;
    public ParticleSystem aaa;
    public ParticleSystem bbb;
    public ParticleSystem ccc;
    public ParticleSystem ddd;
    #endregion

    public NavMeshAgent _navMeshAgent;
    public Animator _anim;
    public Collider targetInRange;// TODO

    public float thirstDuration;
    public float awakeDuration;

    public HealthBar healthBar;
    public int currentHealth;

    public Transform eyes;
    public Transform _water;

    public Collider punch;
 


    private int _index; 
    public GameObject target;
    public Transform chaseTarget;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();      
        targetInRange = GetComponent<Collider>();
    }

    protected virtual void Start()
    {
        thirstDuration = animalStats.thirstDuration;
        awakeDuration = animalStats.awakeDuration;
        currentHealth = animalStats.maxHealth;
        healthBar.SetMaxHealth(animalStats.maxHealth);
    }

    protected virtual void Update()
    {
        //TODO
        //speed = _navMeshAgent.desiredVelocity.magnitude;
        //_anim.SetFloat("speed", speed);
        Thirst();
        Sleepy();
        Look();       
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    protected virtual void Thirst()
    {
        if (thirstDuration > 0)
        {
            if (!isIdleState && !isDrinkState && !isSleepState && !isAttackState && !isPlayState && !isFindWaterState)
            {
                thirstDuration -= Time.deltaTime;
            }
        }
    }

    protected virtual void Sleepy()
    {
        if (awakeDuration > 0)
        {
            if (!isIdleState && !isDrinkState && !isSleepState && !isAttackState && !isPlayState && !isFindWaterState)
            {
                awakeDuration -= Time.deltaTime;
            }
        }
    }

    public void Destination()
    {
        if (_navMeshAgent.remainingDistance < 1f)
        {
            var nextDestination = GetNextDestination();
            _navMeshAgent.SetDestination(nextDestination);
        }
    }

    [SerializeField] private Transform[] _destinations = default;

    //public Vector3 GetNextDestination()
    //{
    //    _index++;

    //    if (_index >= animalStats.destination.Length)
    //    {
    //        _index = 0;
    //    }

    //    return animalStats.destination[_index].position;
    //}
    public Vector3 GetNextDestination()
    {
        _index++;

        if (_index >= _destinations.Length)
        {
            _index = 0;
        }

        return _destinations[_index].position;
    }

    public void FindWater() => _navMeshAgent.SetDestination(_water.transform.position);

    public void FindTarget(Transform target) => _navMeshAgent.SetDestination(target.transform.position);

    protected virtual void Look()
    {   
        Debug.DrawRay(eyes.position, eyes.forward.normalized * animalStats.lookRadius, Color.blue);  
    }   
}