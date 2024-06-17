using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // Start is called before the first frame update
    const float LIFECYCLE = 2.0f;
    float m_lifeCycle;
    void Start()
    {
        m_lifeCycle = LIFECYCLE;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_lifeCycle <= 0)
        {
            Destroy(gameObject);
        }
        m_lifeCycle -= Time.deltaTime;
    }
}
