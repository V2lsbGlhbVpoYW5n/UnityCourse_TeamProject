using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocData : MonoBehaviour
{
    [SerializeField]
    string m_lockey;
    [SerializeField]
    int m_cost;
    TextMeshProUGUI m_TMP;
    // Start is called before the first frame update
    void Start()
    {
        m_TMP = GetComponent<TextMeshProUGUI>();
        if(m_TMP != null)
        {
            m_TMP.SetText(LocalizationManager.m_Instance.GetLocalisedString(m_lockey) + ": " + m_cost);
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_TMP.SetText(LocalizationManager.m_Instance.GetLocalisedString(m_lockey) + ": " + m_cost);
    }
}
