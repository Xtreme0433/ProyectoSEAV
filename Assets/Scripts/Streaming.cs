using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;


public class Streaming : MonoBehaviour {

    [DllImport("StreamingPlugin", EntryPoint = "get_streamer", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr get_streamer();
    
    [DllImport("StreamingPlugin", EntryPoint = "streamer_setup", CallingConvention = CallingConvention.Cdecl)]
    private static extern void streamer_setup(IntPtr streamerHandle);

    [DllImport("StreamingPlugin", EntryPoint = "streamer_start", CallingConvention = CallingConvention.Cdecl)]
    private static extern int streamer_start(IntPtr streamerHandle, string url, int width, int height, int fps, int bitrate);

    [DllImport("StreamingPlugin", EntryPoint = "streamer_sendVideoFrame", CallingConvention = CallingConvention.Cdecl)]
    private static extern int streamer_sendVideoFrame(IntPtr streamerHandle, IntPtr data);

    [DllImport("StreamingPlugin", EntryPoint = "streamer_stop", CallingConvention = CallingConvention.Cdecl)]
    private static extern int streamer_stop(IntPtr streamerHandle);
    
    [DllImport("StreamingPlugin", EntryPoint = "streamer_release", CallingConvention = CallingConvention.Cdecl)]
    private static extern void streamer_release(IntPtr streamerHandle);
    
    
    public String url;
    public RenderTexture renderTexture;
    private Texture2D texture;
    
    private bool streaming = false;
    private int frameCounter;
    private IntPtr streamer;

    
    void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 24;
    }
    
    void Start ()
    {

        texture = new Texture2D(renderTexture.width, renderTexture.height);
        
        streamer = get_streamer();
        streamer_setup(streamer);

        frameCounter = 0;
        streaming = true;
        int result = streamer_start(streamer, url, renderTexture.width, renderTexture.height, 60, 2000000);
        Debug.Log("Start " + result);
    }

    // Update is called once per frame
    void Update ()
    {
        //if (Input.GetKeyDown("1") || SeCargaUnaNuevaEscena())
        //{
        //    frameCounter = 0;
        //    streaming = true;
        //    int result = streamer_start(streamer, url, renderTexture.width, renderTexture.height, 60, 2000000);
        //    Debug.Log("Start " + result);
        //}

        if (Input.GetKeyDown("2"))
        {
            streaming = false;
            streamer_stop(streamer);
            Debug.Log("Stop");
        }

        if (streaming)
        {
            RenderTexture tempRenderTexture = RenderTexture.GetTemporary(renderTexture.width, renderTexture.height, 0, renderTexture.format);
            RenderTexture.active = tempRenderTexture;
            GL.PushMatrix();
            GL.LoadPixelMatrix(0, renderTexture.width, 0, renderTexture.height);
            Graphics.DrawTexture(new Rect(0, 0, renderTexture.width, renderTexture.height), renderTexture);
            GL.PopMatrix();            
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(tempRenderTexture);
            
            
            Color32[] color32 = texture.GetPixels32();
            GCHandle dataHandle = GCHandle.Alloc(color32, GCHandleType.Pinned);
            streamer_sendVideoFrame(streamer, dataHandle.AddrOfPinnedObject());
            dataHandle.Free();

            frameCounter++;

            if (Input.GetKeyDown("q"))
            {
                Application.Quit();
            }
        }
    }

    void OnGUI()
    {
        if (streaming) GUI.Button(new Rect(0, 0, 150, 50), "STREAMING " + frameCounter);
    }

    void OnApplicationQuit()
    {
        if (streaming) streamer_stop(streamer);
        streamer_release(streamer);
    }
    bool SeCargaUnaNuevaEscena()
    {
        // Aquí puedes agregar la lógica para determinar si se está cargando una nueva escena
        // Puedes utilizar SceneManager para esto
        // Por ejemplo, puedes comparar el nombre de la escena actual con el nombre de la escena anterior
        string escenaActual = SceneManager.GetActiveScene().name;
        // Almacena el nombre de la escena actual para comparar luego
        string escenaAnterior = PlayerPrefs.GetString("EscenaAnterior", "");

        // Si el nombre de la escena actual es diferente al nombre de la escena anterior, se está cargando una nueva escena
        if (escenaActual != escenaAnterior)
        {
            // Actualiza el nombre de la escena anterior
            PlayerPrefs.SetString("EscenaAnterior", escenaActual);
            PlayerPrefs.Save();
            return true;
        }

        return false;
    }
}
