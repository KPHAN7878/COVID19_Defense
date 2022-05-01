using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class DragaNDdROPtOWER : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //Class that is responsible for the drag and drop of the game object

    public GameObject objectToInstantiate;

    public int towerCost;

    private GameObject CurrentObject;


    

    // Start is called before the first frame update so we dont do anything on the start
    void Start()
    {
        
    }
    // Update is called once per frame if any updates happen on the frame
    void Update()
    {
        //if user currency is less than 350 then dont let anything happen
        if (PlayerAttributes.currency < towerCost)
        {
            //Try setting the button color to red to indicate that they cant buy it<-- Implement this on 3rd iteration
            //GetComponent<Button>().color = Color.red;
        }
        //Check whether the given mouse button is held down.
        //Button values are 0 for left button, 1 for right button, 2 for the middle button.
        if (Input.GetMouseButton(0) && CurrentObject)
        {
            //The current mouse position is stored in pixel coordinates. 
            var pos = Input.mousePosition;
            pos.z = -Camera.main.transform.position.z;
            CurrentObject.transform.position = Camera.main.ScreenToWorldPoint(pos);

        }
        //When button is released reset the current object to null
        if(Input.GetMouseButtonUp(0) && CurrentObject)
        {
            CurrentObject = null;
        }
        
    }
    //
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");

        var pos = Input.mousePosition;
        pos.z = -Camera.main.transform.position.z;
        pos = Camera.main.ScreenToWorldPoint(pos);

        //check if the user has enough money to buy tower 1
        //if not then don't spawn tower
        if(PlayerAttributes.currency >= towerCost)
        {
            CurrentObject = Instantiate(objectToInstantiate, pos, Quaternion.identity);
            PlayerAttributes.currency -= towerCost;
            
        }
        
    }
    //Function to handle when the pointer is not pressed
    public void OnPointerUp(PointerEventData eventData)
    {
        //console message
        //Debug.Log("OnPointerUp");
    }

    private bool IsMouseOverButton()
    {
        //Is the pointer with the given ID over an EventSystem object?
        return EventSystem.current.IsPointerOverGameObject();
    }
}
