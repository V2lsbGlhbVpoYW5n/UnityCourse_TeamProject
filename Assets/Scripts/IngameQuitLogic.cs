using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameQuitLogic : MonoBehaviour
{
    public void OnQuitClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
