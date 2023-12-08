using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CambiarNivel (string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
