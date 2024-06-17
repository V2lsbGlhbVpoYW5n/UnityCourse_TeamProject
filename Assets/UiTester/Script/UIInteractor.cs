using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject m_playerBasement;
    PlayerBasement m_basementLogic;
    void Start()
    {
        m_basementLogic = m_playerBasement.GetComponent<PlayerBasement>();
    }

    // Update is called once per frame

    public void Warrior()
    {
        m_basementLogic.BuyUnit("Warrior");
    }
    public void Shooter()
    {
        m_basementLogic.BuyUnit("Shooter");
    }
    public void Magician()
    {
        m_basementLogic.BuyUnit("Magician");
    }
    public void Collector()
    {
        m_basementLogic.BuyUnit("Collector");
    }
}
