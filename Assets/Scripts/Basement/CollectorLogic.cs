using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorLogic : MonoBehaviour
{
    private int MAX_HP = 2500;
    private int m_HP;
    // Start is called before the first frame update
    void Start()
    {
        m_HP = MAX_HP;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_HP <= 0)
        {
            m_HP = MAX_HP;
            gameObject.SetActive(false);
        }
    }
    
    public void GetHurt(int delta)
    {
        m_HP -= delta;
        Debug.Log("AA1");
    }
}
