using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]                                  // Only one Scripts
public class Xfactor : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(5f, 5f, 0f);
    [SerializeField] float period = 2f;                     // the full period of a circle is 2(f) secs;

    float movementFactor;         

    Vector3 startingPos; 

    void Start ()
    {
        startingPos = transform.position;                    // the start of moving object
	}
	
	// VERY IMPORTANT FOR Object MOVEMENT!!!! Read carefully
	void Update ()
    {   if(period <= Mathf.Epsilon) { return; }             //protects against NaN and Epsilon is smallest number could obtain

        float cycles = Time.time / period;                  //divides times the game cycle. smoothes it per frame
        const float tau = Mathf.PI * 2f;         //     *****equals to about 6.28. look up tau for more info*****
        float rawSinWave = Mathf.Sin(cycles * tau);
        


        movementFactor = rawSinWave / 2f + .5f;             // *****Crops movement and speeds or slows down obj******
        Vector3 offset = movementVector * movementFactor;  // displacement. multi scale vectors
        transform.position = startingPos + offset;          // add starting plus the offset.

    }
}
