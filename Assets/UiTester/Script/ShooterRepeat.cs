using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRepeat : MonoBehaviour
{
    // Start is called before the first frame update
    Animator m_animator;
    [SerializeField]
    GameObject m_bullet;

    [SerializeField]
    Transform m_leftHandPos, m_rightHandPos, m_lookAtPos;

    [SerializeField]
    Transform m_bulletPos;
    Vector3 m_initialPosition;
    Quaternion m_initialRotation;

    float COOLDOWN = 2.0f;
    float m_cooldown;
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_cooldown = COOLDOWN;
        m_initialPosition = transform.position;
        m_initialRotation = transform.rotation;
    }

    void OnAnimatorIK(int layerIndex)
    {
        //if (ReloadCounter <= 0)
        //{
        // IK for Left Hand
        m_animator.SetIKPosition(AvatarIKGoal.LeftHand, m_leftHandPos.position);
        m_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        m_animator.SetIKRotation(AvatarIKGoal.LeftHand, m_leftHandPos.rotation);
        m_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        //}

        // IK for Right Hand
        m_animator.SetIKPosition(AvatarIKGoal.RightHand, m_rightHandPos.position);
        m_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        m_animator.SetIKRotation(AvatarIKGoal.RightHand, m_rightHandPos.rotation);
        m_animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        // Look at Target Pos
        m_animator.SetLookAtPosition(m_lookAtPos.position);
        m_animator.SetLookAtWeight(1.0f);

        // Left-Foot and Right-Foot IK
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_initialPosition;
        transform.rotation = m_initialRotation;
        m_cooldown -= Time.deltaTime;
        if (m_cooldown <= 0)
        {
            m_animator.SetTrigger("Fire");
            Instantiate(m_bullet, m_bulletPos);
            m_cooldown = COOLDOWN;
        }
    }
}
