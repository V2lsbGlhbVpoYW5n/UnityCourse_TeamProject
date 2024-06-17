using System;
using UnityEngine;

public class PlayerShooterLogic : UnitLogic
{
    [SerializeField] private Transform HitOrigin;
    [SerializeField] Transform m_leftHandPos, m_rightHandPos, m_lookAtPos;

    [SerializeField] GameObject m_gun;

    [SerializeField] GameObject m_bullet;

    [SerializeField] Transform m_bulletPos;

    [SerializeField] AudioClip m_shootSound;
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
            RaycastHit hit;
            Instantiate(m_bullet, m_bulletPos);
            m_audioSource = GetComponent<AudioSource>();
            PlaySound(m_shootSound);
            animator.SetTrigger("Fire");
            if (Physics.Raycast(HitOrigin.position, HitOrigin.forward, out hit, attackRadius))
            {
                if (hit.collider.CompareTag("ComputerShooter") || hit.collider.CompareTag("ComputerWarrior") ||
                    hit.collider.CompareTag("ComputerMagician") || hit.collider.CompareTag("ComputerInfrastructure"))
                {
                    hit.collider.TryGetComponent(out UnitLogic ulogic);
                    hit.collider.TryGetComponent(out ComputerBasement blogic);
                    hit.collider.TryGetComponent(out CollectorLogic clogic);
                    if (ulogic != null)
                    {
                        ulogic.GetHurt(ATK);
                    }
                    if (blogic != null)
                    {
                        blogic.GetHurt(ATK);
                    }
                    if (clogic != null)
                    {
                        clogic.GetHurt(ATK);
                    }
                }
            }
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
                    col.gameObject.CompareTag("ComputerMagician") || col.gameObject.CompareTag("ComputerInfrastructure"))
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

    void OnAnimatorIK(int layerIndex)
    {
        //if (ReloadCounter <= 0)
        //{
        // IK for Left Hand
        animator.SetIKPosition(AvatarIKGoal.LeftHand, m_leftHandPos.position);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, m_leftHandPos.rotation);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        //}

        // IK for Right Hand
        animator.SetIKPosition(AvatarIKGoal.RightHand, m_rightHandPos.position);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        animator.SetIKRotation(AvatarIKGoal.RightHand, m_rightHandPos.rotation);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        // Look at Target Pos
        animator.SetLookAtPosition(m_lookAtPos.position);
        animator.SetLookAtWeight(1.0f);
    }
    
    protected override void Die()
    {
        is_dead = true;
        Destroy(gameObject, 0.0f);
    }
}