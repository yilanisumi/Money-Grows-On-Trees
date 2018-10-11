using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockSingleton : MonoBehaviour
{

    private static StockSingleton instance = null;
    public static StockSingleton Instance
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
}
