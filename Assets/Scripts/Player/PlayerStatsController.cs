using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

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
        if (model.model["money"] <= 0)
            SceneManager.LoadScene("EndScene");
        UpdatePlayerStatsUI();

        // Check for mouse click
        if (Input.GetMouseButtonDown(0)) // 0 = Left Mouse Button
        {
            // Get the mouse position in world coordinates
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Set the starting position of the ray (ignore the Z value for 2D)
            Vector2 rayStart = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

            // Set the direction for the ray (downward in this case)
            Vector2 rayDirection = Vector2.down;

            // Perform the raycast
            RaycastHit2D hit = Physics2D.Raycast(rayStart, rayDirection);

            if (hit)
            {
                Debug.Log("hit: " + hit.collider.name);
                if (hit.transform.gameObject.TryGetComponent<Fish>(out Fish hitFish))
                {
                    if (hitFish.myBubble != null)
                    {
                        if (hitFish.currentStatus == Fish.Status.unhealthy)
                        {
                            model.ChangeStat("money", Mathf.CeilToInt(hitFish.outMoney * .5f));
                        }
                        else if (hitFish.currentStatus == Fish.Status.healthy)
                        {
                            model.ChangeStat("money", Mathf.CeilToInt(hitFish.outMoney));
                        }
                        else if (hitFish.currentStatus == Fish.Status.plusUltra)
                        {
                            model.ChangeStat("money", Mathf.CeilToInt(hitFish.outMoney));
                            model.ChangeStat("fishOil", Mathf.CeilToInt(hitFish.outFishOil));
                            model.ChangeStat("coral", Mathf.CeilToInt(hitFish.outCoral));
                        }
                        hitFish.DestroyBubble();

                    }




                }

            }
        }
        
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
