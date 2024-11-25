using UnityEngine;
using TMPro;
using static Fish;

using System.Collections.Generic;

namespace Tank
{

public class TankView : MonoBehaviour
{
    public TMP_Text co2Text;
    public TMP_Text tempText;
    public TMP_Text wasteText;
    public TMP_Text phText;
    public TMP_Text algaeText;

    public Dictionary<string, TMP_Text> statsTextDict;
    public Dictionary<string, GameObject> statsTickDict;
    public Dictionary<string, GameObject> statsDialDict;

    public GameObject PHTick;
    public GameObject WasteTick;
    public GameObject AlgaeTick;

    public GameObject PHLeft;
    public GameObject PHRight;

    public GameObject CO2Dial;
    public GameObject TempDial;
    public GameObject CO2DialLeft;
    public GameObject CO2DialRight;
    public GameObject TempDialLeft;
    public GameObject TempDialRight;

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Sprite emptyHunger;
    public Sprite fullHunger;
    public Sprite[] fishStatuses;

    public Transform fishGrid;

    public int maxFish = 10;
    public int currFish = 0;
    public GameObject fishHolder;
    public List<GameObject> fishHolders = new();

    public float offsetVal = 0.5f;

    void Start()
    {
        statsTextDict = new Dictionary<string, TMP_Text>
        {
            {"CO2", co2Text},
            {"Temp", tempText},
            {"Waste", wasteText},
            {"PH", phText},
            {"Algae", algaeText}
        };

        statsTickDict = new Dictionary<string, GameObject>
        {
            {"Waste", WasteTick},
            {"PH", PHTick},
            {"Algae", AlgaeTick}
        };

        statsDialDict = new Dictionary<string, GameObject>
        {
            {"CO2", CO2Dial},
            {"Temp", TempDial}
        };

        /*
        for (int i=0; i<maxFish; i++)
        {
            GameObject newFishHolder = Instantiate(fishHolder, fishGrid);
            fishHolders.Add(newFishHolder);
        }
        */
       
        emptyHeart = Resources.LoadAll<Sprite>("Sprites/HealthBar")[0];
        fullHeart = Resources.LoadAll<Sprite>("Sprites/fullHeart")[0];
        emptyHunger = Resources.LoadAll<Sprite>("Sprites/FoodBar")[0];
        fullHunger = Resources.LoadAll<Sprite>("Sprites/fullFood")[0];

        fishStatuses = Resources.LoadAll<Sprite>("Sprites/FishStatusIcons");

    }


    void Update()
    {
    }


    public void RenderTankUI(Dictionary<string, float> statsValsDict, Dictionary<string, float> statsMaxDict)
    {
        foreach(KeyValuePair<string, float> stat in statsValsDict)
        {
            float pcnt = statsValsDict[stat.Key] / statsMaxDict[stat.Key];

            if (statsTickDict.ContainsKey(stat.Key))
            {
                Vector3 right = PHRight.transform.position; 
                Vector3 left = PHLeft.transform.position; 
                Vector3 oldPos = statsTickDict[stat.Key].transform.position;

                Vector3 newPos = Vector3.Lerp(left, right, pcnt);

                statsTickDict[stat.Key].transform.position = new Vector3(newPos.x, oldPos.y, oldPos.z);
            }
            else if (statsDialDict.ContainsKey(stat.Key))
            {
                Vector3 axis = Vector3.forward;
                float rot = (pcnt * 180) + -90;
                Quaternion targetAngle = Quaternion.Euler(0, 0, -1*rot);
                statsDialDict[stat.Key].transform.rotation = Quaternion.Slerp(statsDialDict[stat.Key].transform.rotation,
                                                                                targetAngle, 2.0f*Time.deltaTime);

            }
        }
    }
        

    public void RenderFishUI(Fish myFish)
    {
        float offset = fishHolders.Count * offsetVal;

        GameObject newFishHolder = Instantiate(fishHolder, fishGrid);

        //Set sprite
        newFishHolder.transform.Find("FishHolder").GetComponent<SpriteRenderer>().sprite = myFish.gameObject.GetComponent<SpriteRenderer>().sprite;
        newFishHolder.transform.Translate(Vector3.down * (offset)); 

        //Set hearts, hunger, happiness
        Transform healthBar = newFishHolder.transform.Find("HealthBarContainer");
        Transform hungerBar = newFishHolder.transform.Find("FoodBarContainer");
        for (int i=0; i<myFish.maxHealth; i++)
        {
            if (i < myFish.health)
            {
                healthBar.GetChild(i).GetComponent<SpriteRenderer>().sprite = fullHeart;
                hungerBar.GetChild(i).GetComponent<SpriteRenderer>().sprite = fullHunger;
            }
            else
            {
                healthBar.GetChild(i).GetComponent<SpriteRenderer>().sprite = emptyHeart;
                hungerBar.GetChild(i).GetComponent<SpriteRenderer>().sprite = emptyHunger;
            }
        }


        newFishHolder.transform.Find("FishStatusIcon").GetComponent<SpriteRenderer>().sprite = fishStatuses[(int)myFish.currentStatus];
            
        

        fishHolders.Add(newFishHolder);
    }


    public void UpdateFishUI(List<Fish> myFish)
    {
        for (int j=0; j<myFish.Count; j++)
        {
            Fish fish = myFish[j];
            GameObject fishHolder = fishHolders[j];

            //Set hearts, hunger, happiness
            Transform healthBar = fishHolder.transform.Find("HealthBarContainer");
            Transform hungerBar = fishHolder.transform.Find("FoodBarContainer");
            for (int i=0; i<fish.maxHealth; i++)
            {
                if (i < fish.health)
                {
                    healthBar.GetChild(i).GetComponent<SpriteRenderer>().sprite = fullHeart;
                }
                else
                {
                    healthBar.GetChild(i).GetComponent<SpriteRenderer>().sprite = emptyHeart;
                }
            }

            for (int i=0; i<10; i++)
            {
                if (i < fish.hunger)
                {
                    hungerBar.GetChild(i).GetComponent<SpriteRenderer>().sprite = fullHunger;
                }
                else
                {
                    hungerBar.GetChild(i).GetComponent<SpriteRenderer>().sprite = emptyHunger;
                }
            }



            fishHolder.transform.Find("FishStatusIcon").GetComponent<SpriteRenderer>().sprite = fishStatuses[(int)fish.currentStatus];
        }
            
    }


    public void RenderAllFish(List<Fish> myFish)
    {
        for (int i=0; i<fishHolders.Count; i++)
        {
            fishHolders[i].SetActive(true);
        }

    }

    public void KillAllFish(List<Fish> myFish)
    {
        List<GameObject> toDestroy = new();
        for (int i=0; i<fishHolders.Count; i++)
        {
            //fishHolders[i].GetComponent<SpriteRenderer>().sprite = null;
            //toDestroy.Add(fishHolders[i]);
            fishHolders[i].SetActive(false);
        }
        /*
        for (int i=0; i<toDestroy.Count; i++)
        {
        }
        */
    }



}

}
