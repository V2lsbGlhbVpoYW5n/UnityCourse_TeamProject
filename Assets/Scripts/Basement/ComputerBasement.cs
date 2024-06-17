using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ComputerBasement : MonoBehaviour
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
    [SerializeField] private int MAX_HP = 10000;
    private int m_HP;

    [SerializeField] private GameObject m_unitManager;

    private int m_currentCollector = 0;

    private UnitType m_nextExpectedUnitType;

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
        m_nextExpectedUnitType = UnitType.Collector;
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

        if (m_cash >= GetNextExpectedUnitCost(m_nextExpectedUnitType))
        {
            UnitSpawn(m_nextExpectedUnitType);
        }
        
    }

    private System.Collections.IEnumerator IncrementCash()
    {
        while (true)
        {
            if (m_collector1 != null && m_collector1.activeSelf)
            {
                m_cash += m_collectorYield + 1;
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

    public void GetHurt(int delta)
    {
        m_HP -= delta;
    }

    public void UnitSpawn(UnitType type)
    {
        m_cash -= GetNextExpectedUnitCost(type);
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
        m_nextExpectedUnitType = NextExpectedUnitType();
    }

    private void LateUpdate()
    {
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

    private UnitType NextExpectedUnitType()
    {
        var values = Enum.GetValues(typeof(UnitType));
        Random random = new Random();
        int randomValue = random.Next(10);
        switch (randomValue)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                return UnitType.Warrior;
            case 4:
            case 5:
            case 6:
                return UnitType.Shooter;
            case 7:
            case 8:
                return UnitType.Magician;
            case 9:
                return UnitType.Collector;
            default:
                return UnitType.Warrior;
        }
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
}
