using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]


//Wave class that contains metadata for each wave
//variables are statically set in the unity inspector
public class Wave
{
    public string waveName;
    //public int numberOfEnemies;

    //array that holds different types of covid variants
    public GameObject[] enemyType;

    //controls how fast covid units spawn
    public float spawnInterval;


    //variable that defines different types of enemies in a round
    public int numberOfEnemy1;
    public int numberOfEnemy2;
    public int numberOfEnemy3;
    public int numberOfEnemy4;
    public int numberOfEnemy5;
    public int numberOfEnemy6;



    //variable that defines the base amount of money to gain after completing each round
    public int roundMoney;

}


//wave spawner implementation
public class Spawner : MonoBehaviour
{

    //reference to UI button "start round"
    private Button _button;

    //boolean to detect if button is clicked yet
    private bool buttonClicked = false;

    public Wave[] waves;
    

    public Transform[] spawnPoint;




    //keep track of which wave you are in
    private Wave currentWave;
    private int currentWaveIndex;
    private float nextSpawnTime;

    //boolean to allow spawning of covid
    private bool canSpawn = true;


    private void Start()
    {
        //REFERENCE BUTTON UI
        _button = GameObject.Find("StartButton").GetComponent<Button>();
    }



    private void Update()
    {
        //find all covid assets on the map
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Character");


        //when "start button" is clicked, make it non interactable during the round
        if (buttonClicked == true)
        {
            _button.interactable = false;
        }
        else
        {
            //when there are no more covid assets on the map, map button pressable again
            if (totalEnemies.Length == 0)
            {
                _button.interactable = true;
                _button.onClick.AddListener(delegate{ParameterOnClick(ref buttonClicked);});

            }
            
        }
 
        
        //keep track of waves
        currentWave = waves[currentWaveIndex];



        //CALL SPAWN WAVE FUNCTION
        SpawnWave();


        //THIS IS FOR IN BETWEEN WAVES CONTROL
        //DETERMINES IF PLAYER CAN CLICK "START ROUND" BUTTON
        //1. no covid assets remaining on map
        //2. "canspawn" boolean is false
        //3. the next wave index does not exceed the max wave index
        if(totalEnemies.Length == 0 && !canSpawn && currentWaveIndex + 1 != waves.Length)
        {

            //when the user clicks the "start round" button, start a new round and set "canspawn" boolean to true
            if(buttonClicked == true)
            {
                currentWaveIndex++;
                canSpawn = true;

                //increment currency by a flat amount after finishing each round
                PlayerAttributes.currency += currentWave.roundMoney;

                PlayerAttributes.roundTracker += 1;
            }
            
        }
    }


