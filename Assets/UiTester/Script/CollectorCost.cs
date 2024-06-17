using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectorCost : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    string m_lockey;
    TextMeshProUGUI m_TMPro;
    [SerializeField]
    private GameObject m_basement;
    PlayerBasement m_playerBasement;
    TextMeshProUGUI m_TMP;
    void Start()
    {
        m_TMP = GetComponent<TextMeshProUGUI>();
        m_playerBasement = m_basement.GetComponent<PlayerBasement>();
    }

    int GetCost()
    {
        return m_playerBasement.GetCurrentCollectorCost();
    }

    void SetText()
    {
        int cost = GetCost();
        if (cost != 1145141919)
        {
            m_TMP.SetText(LocalizationManager.m_Instance.GetLocalisedString(m_lockey) + ": " + cost);
        }
        else
        {
            m_TMP.SetText("maximum");
        }
    }
    private void Update()
    {
        SetText();
    }
}
