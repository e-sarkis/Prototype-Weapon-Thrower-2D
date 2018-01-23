using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour 
{
	public List<GameObject> ignore;
	public float nonLethalSpeed = 0.4f;
	public Vector2 launchForce;
	private Collider2D _c2d;
	private Rigidbody2D _rb2d;
	private Collider2D _friendlyC2d;
	private bool _isHeld;

	void Start () 
	{
		_c2d = GetComponent<Collider2D>();
		_rb2d = GetComponent<Rigidbody2D>();

		Collider2D otherC2d;
		foreach (GameObject gObj in ignore)
		{
			otherC2d = gObj.GetComponent<Collider2D>();
			if (otherC2d)
			{
				Physics2D.IgnoreCollision(_c2d, otherC2d, true);
				Physics2D.IgnoreCollision(otherC2d, _c2d, true);
			}
		}

		_isHeld = false;
	}

	void Update() 
	{
		Launch();
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Parry")
		{
			Parry otherParry = other.gameObject.GetComponent<Parry>();
			if ((otherParry && otherParry.GetParryActive()) || IsSafe())
			{
				ParryResponse(other.gameObject);
			}
		}	
	}

	void OnTriggerStay2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Parry")
		{
			Parry otherParry = other.gameObject.GetComponent<Parry>();
			if ((otherParry && otherParry.GetParryActive()) || IsSafe())
			{
				ParryResponse(other.gameObject);
			}
		}	
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if (other.gameObject.tag == "Player")
		{
			if (IsSafe())
			{
				ParryResponse(other.gameObject);
			} else if (other.collider == _friendlyC2d)
			{
				ParryResponse(other.gameObject);
			} else
			{
				PlayerController pc = other.gameObject.GetComponentInParent<PlayerController>();
				Debug.Log("DEAD");
				// Temporary Round End
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			
		}	
	}

	void ParryResponse(GameObject other)
	{
		transform.parent = other.gameObject.transform;
		transform.position = transform.parent.transform.position;
		_rb2d.simulated = false;
		Physics2D.IgnoreCollision(_c2d, _friendlyC2d, false);
		Physics2D.IgnoreCollision(_friendlyC2d, _c2d, false);

		Collider2D[] colliders;
		if (other.gameObject.transform.parent != null)
		{
			colliders = other.gameObject.transform.parent.GetComponentsInChildren<Collider2D>();
		} else
		{
			colliders = other.gameObject.GetComponentsInChildren<Collider2D>();
		}

		foreach (Collider2D c in colliders)
		{
			if (c.gameObject.tag == "DamageHitbox")
			{
				_friendlyC2d = c.gameObject.GetComponent<Collider2D>();
				Physics2D.IgnoreCollision(_c2d, _friendlyC2d, true);
				Physics2D.IgnoreCollision(_friendlyC2d, _c2d, true);
			}
		}
		_isHeld = true;
	}

	void Launch()
	{
		if (_isHeld)
		{
			PlayerController pc = _friendlyC2d.GetComponentInParent<PlayerController>();
			if (pc.inputAttack)
			{
				_rb2d.simulated = true;
				SpriteAnimator sprAnimator = pc.GetComponentInChildren<SpriteAnimator>();
				if (pc.axisInputDirectionMovement.y > 0 || pc.axisInputDirectionMovement.y < 0)
				{
					_rb2d.AddForce( new Vector2(0 , launchForce.x * (pc.axisInputDirectionMovement.y)));
				} else
				{
					_rb2d.AddForce( new Vector2(launchForce.x * sprAnimator.GetDirectionFaced(), launchForce.y));
				}
				_isHeld = false;
				transform.parent = null;
			}	
		}
	}

	bool IsSafe()
	{
		return ( Mathf.Abs(_rb2d.velocity.x) <= nonLethalSpeed && Mathf.Abs(_rb2d.velocity.y) <= nonLethalSpeed);
	}
}