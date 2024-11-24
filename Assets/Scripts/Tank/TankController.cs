using UnityEngine;
using System.Collections.Generic;

using static Item;

namespace Tank
{
public class TankController : MonoBehaviour
{

    public TankModel model;
    public TankView view;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.model = new();
    }


    public void BuyHandler(Item i)
    {
    }


    // Update is called once per frame
    void Update()
    {
        view.RenderTankStats(model.statsDict);
    }
}
}
