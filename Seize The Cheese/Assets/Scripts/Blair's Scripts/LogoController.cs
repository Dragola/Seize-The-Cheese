using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoController : MonoBehaviour
{
    public void MoveToMain()
    {
        SceneManager.LoadScene(1);
    }
}
