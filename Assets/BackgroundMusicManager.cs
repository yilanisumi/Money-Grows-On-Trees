using UnityEngine;
using System.Collections;

public class BackgoundMusicManager : MonoBehaviour
{
    AudioClip clip;
    // Use this for initialization
    void Start()
    {
        clip = GetComponent<AudioSource>().clip;
        GameObject gameMusic = GameObject.Find("Game Music");
        if (gameMusic != null)
        {
            MyUnitySingleton myUnitySingleton = gameMusic.GetComponent<MyUnitySingleton>();
            myUnitySingleton.StopMusic();
            myUnitySingleton.GetComponent<AudioSource>().clip = clip;
            myUnitySingleton.StartMusic();
        }
        else {
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
