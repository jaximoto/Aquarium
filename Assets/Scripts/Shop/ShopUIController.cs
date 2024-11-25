using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class ShopUIController : MonoBehaviour
{
    public TMP_Text categoryText;
    public int currentIndex = 0;
    List<GameObject> shopCategories;
    public GameObject shopFishPage;
    public GameObject shopConsumablesPage;
    public GameObject shopPassivePage;
    
    public GameObject shopContainer;
    public GameObject topTransform;
    public float scrollSpeed;
    private float bottomBound;
    private float topBound;
    private void Awake()
    {
        bottomBound = topTransform.transform.position.y - .01f;
        topBound = bottomBound + 20;

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
            if (hit.transform.gameObject.name == "ScrollArea")
            {
                if (bottomBound < topTransform.transform.position.y && topTransform.transform.position.y < topBound) 
                {
                    shopContainer.transform.Translate(Input.mouseScrollDelta * scrollSpeed * Time.deltaTime * -1);
                    if (bottomBound > topTransform.transform.position.y)
                    {
                        shopContainer.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
                    }
                    if (topTransform.transform.position.y > topBound)
                    {
                        shopContainer.transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
                    }
                }
                
                
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
