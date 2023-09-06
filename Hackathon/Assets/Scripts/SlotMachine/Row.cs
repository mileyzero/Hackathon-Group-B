using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Row : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;

    public bool rowStopped;
    public string stoppedslot;
    // Start is called before the first frame update
    void Start()
    {
        rowStopped = true;
        Control.ButtonPressed += StartPressing;
    }

    private void StartPressing()
    {
        stoppedslot = "";
        StartCoroutine("Rotate");
    }

    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;

        for(int i = 0; i <30; i++) //for loop to make the row spin down the y axis
        {
            if(transform.position.y <= -6.25f)
            {
                transform.position = new Vector2(transform.position.x, -2f);
            }

            transform.position = new Vector2 (transform.position.x, transform.position.y - 0.25f);

            yield return new WaitForSeconds(timeInterval);
        }

        randomValue = Random.Range(60, 100);

        switch(randomValue % 3)
        {
            case 1:
                randomValue += 2;
                break;
            case 2:
                randomValue += 1;
                break;
        }

        for( int i = 0; i< randomValue; i++)
        {
            if(transform.position.y <= -6.25f)
                transform.position = new Vector2(transform.position.x, -2f);

            transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);

            if (i > Mathf.RoundToInt(randomValue * 0.5f))
                timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(randomValue * 0.75f))
                timeInterval = 0.1f;
            if (i > Mathf.RoundToInt(randomValue * 1f))
                timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(randomValue * 1.5f))
                timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);

        }

        if(transform.position.y <=-2f) //if else statment to make the row snap into a resouces when the row stop between two resouce
        {
            if(transform.position.y <=-2.75f)
            {
                if(transform.position.y <= -4f)
                {
                    if(transform.position.y <= -5f)
                    {
                        if(transform.position.y <= -6f)
                        {
                            transform.position = new Vector2(transform.position.x,-6.25f);
                            stoppedslot = "Popularity";
                        }
                        else
                        {
                            transform.position = new Vector2(transform.position.x,-5.25f);
                            stoppedslot = "Happy";
                        }
                        
                    }
                    else
                    {
                        transform.position = new Vector2(transform.position.x,-4.25f);
                        stoppedslot = "Money";
                    }
                    
                }
                else
                {
                    transform.position = new Vector2(transform.position.x, -3f);
                    stoppedslot = "7";
                }
                
            }
            else
            {
                transform.position = new Vector2(transform.position.x, -2f);
                stoppedslot = "Popularity";
            }
            
        }

        rowStopped = true;
    }

    private void OnDestroy()
    {
        Control.ButtonPressed -= StartPressing;
    }
}
