using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerShooterLogic : UnitLogic
{
    [SerializeField] private Transform HitOrigin;
    [SerializeField] Transform m_leftHandPos, m_rightHandPos, m_lookAtPos;

    [SerializeField] GameObject m_gun;

    [SerializeField] GameObject m_bullet;

    [SerializeField] Transform m_bulletPos;

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
            animator.SetTrigger("Fire");
            if (Physics.Raycast(HitOrigin.position, HitOrigin.forward, out hit, attackRadius))
            {
                if (hit.collider.CompareTag("PlayerShooter") || hit.collider.CompareTag("PlayerWarrior") ||
                    hit.collider.CompareTag("PlayerMagician") || hit.collider.CompareTag("PlayerInfrastructure"))
                {
                    hit.collider.TryGetComponent(out UnitLogic ulogic);
                    hit.collider.TryGetComponent(out PlayerBasement blogic);
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