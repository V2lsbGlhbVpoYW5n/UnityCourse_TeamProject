using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLogic : MonoBehaviour
{
    // The lifetime of the fireball

    // The speed of the fireball
    protected float m_LifeTime = 2.0f;
    [SerializeField]
    protected float m_Speed = 15.0f;

    [SerializeField] protected int ATK;

    [SerializeField]
    protected GameObject m_FireballVFX;

    //[SerializeField]
    //AudioClip m_FireballSound;

    //AudioSource m_audioSource;
    
    Rigidbody m_rigidbody;
    const float SPEED = 10.0f;

    protected void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.velocity = transform.forward * SPEED;
        //m_FireballVFX.SetActive(true);
        m_FireballVFX.transform.localScale = Vector3.one * 1.0f;
    }

    protected void Update()
    {

        m_LifeTime -= Time.deltaTime;
        if (m_LifeTime < 0.0f)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
}
