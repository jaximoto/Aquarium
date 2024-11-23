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
    }


    void Update()
    {
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
