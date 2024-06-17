using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFireballLogic : FireballLogic
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ComputerMagician") || other.gameObject.CompareTag("ComputerShooter") ||
            other.gameObject.CompareTag("ComputerWarrior") || other.gameObject.CompareTag("ComputerInfrastructure"))
        {
            other.gameObject.TryGetComponent(out UnitLogic ulogic);
            other.gameObject.TryGetComponent(out ComputerBasement blogic);
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