 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProManager : MonoBehaviour
{
    //可用于显示拾取物品数量管理等。

    public int applecount=0;
    public int pineapplecount=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool PickupItem(GameObject obj)
    {
        switch (obj.tag)
        {
            case "apple":
            pickupapple();
            return  true;

            case "pineapple":
            pickuppineapple();
            return true;

            default:
                return false;//不可拾取
        }
    }

    private void pickupapple()
    {
        applecount++;
    }

    private void pickuppineapple()
    {
        pineapplecount++;
    }
}
