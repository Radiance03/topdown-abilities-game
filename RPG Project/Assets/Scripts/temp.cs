using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class temp : MonoBehaviour
{
    Computer pc1;

    Computer pc2;
    private void Start()
    {
        pc1 = new Computer(5, "pcOne");
        pc2 = new Computer(4, "pcTwo");

        if(pc2.GetPrice() > pc1.GetPrice())
        {
            Debug.Log(pc2.GetPrice());
        }
        else
        {
            Debug.Log(pc1.GetPrice());
        }
    }
}




public class Computer
{
    private int price;
    private string name;

    public int GetPrice()
    {
        return price;
    }
    public void SetPrice(int price)
    {
        this.price = price;
    }
    public string GetName()
    {
        return name;
    }
    public void SetName(string name)
    {
        this.name = name;
    }
    public Computer(int price, string name)
    {
        this.price = price;
        this.name = name;
    }
}
