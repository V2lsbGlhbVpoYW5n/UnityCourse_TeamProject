using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = System.Random;

public class UnitLogic : MonoBehaviour
{
    public enum UnitStatus
    {
        Move,
        Fight,
    }

    [SerializeField] protected float distributingOffset;
    [SerializeField] protected float detectionRadius;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected float spacingFactor;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float MAX_COOLDOWN;

    [SerializeField] protected int HP;
    [SerializeField] protected int ATK;

    protected Random rand = new Random();
    protected List<GameObject> enemies = new List<GameObject>();
    protected GameObject attackTarget;

    protected NavMeshAgent navMeshAgent;
    protected Vector3 movementDest;

    public UnitStatus status = UnitStatus.Move;

    protected Animator animator;

    protected bool is_dead;

    public bool GetDeath()
    {
        return is_dead;
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        movementDest = transform.position;
        animator = GetComponent<Animator>();
        is_dead = false;
    }

    void Update()
    {
        if (is_dead)
        {
            return;
        }
        if (HP <= 0)
        {
            Die();
        }

        if (status == UnitStatus.Fight)
        {
            DetectEnemies();
            if (cooldown <= 0)
            {
                Attack();
            }
        }
        else if (status == UnitStatus.Move)
        {
            navMeshAgent.destination = movementDest;
            transform.LookAt(navMeshAgent.destination);
            DetectEnemies();
        }

        AnimationUpdate();
    }

    private void LateUpdate()
    {
        if (is_dead)
        {
            return;
        }
        cooldown -= Time.deltaTime;
        enemies.Clear();
    }

    protected virtual void Attack()
    {
    }

    protected void Align()
    {
        Vector3 sumPosition = Vector3.zero;
        foreach (GameObject enemy in enemies)
        {
            sumPosition += enemy.transform.position;
        }

        Vector3 averagePosition = sumPosition / enemies.Count;

        float offsetX = (float)(rand.NextDouble() * distributingOffset - distributingOffset / 2);
        float offsetZ = (float)(rand.NextDouble() * distributingOffset - distributingOffset / 2);
        Vector3 randOffset = new Vector3(offsetX, 0, offsetZ);

        Vector3 direction = (averagePosition - transform.position).normalized;

        Vector3 targetPosition = averagePosition + randOffset;

        targetPosition -= direction * spacingFactor * attackRadius;

        if (!is_dead)
        {
            navMeshAgent.SetDestination(targetPosition);
        }
    }

    public void SetMovementDest(Vector3 dest)
    {
        movementDest = dest;
    }

    protected virtual void DetectEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, LayerMask.GetMask("Default"));
        if (colliders.Length > 0)
        {
            foreach (var col in colliders)
            {
            }
        }

        if (enemies.Count > 0)
        {
            status = UnitStatus.Fight;
            Align();
        }

        else
        {
            status = UnitStatus.Move;
        }
    }

    public void GetHurt(int delta)
    {
        HP -= delta;
    }

    protected virtual void Die()
    {
        is_dead = true;
        Destroy(gameObject);
    }

    protected virtual void AnimationUpdate()
    {
        
    }

    public Vector3 GetMovementDest()
    {
        return movementDest;
    }
}