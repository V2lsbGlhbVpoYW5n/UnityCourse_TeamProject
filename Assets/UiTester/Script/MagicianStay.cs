using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianStay : MonoBehaviour
{
    [SerializeField]
    GameObject m_Fireball;
    [SerializeField]
    GameObject m_FireballSpawn;
    // Start is called before the first frame update
    Vector3 m_initialPosition;
    Quaternion m_initialRotation;
    void Start()
    {
        m_initialPosition = transform.position;
        m_initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_initialPosition;
        transform.rotation = m_initialRotation;
    }
    public void ReleaseFireball()
    {
        Instantiate(m_Fireball, m_FireballSpawn.transform.position, m_FireballSpawn.transform.rotation);
    }
}
