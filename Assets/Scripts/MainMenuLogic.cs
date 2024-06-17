using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField]
    AudioClip m_audioClip;
    AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        PlaySound(m_audioClip);
    }

    void PlaySound(AudioClip audioClip)
    {
        m_audioSource.PlayOneShot(audioClip);
    }
    public void OnStartClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
