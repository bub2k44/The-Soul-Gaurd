using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public abstract class Animal : NPC
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
    public bool isFleeState;
    public bool isAlertState;
    #endregion

    #region ParticleSystems
    public ParticleSystem sleepyFX;
    public ParticleSystem attackFX;
    public ParticleSystem playFX;
    #endregion

    public NavMeshAgent _navMeshAgent;
    public Animator _anim;
    public Collider targetInRange;

    public float thirstDuration;
    public float awakeDuration;

    [SerializeField]
    private CanvasGroup healthGroup;

    public bool isActive;

    public override void Deselect()
    {
        healthGroup.alpha = 0;

        base.Deselect();
    }
    public override Transform Select()
    {
        healthGroup.alpha = 1;

        return base.Select();
    }

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

    protected override void Start()
    {
        thirstDuration = animalStats.thirstDuration;
        awakeDuration = animalStats.awakeDuration;

        MyHealth.Initialized(animalStats.maxHealth, animalStats.maxHealth);
    }

    protected virtual void Update()
    {
        Thirst();
        Sleepy();
        Look();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        
        OnHealthChanged(MyHealth.MyCurrentValue);
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

    public void FleeTarget(Transform target) => _navMeshAgent.SetDestination(target.transform.position - _navMeshAgent.transform.position);

    protected virtual void Look()
    {   
        Debug.DrawRay(eyes.position, eyes.forward.normalized * animalStats.lookRadius, Color.blue);  
    }

    public IEnumerator ResetAnimal()
    {
        yield return new WaitForSeconds(30);
        gameObject.SetActive(true);
    }
}