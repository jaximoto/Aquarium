using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PlayerStatsController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerStatsModel model;
    List<TMP_Text> playerResourceTexts;
    public TMP_Text playerMoneyText;
    public TMP_Text playerFishOilText;
    public TMP_Text playerCoralText;
    public TMP_Text playerDayText;

    void Awake()
    {
        this.model = new PlayerStatsModel();
        //ShopController.OnBuyPlayer += model.Buy();
        ShopController.OnBuyPlayer += model.Buy;
        // TODO subscribe the model buy method to the shop OnBuy event
        playerResourceTexts = new List<TMP_Text>()
        {
            playerMoneyText,
            playerFishOilText,
            playerCoralText
        };
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerStatsUI();
    }

    public bool CanBuy(Dictionary<string, int> costs)
    {
        foreach(KeyValuePair<string, int> pair in costs)
        {
            if (this.model.model[pair.Key] < pair.Value)
                return false;
        }
        return true;
    }

    public void UpdatePlayerStatsUI()
    {
        int indexer = 0;
        foreach (var key in this.model.model.Keys) 
        {
            
            string newText = this.model.model[key].ToString();
            playerResourceTexts[indexer].text = newText;
            indexer++;
        }

        playerDayText.text = DayCycle.dayCounter.ToString();
    }

}
