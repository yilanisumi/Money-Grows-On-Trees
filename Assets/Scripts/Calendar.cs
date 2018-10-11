using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Calendar : MonoBehaviour {
    public float seasDuration = 40;
    string[] calendar;
    public GameObject dateText;
    float timer;
    public int month;
    public GameObject gameOverText;
    StockInventory inventory;
    public GameObject scoreText;
    double score;
	// Use this for initialization
	void Start () {
        calendar = new[] { "Spring", "Summer", "Fall", "Winter" };
        timer = 0;
        month = 0;
        if (GameObject.Find("StockDatabase") != null)
        {
            inventory = GameObject.Find("StockDatabase").GetComponent<StockInventory>();
        }
        score = 0;
       
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > seasDuration)
        {
            timer = 0;
            month += 1;
            inventory.ChangeSeason(month);
            //GameObject.Find("World").GetComponent<World>().RejevunateTrees();
            if(month < calendar.Length)
            {
                dateText.GetComponent<TextMesh>().text = "Date: " + calendar[month];
            }
            else
            {
                GameOver();
            }
            
        }		
	}
    void GameOver()
    {
        Time.timeScale = 0;
        gameOverText.GetComponent<TextMesh>().text = "Game Over";
        CalculateScore();
        float flot = (float)score;
        scoreText.GetComponent<TextMesh>().text = "Score: $" + flot.ToString("F2");
    }
    void CalculateScore()
    {
        score = inventory.money;
        foreach (DictionaryEntry pair in inventory.stocksInventory)
        {
            int num = (int)pair.Value;
            string name = (string)pair.Key;
            Company c = (Company)inventory.companyTranslation[name];
            List<double> costs = (List<double>)inventory.stocksValue[c];
            score += costs[3] * num;
        }
    }
}
