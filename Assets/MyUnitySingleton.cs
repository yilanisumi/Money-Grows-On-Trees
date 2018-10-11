using UnityEngine;
using System.Collections;

public class MyUnitySingleton : MonoBehaviour
{

    private static MyUnitySingleton instance = null;
    public static MyUnitySingleton Instance
    {
        get { return instance; }
    }
    void Awake()
    {


        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void StopMusic()
    {
        AudioSource introMusic = GetComponent<AudioSource>();
        introMusic.Stop();
    }
    public void StartMusic()
    {
        AudioSource introMusic = GetComponent<AudioSource>();
        introMusic.Play();
    }
}