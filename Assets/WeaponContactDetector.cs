using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContactDetector : MonoBehaviour 
{


	void OnCollisionEnter2D(Collision2D other) 
	{
		GameObject otherGameObject = other.gameObject;
		if (other.gameObject.tag == "Weapon")
		{
			
		}		
	}
}
