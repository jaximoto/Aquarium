using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject shopTab;
    public GameObject fishTab;
    public GameObject tankTab;
    bool shopTabOpen = false, fishTabOpen = false, tankTabOpen = false;
    GameObject activeTab;
    GameObject lastTab;

    public float tabMoveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float shopTabHomeX, fishTabHomeX, tankTabHomeY;

    private void Awake()
    {
        shopTabHomeX = shopTab.transform.position.x;
        fishTabHomeX = fishTab.transform.position.x;
        tankTabHomeY = tankTab.transform.position.y;
    }
    public void OpenShopTab()
    {
        if (shopTabOpen)
        {
            StartCoroutine(MoveCloseShopTab());
        }
        else
        {
            shopTabOpen = true;
            lastTab = activeTab;
            activeTab = shopTab;
            StartCoroutine(MoveOpenShopTab(lastTab));
        }

    }
    public void OpenFishTab()
    {
        if (fishTabOpen)
        {
            
            StartCoroutine(MoveCloseFishTab());
        }
        else
        {
            fishTabOpen = true;
            lastTab = activeTab;
            activeTab = fishTab;
            
            StartCoroutine(MoveOpenFishTab(lastTab));
        } 
            

    }

    public void OpenTankTab()
    {
        if (tankTabOpen)
        {
            StartCoroutine(MoveCloseTankTab());
        }
        else
        {

            tankTabOpen = true;
            lastTab = activeTab;
            activeTab = tankTab;
            StartCoroutine(MoveOpenTankTab(lastTab));
        }
    }


    // Update is called once per frame
    IEnumerator MoveOpenShopTab(GameObject lastTab)
    {
        if (lastTab == fishTab)
        {
            StartCoroutine(MoveCloseFishTab());
        }
        else if (lastTab == tankTab)
        {
            StartCoroutine(MoveCloseTankTab());
        }
        float shopTarget = shopTab.transform.position.x + 8f;
        Debug.Log("shoptab transform = " + shopTab.transform.position.x);
        while (shopTab.transform.position.x < shopTarget) 
        {
            shopTab.transform.Translate(Vector3.left * tabMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator MoveOpenFishTab(GameObject lastTab)
    {
        if (lastTab == shopTab)
        {
            StartCoroutine(MoveCloseShopTab());
        }
        else if (lastTab == tankTab)
        {
            StartCoroutine(MoveCloseTankTab());
        }
        
        float fishTarget = fishTab.transform.position.x - 10f;
        while (fishTab.transform.position.x > fishTarget)
        {
            fishTab.transform.Translate(Vector3.left * tabMoveSpeed * Time.deltaTime);
            yield return null;
        }
        
    }
    IEnumerator MoveOpenTankTab(GameObject lastTab)
    {
        if (lastTab == shopTab)
        {
            StartCoroutine(MoveCloseShopTab());
        }
        else if (lastTab == fishTab)
        {
            StartCoroutine(MoveCloseFishTab());
        }
        float tankTarget = tankTab.transform.position.y + 9.025f;
        while (tankTab.transform.position.y < tankTarget)
        {
            tankTab.transform.Translate(Vector3.up * tabMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MoveCloseShopTab()
    {
        while (shopTab.transform.position.x > shopTabHomeX)
        {
            shopTab.transform.Translate(Vector3.right * tabMoveSpeed * Time.deltaTime);
            yield return null;
        }
        shopTabOpen = false;
    }
    IEnumerator MoveCloseFishTab()
    {
        while (fishTab.transform.position.x < fishTabHomeX)
        {
            fishTab.transform.Translate(Vector3.right * tabMoveSpeed * Time.deltaTime);
            yield return null;
        }
        fishTabOpen = false;
    }
    IEnumerator MoveCloseTankTab()
    {
        while (tankTab.transform.position.y > tankTabHomeY)
        {
            tankTab.transform.Translate(Vector3.down * tabMoveSpeed * Time.deltaTime);
            yield return null;
        }
        tankTabOpen = false;
    }



}
