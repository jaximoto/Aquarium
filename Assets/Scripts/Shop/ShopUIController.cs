using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ShopUIController : MonoBehaviour
{
    public TMP_Text categoryText;
    public int currentIndex = 0;
    List<GameObject> shopCategories;
    public GameObject shopFishPage;
    public GameObject shopConsumablesPage;
    public GameObject shopPassivePage;
    
    public GameObject shopContainer;
    public float scrollSpeed;
    private void Awake()
    {
        shopCategories = new List<GameObject>()
        {
            shopFishPage, shopConsumablesPage, shopPassivePage
        };
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.gameObject.name == "ScrollZone" && Input.mouseScrollDelta != Vector2.zero)
            {
                transform.Translate(Input.mouseScrollDelta * scrollSpeed * Time.deltaTime);
            }
        }
    }

    public void MoveLeft()
    {
        shopCategories[currentIndex].SetActive(false);
        int newIdx = --currentIndex;

        if (newIdx < 0)
        {
            currentIndex = shopCategories.Count - 1;
        }

        else
        {
            currentIndex = newIdx;
        }
        shopCategories[currentIndex].SetActive(true);
        categoryText.text = shopCategories[currentIndex].name;
    }
     public void MoveRight()
    {
        shopCategories[currentIndex].SetActive(false);
        int newIdx = ++currentIndex;

        if (newIdx >= shopCategories.Count)
        {
            currentIndex = 0;
        }

        else
        {
            currentIndex = newIdx;
        }
        shopCategories[currentIndex].SetActive(true); 
        categoryText.text = shopCategories[currentIndex].name;
    }

}
