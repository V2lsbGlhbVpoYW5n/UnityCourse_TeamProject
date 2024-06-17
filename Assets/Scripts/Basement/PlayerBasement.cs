using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class PlayerBasement : MonoBehaviour
{
    [SerializeField] private GameObject m_collector1;
    [SerializeField] private GameObject m_collector2;
    [SerializeField] private GameObject m_collector3;
    [SerializeField] private GameObject m_collector4;

    [SerializeField] private Transform m_spawnPos;
    
    [SerializeField] private GameObject m_warrior;
    [SerializeField] private GameObject m_shooter;
    [SerializeField] private GameObject m_magician;

    [SerializeField] private int m_collectorYield = 2;
    private int m_cash;
    private int m_HP;
    [SerializeField] private int MAX_HP = 10000;

    [SerializeField] private GameObject m_unitManager;

    private int m_currentCollector = 0;

    [SerializeField] private TextMeshProUGUI m_cashText;
    [SerializeField] private TextMeshProUGUI m_HPText;

    public enum UnitType
    {
        Warrior,
        Shooter,
        Magician,
        Collector,
    }
    
    // Start is called before the first frame update
    void Start()
    {
        m_cash = 0;
        m_HP = MAX_HP;
        StartCoroutine(IncrementCash());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_HP <= 0)
        {
            if (m_collector1 != null)
            {
                Destroy(m_collector1);
            }
            if (m_collector2 != null)
            {
                Destroy(m_collector2);
            }
            if (m_collector3 != null)
            {
                Destroy(m_collector3);
            }
            if (m_collector4 != null)
            {
                Destroy(m_collector4);
            }
            Destroy(gameObject);
        }

        SetCashText();
        SetHPText();
    }

    private System.Collections.IEnumerator IncrementCash()
    {
        while (true)
        {
            if (m_collector1 != null && m_collector1.activeSelf)
            {
                m_cash += m_collectorYield;
            }
            if (m_collector2 != null && m_collector2.activeSelf)
            {
                m_cash += m_collectorYield;
            }
            if (m_collector3 != null && m_collector3.activeSelf)
            {
                m_cash += m_collectorYield;
            }
            if (m_collector4 != null && m_collector4.activeSelf)
            {
                m_cash += m_collectorYield * 2;
            }
            m_cash += 1;
            yield return new WaitForSeconds(0.025f);
        }
    }
    
    private int GetNextExpectedUnitCost(UnitType type)
    {
        switch (type)
        {
            case UnitType.Warrior:
                return 300;
            case UnitType.Shooter:
                return 600;
            case UnitType.Magician:
                return 1200;
            case UnitType.Collector:
                switch (m_currentCollector)
                {
                    case 0:
                        return 400;
                    case 1:
                        return 2000;
                    case 2:
                        return 6000;
                    case 3:
                        return 12000;
                }
                break;
        }
        return 0;
    }

    public int GetCurrentCollectorCost()
    {
        switch (m_currentCollector)
        {
            case 0:
                return 400;
            case 1:
                return 2000;
            case 2:
                return 6000;
            case 3:
                return 12000;
            default:
                return 1145141919;
        }
    }

    public void GetHurt(int delta)
    {
        m_HP -= delta;
        Debug.Log("AAA");
    }

    private void UnitSpawn(UnitType type)
    {
        if (m_cash >= GetNextExpectedUnitCost(type))
        {
            m_cash -= GetNextExpectedUnitCost(type);
        }
        else
        {
            return;
        }
        switch (type)
        {
            case UnitType.Warrior:
                Instantiate(m_warrior, m_spawnPos.position, m_spawnPos.rotation);
                break;
            case UnitType.Shooter:
                Instantiate(m_shooter, m_spawnPos.position, m_spawnPos.rotation);
                break;
            case UnitType.Magician:
                Instantiate(m_magician, m_spawnPos.position, m_spawnPos.rotation);
                break;
            case UnitType.Collector:
                switch (m_currentCollector)
                {
                    case 0:
                        m_collector1.SetActive(true);
                        m_currentCollector += 1;
                        break;
                    case 1:
                        m_collector2.SetActive(true);
                        m_currentCollector += 1;
                        break;
                    case 2:
                        m_collector3.SetActive(true);
                        m_currentCollector += 1;
                        break;
                    case 3:
                        m_collector4.SetActive(true);
                        m_currentCollector += 1;
                        break;
                }
                break;
        }
    }

    private void LateUpdate()
    {
        // maintain m_collector
        if (m_collector4 != null && !m_collector4.activeSelf)
        {
            m_currentCollector = 3;
        }
        if (m_collector3 != null && !m_collector3.activeSelf)
        {
            m_currentCollector = 2;
        }
        if (m_collector2 != null && !m_collector2.activeSelf)
        {
            m_currentCollector = 1;
        }
        if (m_collector1 != null && !m_collector1.activeSelf)
        {
            m_currentCollector = 0;
        }
    }
    
    // Functions for interaction
    // This function is called by external trigger to interactive with the basement and buy units.
    // All money and other problems have already been maintained by other functions.
    // What needs to be noticed is that the sequence of the collectors and the next collector are also well maintained.
    // The cost of the next collector can be got through function "GetUnitCost"
    public void BuyUnit(string unitStr) // give the name of the unit with the first letter Capitalized
    {
        switch (unitStr)
        {
            case "Warrior":
                UnitSpawn(UnitType.Warrior);
                break;
            case "Shooter":
                UnitSpawn(UnitType.Shooter);
                break;
            case "Magician":
                UnitSpawn(UnitType.Magician);
                break;
            case "Collector":
                UnitSpawn(UnitType.Collector);
                break;
        }
    }
    
    // -1 means there is no this unit (especially no more new collector)
    public int GetUnitCost(string unitStr)
    {
        int cost = -1;
        switch (unitStr)
        {
            case "Warrior":
                cost = GetNextExpectedUnitCost(UnitType.Warrior);
                break;
            case "Shooter":
                cost = GetNextExpectedUnitCost(UnitType.Shooter);
                break;
            case "Magician":
                cost = GetNextExpectedUnitCost(UnitType.Magician);
                break;
            case "Collector":
                cost = GetNextExpectedUnitCost(UnitType.Collector);
                break;
        }
        return cost == 0 ? -1 : cost;
    }

    public int GetCash()
    {
        return m_cash >= 0 ? m_cash : 0;
    }

    public int GetBasementHP()
    {
        return m_HP >= 0 ? m_HP : 0;
    }

    public float GetBasementHP_frac()
    {
        // May be useful when making a HP bar
        return m_HP >= 0 ? m_HP / MAX_HP : 0f;
    }

    private void OnDestroy()
    {
        // sound effect or visual effect can be added here
    }

    public void SetCashText()
    {
        m_cashText.SetText("Cash: "+GetCash());
    }

    public void SetHPText()
    {
        m_HPText.SetText("Basement HP: "+ GetBasementHP());
    }
}
