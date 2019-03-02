using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hareket : MonoBehaviour {
    public GameObject ok;
    void Start () {
		
	}
	       
	
	void Update () {
                  
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);    
      
              
    }
   
}
