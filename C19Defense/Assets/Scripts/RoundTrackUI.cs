using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTrackUI : MonoBehaviour
{

    //reference to text object that displays round number
    public Text RoundToText;

    // Start is called before the first frame update
    void Start()
    {
        //upon starting a game session, initialize the rounds counter to 1
        PlayerAttributes.roundTracker = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //display the current round that the player is on
        RoundToText.text = PlayerAttributes.roundTracker.ToString();
    }
}
