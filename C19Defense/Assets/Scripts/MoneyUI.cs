using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    //Class to take the current currency and display on the front end.
    public Text currencyToText;
    
    // Update is called once per frame

    void Start()
    {
        //set the Health at the start up of a map
        PlayerAttributes.currency = 400;
    }

    void Update()
    {
        //display the amount of money that the player currently has
        currencyToText.text = PlayerAttributes.currency.ToString();
        
    }
}
