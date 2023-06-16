using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAway : MonoBehaviour
{
	//define to get value
	private float _runOrStay;
	//define for let other script get access
	public float runOrStay {get {return _runOrStay;}}
	
    // Update is called once per frame
    private void Update()
    {
		//collider overlap and return the specify result
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0f);
		if(hitColliders.Length > 0)
		{
			//Debug.Log("run");
			_runOrStay = 10;
		}
		else
		{
			//Debug.Log("stay");
			_runOrStay = 0;
		}
		
    }
}