    //spawn covid assets
    void SpawnWave()
    {
        if (canSpawn && buttonClicked && nextSpawnTime < Time.time)
        {
            
            //these if statements determine the amount of each covid variant to spawn in each round
            //quantity for each variant is set inside the unity inspector
            if(currentWave.numberOfEnemy1 > 0)
            {
                GameObject randomEnemy = currentWave.enemyType[0];
                Transform spawnLocation = spawnPoint[Random.Range(0, spawnPoint.Length)];
                Instantiate(randomEnemy, spawnLocation.position, Quaternion.identity);
                currentWave.numberOfEnemy1--;
                nextSpawnTime = Time.time + currentWave.spawnInterval;
            }
            if(currentWave.numberOfEnemy1 == 0 && currentWave.numberOfEnemy2 > 0)
            {
                GameObject randomEnemy = currentWave.enemyType[1];
                Transform spawnLocation = spawnPoint[Random.Range(0, spawnPoint.Length)];
                Instantiate(randomEnemy, spawnLocation.position, Quaternion.identity);
                currentWave.numberOfEnemy2--;
                nextSpawnTime = Time.time + currentWave.spawnInterval;
            }
            if(currentWave.numberOfEnemy1 == 0 && currentWave.numberOfEnemy2 == 0 && currentWave.numberOfEnemy3 > 0)
            {
                GameObject randomEnemy = currentWave.enemyType[2];
                Transform spawnLocation = spawnPoint[Random.Range(0, spawnPoint.Length)];
                Instantiate(randomEnemy, spawnLocation.position, Quaternion.identity);
                currentWave.numberOfEnemy3--;
                nextSpawnTime = Time.time + currentWave.spawnInterval;
            }
            if(currentWave.numberOfEnemy1 == 0 && currentWave.numberOfEnemy2 == 0 && currentWave.numberOfEnemy3 == 0 && currentWave.numberOfEnemy4 > 0)
            {
                GameObject randomEnemy = currentWave.enemyType[3];
                Transform spawnLocation = spawnPoint[Random.Range(0, spawnPoint.Length)];
                Instantiate(randomEnemy, spawnLocation.position, Quaternion.identity);
                currentWave.numberOfEnemy4--;
                nextSpawnTime = Time.time + currentWave.spawnInterval;
            }
            if(currentWave.numberOfEnemy1 == 0 && currentWave.numberOfEnemy2 == 0 && currentWave.numberOfEnemy3 == 0 && currentWave.numberOfEnemy4 == 0 && currentWave.numberOfEnemy5 > 0)
            {
                GameObject randomEnemy = currentWave.enemyType[4];
                Transform spawnLocation = spawnPoint[Random.Range(0, spawnPoint.Length)];
                Instantiate(randomEnemy, spawnLocation.position, Quaternion.identity);
                currentWave.numberOfEnemy5--;
                nextSpawnTime = Time.time + currentWave.spawnInterval;
            }
            if(currentWave.numberOfEnemy1 == 0 && currentWave.numberOfEnemy2 == 0 && currentWave.numberOfEnemy3 == 0 && currentWave.numberOfEnemy4 == 0 && currentWave.numberOfEnemy5 == 0 && currentWave.numberOfEnemy6 > 0)
            {
                GameObject randomEnemy = currentWave.enemyType[5];
                Transform spawnLocation = spawnPoint[Random.Range(0, spawnPoint.Length)];
                Instantiate(randomEnemy, spawnLocation.position, Quaternion.identity);
                currentWave.numberOfEnemy6--;
                nextSpawnTime = Time.time + currentWave.spawnInterval;
            }


            //where there are no covid assets left on the map, set "canspawn" and "buttonclicked" booleans to false to allow user to click "start round" button
            if(currentWave.numberOfEnemy1 == 0 && currentWave.numberOfEnemy2 == 0 && currentWave.numberOfEnemy3 == 0 && currentWave.numberOfEnemy4 == 0 && currentWave.numberOfEnemy5 == 0 && currentWave.numberOfEnemy6 == 0)
            {
                canSpawn = false;
                buttonClicked = false;
                if (PlayerAttributes.roundTracker == 50)
                {
                    PlayerAttributes.roundTracker++;
                }

            }
        }
        
    }

    
    private void ParameterOnClick(ref bool buttonClicked)
    {
        buttonClicked = true;
    }
    
    
    
}





















//TESTING STUFF


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
[System.Serializable]

public class Wave
{
    public string waveName;
    public int numberOfEnemies;
    public GameObject[] enemyType;
    public float spawnInterval;
}

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    //public Transform spawnpoints;

    public Transform[] spawnPoint;

    //testing text
    //DELETE LATER
    // public Text testText;


    //keep track of which wave you are in
    private Wave currentWave;
    private int currentWaveIndex;
    private float nextSpawnTime;

    //boolean to allow spawning of covid
    private bool canSpawn = true;

    private void Update()
    {
        //keep track of waves
        currentWave = waves[currentWaveIndex];

        //call spawn wave function
        SpawnWave();
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            //test random spawn
            GameObject randomEnemy = currentWave.enemyType[Random.Range(0, currentWave.enemyType.Length)];
            Transform spawnLocation = spawnPoint[Random.Range(0, spawnPoint.Length)];
            Instantiate(randomEnemy, spawnLocation.position, Quaternion.identity);

            //DELETE LATER
            // testText.text = currentWave.numberOfEnemies.ToString();

            //spawn proper number of enemies
            currentWave.numberOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if(currentWave.numberOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
        



    }
    
    
}

*/