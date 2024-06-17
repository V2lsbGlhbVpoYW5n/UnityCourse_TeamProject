using UnityEngine;
using System.Collections;

public class CameraLogic : MonoBehaviour
{
    public float Speed = 40;
    public float zoomSpeed = 20;

    [SerializeField]
    AudioClip m_BGM;

    AudioSource m_audioSource;

    // Use this for initialization
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.loop = true;
        PlaySound();
    }


    void PlaySound()
    {
        m_audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(
            Input.GetAxis("Horizontal") * Speed * Time.deltaTime,
            Input.GetAxis("Vertical") * Speed * Time.deltaTime,
            0);
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (GetComponent<Camera>().fieldOfView <= 80) // 小于一个放大范围后就不继续放大了
            {
                GetComponent<Camera>().fieldOfView += zoomSpeed;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (GetComponent<Camera>().fieldOfView >= 30) // 小于一个放大范围后就不继续放大了
            {
                GetComponent<Camera>().fieldOfView -= zoomSpeed;
            }
        }
    }
    
}