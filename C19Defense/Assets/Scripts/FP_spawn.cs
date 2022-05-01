using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.EventSystems;

public class FP_spawn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //
    public GameObject objectToInstantiate;

    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        Instantiate(objectToInstantiate, spawnPoint.position, Quaternion.identity);
        
        
    }
    //Function to handle when the pointer is not pressed
    public void OnPointerUp(PointerEventData eventData)
    {
        //console message
        Debug.Log("OnPointerUp");
    }
}
