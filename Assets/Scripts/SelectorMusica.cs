using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorMusica : MonoBehaviour
{
    public AudioSource reproductorDeAudio;


    public AudioClip cancion1;
    public AudioClip cancion2;
    public AudioClip cancion3;

    void Start()
    {
        // Configurar la primera canción como la canción predeterminada
        reproductorDeAudio.clip = cancion1;
        reproductorDeAudio.Play();
    }

    public void DropDown (int index) 
    { 
        switch (index)
        {
            case 0:
                reproductorDeAudio.clip = cancion1;
                break;
            case 1:
                reproductorDeAudio.clip = cancion2;
                break;
            case 2:
                reproductorDeAudio.clip = cancion3;
                break;
        }

        // Reproducir la nueva canción
        reproductorDeAudio.Play();

    }
}
