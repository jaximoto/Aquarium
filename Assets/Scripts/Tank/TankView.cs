using UnityEngine;
using TMPro;

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

                Vector3 newPos = Vector3.Lerp(right, left, pcnt);

                statsTickDict[stat.Key].transform.position = new Vector3(newPos.x, oldPos.y, oldPos.z);
            }
            else if (statsDialDict.ContainsKey(stat.Key))
            {
                Vector3 axis = Vector3.forward;
                float rot = (pcnt * 180) + -90;
                    Debug.Log($"{gameObject.name}: rot = {rot}");
                Quaternion targetAngle = Quaternion.Euler(0, 0, -1*rot);
                statsDialDict[stat.Key].transform.rotation = Quaternion.Slerp(statsDialDict[stat.Key].transform.rotation,
                                                                                targetAngle, 2.0f*Time.deltaTime);

                Debug.Log($"{gameObject.name}: slerp = {statsDialDict[stat.Key].transform.rotation}");

            }
        }
    }
        

    public void RenderTankStats(Dictionary<string, float> statsValsDict)
    {
        //If you want to render an integer, just cast it before rendering it
        //sucks but works
        foreach (KeyValuePair<string, float> stat in statsValsDict)
        {
            statsTextDict[stat.Key].text = $"{stat.Key}: {statsValsDict[stat.Key]}";
        }
    }


}

}
