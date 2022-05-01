using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttributes : MonoBehaviour
{
    //Class that has the properties and variable initialization
    //variable to keep track of currency currently hardcoded to 400
    //public static int currency = 400;

    public static int currency;
    int initialMoney;


    //variables to keep track of lives <-- Health functionality will be implemented in 3rd iteration
    public static int health;
    public int initialHealth = 20;

    public static int covidUnitsKilled;

    public static int roundTracker;

    // Start is called before the first frame update
    void Start()
    {
        //set the initial money to currency amount that is hardcoded to 400
        //initialMoney = currency;
    }
    // Update is called once per frame
    void Update()
    {
        //Do nothing here
    }
}
