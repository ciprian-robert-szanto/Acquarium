using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StareScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 distanceFromPlayer = this.transform.position - PlayerInput.Instance.transform.position;
    	this.GetComponent<Animator>().SetFloat("XInput", -distanceFromPlayer.normalized.x);
    	this.GetComponent<Animator>().SetFloat("YInput", -distanceFromPlayer.normalized.y);
    }
}
