using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class covidKillsUI : MonoBehaviour
{

    //reference to text object that displays round number
    public Text KillsToText;

    // Start is called before the first frame update
    void Start()
    {
        //upon starting a game session, initialize the covid kill count to 0
        PlayerAttributes.covidUnitsKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //display the current round that the player is on
        KillsToText.text = PlayerAttributes.covidUnitsKilled.ToString();
    }
}
