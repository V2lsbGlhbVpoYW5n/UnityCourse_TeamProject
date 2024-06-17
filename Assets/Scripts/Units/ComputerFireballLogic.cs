using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ComputerFireballLogic : FireballLogic
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerMagician") || other.gameObject.CompareTag("PlayerShooter") ||
            other.gameObject.CompareTag("PlayerWarrior") || other.gameObject.CompareTag("PlayerInfrastructure"))
        {
            other.gameObject.TryGetComponent(out UnitLogic ulogic);
            other.gameObject.TryGetComponent(out PlayerBasement blogic);
            other.gameObject.TryGetComponent(out CollectorLogic clogic);
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
            Destroy(gameObject);
        }
    }
}