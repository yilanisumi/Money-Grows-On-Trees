using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;
using System;
using System.Net;
using System.Xml;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

public class Company
{
    public string ticker;
    public string name;
    public double initStockVal;
    //  public string url{ get; }
    //  public double[] percentChanges;
    //  public double[] allAbsChanges;
    //  public double[] quarterChanges;
    public List<double> percentChanges;
    public List<double> allAbsChanges;
    public List<double> quarterChanges;
    public double avg, max, min, incrementer;
    public Company(string n, string t, double stock)
    {
        name = n;
        ticker = t;
        //      url = u;
        //      percentChanges = new double[12];
        //      allAbsChanges = new double[12];
        //      quarterChanges = new double[4];
        percentChanges = new List<double>();
        allAbsChanges = new List<double>();
        quarterChanges = new List<double>();

        avg = 0; max = 0; min = 0; incrementer = 0;

        initStockVal = stock;
    }
}


public class StockInventory : MonoBehaviour {
    public double money;
    int season = 0;
    public double startMoney = 1000;
    //  List<string> companies;
    //  Hashtable companyStocks;
    public Hashtable stocksInventory;
    public  Hashtable stocksValue;
    List<Company> companies;
    List<Company> realCompanies;
    Hashtable companyNames;
    public GameObject moneyText;
    AudioSource audioSource;
    public AudioClip cannotBuy;
    public AudioClip bought;
    public int numComp = 0;
    Hashtable companyRequests;
    int timeouts = 10;
    Hashtable companyTimeouts;
    public Hashtable companyTranslation;
    public bool done { get; private set; }
 public List<string> getCompaniesList()
    {
        List<string> list = new List<string>();
        for(int i = 0; i < realCompanies.Count; i++)
        {
            list.Add(realCompanies[i].name);
        }
        return list;
    }
    // Use this for initialization
    void Start () {
        companyTranslation = new Hashtable();
        realCompanies = new List<Company>();
        audioSource = GetComponent<AudioSource>();
        money = startMoney;
        // companies = new List<string>();
        //  companyStocks = new Hashtable();
     stocksInventory = new Hashtable();
           stocksValue = new Hashtable();
        companyRequests = new Hashtable();
        companyTimeouts = new Hashtable();
        /*

          companies.Add("Apple");
              companies.Add("Alphabet");
              companies.Add("Amazon");
              companies.Add("Yahoo");
              companies.Add("Twitter");
              //companies.Add("B");
              AddStockValue("Apple", 143.66f);
              AddStockValue("Alphabet", 63.66f);
              AddStockValue("Amazon", 23.66f);
              AddStockValue("Yahoo", 233.66f);
              AddStockValue("Twitter", 443.66f);
              for (int i = 0; i < companies.Count;i++)
              {
                  stocksInventory.Add(companies[i], 0);
              }
              */
        companies = new List<Company>();
        companyNames = new Hashtable();
        //have to change absolute filepath
        
    
    
      //  CalculateStockValue();
    }
    public void StartMoney()
    {
        money = startMoney;
    }
    public void CalculateStockValue()
    {
        foreach(Company com in realCompanies){
            List<double> list = new List<double>();
            for(int i = 0; i< 4; i++)
            {
                
              //  Debug.Log("Length:" + com.allAbsChanges.Count);
                double cost = (com.allAbsChanges[i * 3 + 0] + com.allAbsChanges[i * 3 + 1] + com.allAbsChanges[i * 3 + 2]) / 3.0;
                list.Add(cost);
            }
            if (!stocksValue.Contains(com))
            {
                stocksValue.Add(com, list);
            }
           
        }
        
    }
    private string companyURL(string name)
    {
        string company = name;
        string urlString = "https://www.blackrock.com/tools/hackathon/portfolio-analysis?calculateExposures=true&calculatePerformance=true&";

        //use company name as identifier/query. for now, only accept tickers--translate from company to 
        //ticker later

        //      endDate=20160101&positions=AAPL~100&startDate=20150101"
        string append = "endDate=20161231&positions=" + company + "~100&returnsType=MONTHLY&startDate=20160201";

        //whole url
        urlString += append;

        return urlString;
    }
    IEnumerator WaitForRequest(WWW w,  Company company)
    {
        yield return w;
        Debug.Log(company.name);
        // Print the error to the console
        if (w.error != null)
        {

        }
        else
        {
       ///     UnityEngine.Debug.Log("request success");
        //    UnityEngine.Debug.Log("returned data" + w.text);
       //     UnityEngine.Debug.Log("request error: " + w.text);
            JSONNode json;
            string text = w.text;
            if (!text.Contains("<"))
            {
                numComp++;
                if (!realCompanies.Contains(company))
                {
                    realCompanies.Add(company);
                }
               
                //   done = true;
                companyRequests[company] = true;
                //use simple json to load the values
                json = SimpleJSON.JSON.Parse(w.text);

                var upstr = json["resultMap"]["PORTFOLIOS"][0]["portfolios"][0]["returns"]["returnsMap"];
                var len = upstr.Count;
                //      Debug.Log ("len: " + len);

                for (int i = 0; i < 12; i++)
                {

                    //          Debug.Log ("huh?" + i);
                    if (i < len - 1)
                    {
                        var percentDown = json["resultMap"]["PORTFOLIOS"][0]["portfolios"][0]["returns"]["returnsMap"][i]["oneMonth"];


                        //          var len = Object.keys json ["resultMap"] ["PORTFOLIOS"] [0] ["portfolios"] [0] ["returns"] ["returnsMap"].Length;

                        //set values of the percentChanges array in said object
                        //              Debug.Log("percent down @: " + i + " " + percentDown);

                        //          if (percentDown != null) {
                        //              company.percentChanges.SetValue (percentDown.AsDouble, i);
                        company.percentChanges.Add(percentDown.AsDouble);
                    }
                    else {
                        //              company.percentChanges.SetValue(0, i);
                        company.percentChanges.Add(0);
                    }
                }

                Company c = company;
                stockChanges(c);

                c.avg = c.allAbsChanges.Average();
                c.max = c.allAbsChanges.Max();
                c.min = c.allAbsChanges.Min();
                c.incrementer = (c.max - c.min) / 5;
             //   UnityEngine.Debug.Log(c.name);

            }
            else
            {
                int temp = (int)companyTimeouts[company];
                temp++;
                companyTimeouts[company] = temp;
                bool flag = (Boolean)companyRequests[company];
                if (!flag && temp < timeouts)
                {
                    StartCoroutine(WaitForRequest(w, company));
                }
               
            }

        }
    }
    void Update()
    {
        if(numComp < 5)
        {
            using (var streamReader = new StreamReader("Assets/Companies.json"))
            {

                var content = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(content);
                var fileJson = JSON.Parse(content);
                //
                //randomly choose out of all the companies
                System.Random rand = new System.Random();
                System.Diagnostics.Debug.WriteLine("blah2");

                //          int[] companyIndexes = new int[5];

                    int starting = rand.Next(0, 15);

                    string name = fileJson["Companies"][starting]["name"];
                    string ticker = fileJson["Companies"][starting]["ticker"];
                    System.Diagnostics.Debug.WriteLine(name);
                    System.Diagnostics.Debug.WriteLine(ticker);
                    Company c = new Company(name, ticker, GetStockPrice(ticker));
                    if (!companyRequests.Contains(c))
                    {
                        companyRequests.Add(c, false);
                    }
                    if (!companyTimeouts.Contains(c))
                    {
                        companyTimeouts.Add(c, 0);
                    }

                    companies.Add(c);
                    if (!companyNames.Contains(ticker))
                    {
                        companyNames.Add(ticker, c);
                        //Debug.Log(i);
                        percentChange(c);

                    }
                if (!companyTranslation.Contains(c.name))
                {
                    companyTranslation.Add(c.name, c);
                }


                


            }
        }
        
    }
    void percentChange(Company company)
    {
        string url = companyURL(company.ticker);
        string jsonString = "{username:\"********\",password:\"********\"}";
        byte[] pData = Encoding.ASCII.GetBytes(jsonString.ToCharArray());
        var encoding = new System.Text.UTF8Encoding();
        var postHeader = new Dictionary<string, string>();
        
        postHeader.Add("Accept", "application/json, text/plain, */*");
        postHeader.Add("Content-Type", "application/json");
        //var webRequest = (HttpWebRequest)WebRequest.Create(url);
        //  var response = webRequest.GetResponse();

        //  var stream = response.GetResponseStream();
        //  var sr = new StreamReader(stream);
        //  var content = sr.ReadToEnd();


        //      double percentDown = 0;
        //      JSONArray items = (JSONArray)json["resultMap"] ["PORTFOLIOS"] [0] ["portfolios"] [0] ["returns"]["returnsMap"];
        //      var len = items.Count;
        //      Debug.Log ("length: " + len);

        //for loop through all the json results to add them to the company's 
        //      var str = json ["resultMap"] ["PORTFOLIOS"] [0] ["portfolios"] [0] ["returns"];
        //      var token = str.Value<JSONArray>()
        //      var dataLen = str.Value<JSONArray> ("returnsMap");

        WWW w = new WWW(url, pData, postHeader);

        StartCoroutine(WaitForRequest(w, company));

    }

