using UnityEngine;

public class PlayerWarriorLogic : UnitLogic
{
protected override void Attack()
    {
        if (attackTarget != null)
        {
            navMeshAgent.SetDestination(attackTarget.transform.position);
            attackTarget.TryGetComponent(out UnitLogic unitLogic);
            if (unitLogic != null && !unitLogic.GetDeath())
            {
                transform.LookAt(attackTarget.transform);
                Collider[] colliders =
                    Physics.OverlapSphere(transform.position, attackRadius, LayerMask.GetMask("Default"));
                if (colliders.Length > 0)
                {
                    foreach (var col in colliders)
                    {
                        if (col.gameObject.CompareTag("ComputerShooter") ||
                            col.gameObject.CompareTag("ComputerWarrior") ||
                            col.gameObject.CompareTag("ComputerMagician") || col.gameObject.CompareTag("ComputerInfrastructure"))
                        {
                            col.TryGetComponent(out UnitLogic ulogic);
                            col.TryGetComponent(out ComputerBasement blogic);
                            col.TryGetComponent(out CollectorLogic clogic);
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
                            cooldown = MAX_COOLDOWN;
                            animator.SetTrigger("Attack");
                        }
                    }
                }
            }
            else
            {
                attackTarget = null;
            }
        }
        else
        {
            if (enemies.Count > 0)
            {
                attackTarget = enemies[rand.Next(enemies.Count)];
            }
        }
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
        }
        else
        {
            status = UnitStatus.Move;
        }
    }

    protected override void Die()
    {
        is_dead = true;
        animator.SetTrigger("Die");
        Destroy(gameObject, 2.0f);
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
}