using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    public Text healthToText;

    // Start is called before the first frame update
    void Start()
    {
        //set the Health at the start up of a map
        PlayerAttributes.health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        //display the amount of money that the player currently has
        healthToText.text = PlayerAttributes.health.ToString();
    }
}
