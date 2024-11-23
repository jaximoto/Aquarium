using UnityEngine;
using System.Collections.Generic;

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


    // Update is called once per frame
    void Update()
    {
        view.RenderTankStats(model.statsDict);
    }
}
}