    void stockChanges(Company company)
    {

        //initial stock value
        double prevVal = company.initStockVal;

        //      Debug.Log ("length: " + company.percentChanges.Length);

        //loop through months and add relevant values to arraylist
        for (int k = 0; k < 12; k++)
        {
            //          prevVal = json ["resultMap"] ["PORTFOLIOS"] [0] ["portfolios"] [0] ["returns"] ["returnsMap"][i]["level"].AsFloat;
            var update = prevVal + (company.percentChanges[k] * prevVal);

            //changes from month to month
            //          company.allAbsChanges.SetValue(update, k);
            company.allAbsChanges.Add(update);
          //  Debug.Log("Add: " + update);
            //quarterly changes---indexes 2, 5, 8, 11
            //          if ((k+1) %3 == 0) {
            //              Debug.Log ("inside");
            //              company.quarterChanges.Add (update);
            //          }
            //
        }


    }
    private double GetStockPrice(string company)
    {
        string Name = company;
        const string temp_url = "http://ichart.finance.yahoo.com/table.csv?s=@&a=01&b=02&c=2016&d=01&e=02&f=2016";
        Name = temp_url.Replace("@", Name);

        //
        WebClient client = new WebClient();
        Stream result = client.OpenRead(Name);

        using (StreamReader reader = new StreamReader(result))
        {
            string text = "";
            //this line is needed cause the excel file has a line that needs to be skipped
            //          string headerLine = reader.ReadLine();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                text = line;
            }

            string temp = text.Split(',')[1].TrimStart();
            double stock_price = double.Parse(temp, System.Globalization.CultureInfo.InvariantCulture);
            reader.Close();
            return stock_price;
        }
    }


    public void UpdateMoney()
    {
        moneyText = GameObject.Find("MoneyText");
        float flot = (float)money;
        Debug.Log("fl" + flot);
        Debug.Log("m"+money);
        Debug.Log("s"+startMoney);
        moneyText.GetComponent<TextMesh>().text = "Money: " + flot.ToString("F2");
    }
    public void UpdateStocks()
    {
        for(int i = 0; i < realCompanies.Count; i++)
        {
            if(!stocksInventory.Contains(realCompanies[i].name))
                stocksInventory.Add(realCompanies[i].name, 0);
        }
       
    }
 //   public void AddStockValue(string company, float value)
 //   {
  //      stocksValue.Add(company, value);
  //  }
 //   public void AddCompany(string company)
 //   {
  //      companies.Add(company);
  //  }
    public bool AddToInventory(string company)
    {
        Debug.Log(company);
        Company com = (Company)companyTranslation[company];
        Debug.Log(com.allAbsChanges.Count);
        double cost = (com.allAbsChanges[season*3+0]+com.allAbsChanges[season * 3+1] +com.allAbsChanges[season * 3+2])/3.0;

        if (cost > money)
        {
            audioSource.clip = cannotBuy;
            audioSource.Play();
            return false;
        }
        audioSource.clip = bought;
        audioSource.Play();
        money -= cost;

        //increase stock by one
        int count = (int)stocksInventory[company];
        count++;
        stocksInventory[company] = count;

        UpdateMoney();

        World world = GameObject.Find("World").GetComponent<World>();
        world.AddToGarden(company, count);
        return true;
    }
    public void RemoveFromInventory(string company)
    {
        Company com = (Company)companyTranslation[company];
        //decrease stock by one
        int count = (int)stocksInventory[company];
        count--;
        stocksInventory[company] = count;
        List<double> list = (List <double>) stocksValue[com];
        money += list[season];
        UpdateMoney();
    }
    public void ChangeSeason(int sea)
    {
        season = sea;
    }
}
