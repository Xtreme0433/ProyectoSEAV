using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;

    // Nombre de la escena en la que deseas reiniciar el objeto AudioManager
    public string sceneToReloadOn = "Menú 2";

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == sceneToReloadOn)
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
                instance = null;
            }
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == sceneToReloadOn)
        {
            PlayMusic();
        }
    }

    private void Update()
    {
        // Puedes agregar lógica adicional en Update si es necesario
    }



    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

  
}
