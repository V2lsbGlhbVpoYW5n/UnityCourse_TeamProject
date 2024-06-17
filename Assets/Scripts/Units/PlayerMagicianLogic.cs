using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = System.Random;

public class PlayerMagicianLogic : UnitLogic
{
    [SerializeField] GameObject m_Fireball;

    [SerializeField] GameObject m_FireballSpawn;
    [SerializeField]
    AudioClip m_FireballCastSound;
    [SerializeField]
    AudioSource m_audioSource;

    void PlaySound(AudioClip audioClip)
    {
        m_audioSource.PlayOneShot(audioClip);
    }

    protected override void Attack()
    {
        if (enemies.Count > 0)
        {
            attackTarget = enemies[rand.Next(enemies.Count)];
        }

        if (attackTarget != null)
        {
            transform.LookAt(attackTarget.transform);
            m_audioSource = GetComponent<AudioSource>();
            PlaySound(m_FireballCastSound);
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
                if (col.gameObject.CompareTag("ComputerShooter") || col.gameObject.CompareTag("ComputerWarrior") ||
                    col.gameObject.CompareTag("ComputerMagician") ||
                    col.gameObject.CompareTag("ComputerInfrastructure"))
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