using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VueltaMenú : MonoBehaviour
{

    public string sceneToLoad; // Nombre de la escena a cargar

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }

    }
}
