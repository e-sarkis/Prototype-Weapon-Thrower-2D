using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxContactDetector : MonoBehaviour 
{
	public List<GameObject> ignore;

	private Collider2D _c2d;

	void Start () 
	{
		_c2d = GetComponent<Collider2D>();

		Collider2D otherC2d;
		foreach (GameObject gObj in ignore)
		{
			otherC2d = gObj.GetComponent<Collider2D>();
			if (otherC2d)
			{
				Physics2D.IgnoreCollision(_c2d, otherC2d);
				Physics2D.IgnoreCollision(otherC2d, _c2d);
			}
		}
	}

}
