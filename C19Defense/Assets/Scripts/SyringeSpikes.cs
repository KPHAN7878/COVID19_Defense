using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeSpikes : MonoBehaviour
{

    //test comment for sync
    public float spikeHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            spikeHealth -= 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spikeHealth == 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
