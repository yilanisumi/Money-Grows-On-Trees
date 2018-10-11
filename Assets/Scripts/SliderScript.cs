using UnityEngine.UI;
using UnityEngine;
using System.Collections;


public class SliderScript : MonoBehaviour
{
    public Text sliderValue;
    public Slider slider;
    StockInventory inventory;
    void Start()
    {
        inventory = GameObject.Find("StockDatabase").GetComponent<StockInventory>();
    }
    void Update()
    {

        sliderValue.text = "Starting Budget: $" + slider.value.ToString("0.0");
        inventory.startMoney = slider.value;

    }
    void SetMoney()
    {
        Debug.Log("Slider:" + slider.value);
        inventory.startMoney = slider.value;
        Debug.Log(inventory.startMoney);
    }
}