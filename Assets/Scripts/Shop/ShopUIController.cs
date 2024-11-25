using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ShopUIController : MonoBehaviour
{
    public TMP_Text categoryText;
    public int currentIndex = 0;
    public List<string> shopCategories;

    private void Awake()
    {
        shopCategories = new List<string>();
    }

    public void MoveLeft()
    {
        int newIdx = --currentIndex;

        if (newIdx < 0)
        {
            currentIndex = shopCategories.Count - 1;
        }

        else
        {
            currentIndex = newIdx;
        }
        
        categoryText.text = shopCategories[currentIndex];
    }
     public void MoveRight()
    {
        int newIdx = ++currentIndex;

        if (newIdx >= shopCategories.Count)
        {
            currentIndex = 0;
        }

        else
        {
            currentIndex = newIdx;
        }
        
        categoryText.text = shopCategories[currentIndex];
    }

}
