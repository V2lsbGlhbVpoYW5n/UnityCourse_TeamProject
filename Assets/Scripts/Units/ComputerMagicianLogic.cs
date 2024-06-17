using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMagicianLogic : UnitLogic
{
    [SerializeField]
    GameObject m_Fireball;
    [SerializeField]
    GameObject m_FireballSpawn;
    //[SerializeField]
    //AudioClip m_FireballCastSound;

    protected override void Attack()
    {
        if (enemies.Count > 0)
        {
            attackTarget = enemies[rand.Next(enemies.Count)];
        }

        if (attackTarget != null)
        {
            transform.LookAt(attackTarget.transform);
            animator.SetTrigger("Fireball");
            Instantiate(m_Fireball, m_FireballSpawn.transform.position, m_FireballSpawn.transform.rotation);
        }

        cooldown = MAX_COOLDOWN;
    }

    protected override void DetectEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, LayerMask.GetMask("Default"));
        if (colliders.Length > 0)
        {
            foreach (var col in colliders)
            {
                if (col.gameObject.CompareTag("PlayerShooter") || col.gameObject.CompareTag("PlayerWarrior") ||
                    col.gameObject.CompareTag("PlayerMagician") || col.gameObject.CompareTag("PlayerInfrastructure"))
                {
                    enemies.Add(col.gameObject);
                }
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

    protected override void AnimationUpdate()
    {
        if (Mathf.Abs(navMeshAgent.remainingDistance) > 0.1f)
        {
            animator.SetFloat("Run", 1.0f);
        }
        else
        {
            animator.SetFloat("Run", 0.0f);
        }
    }
    
    protected override void Die()
    {
        is_dead = true;
        animator.SetTrigger("Die");
        Destroy(gameObject, 2.0f);
    }
}
