﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	// Inputs
	[HideInInspector] public bool inputJump			= false;
	[HideInInspector] public bool inputJumpDown		= false;
	[HideInInspector] public bool inputJumpReleased	= false;
	[HideInInspector] public bool inputParry		= false;
	[HideInInspector] public Vector2 axisInputDirectionMovement;
	[HideInInspector] public Vector2 axisInputDirectionThrow;
	// Timers & Cooldowns
	public float parryCooldown;	// Time between Parry use and reset
	[HideInInspector] public float timeSinceParry;
	// Joystick Information
	[HideInInspector] public string joyStr;
	//public GameController.Joystick joy = GameController.Joystick.Joy1;
	//public GameController.PlayerNum playerNum = GameController.PlayerNum.P1;
	// Events
	public event System.Action deathEvent;
	// Scene Information
	public bool isGrounded;
	public GameObject groundRayOriginGameObject;
	public float groundRayDist;
	private Vector2 _groundRayOrigin;

	void Start()
	{
		joyStr = "Joy1";
		//joyStr = GameController.Instance.GetJoystickInputString(joy);
	}

	void Update()
	{
		timeSinceParry += Time.deltaTime;

		inputJump 			= Input.GetButton(joyStr + "Jump");
		inputJumpDown 		= Input.GetButtonDown(joyStr + "Jump");
		inputJumpReleased 	= Input.GetButtonUp(joyStr + "Jump");
		inputParry			= Input.GetButton(joyStr + "Parry");

		axisInputDirectionMovement = new Vector2(Input.GetAxisRaw(joyStr + "LStickHorizontal"), Input.GetAxisRaw(joyStr + "LStickVertical"));
		axisInputDirectionMovement.Normalize();
		axisInputDirectionThrow = new Vector2(Input.GetAxisRaw(joyStr + "RStickHorizontal"), Input.GetAxisRaw(joyStr + "RStickVertical"));
		axisInputDirectionThrow.Normalize();

		//Debug.DrawLine(Vector3.zero, axisInputDirectionMovement);
		//Debug.DrawLine(Vector3.zero, axisInputDirectionThrow);

		// Cast Ray to see if we're grounded
		_groundRayOrigin = new Vector2(groundRayOriginGameObject.transform.position.x, groundRayOriginGameObject.transform.position.y);
        Debug.DrawLine(_groundRayOrigin, _groundRayOrigin + (groundRayDist * Vector2.down), Color.red);
		RaycastHit2D ray = Physics2D.Raycast(_groundRayOrigin, Vector2.down, groundRayDist);
		if (ray) //&& ray.collider.gameObject.tag == "Ground")
        {
            isGrounded = true;
		} else
		{
			isGrounded = false;
		}
	}


	/// <summary>
	/// Called when the player is killed
	/// </summary>
	void Die()
	{
		if (deathEvent != null)
		{
			deathEvent();
		}
		Destroy(this);
	}

	/// <summary>
	/// Return the Joystick String Prepend for use in InputAxis
	/// </summary>
	/// <returns>String Joystick Prepend</returns>
	public string GetJoyString()
	{
		return joyStr;
		//return GameController.Instance.GetJoystickInputString(joy);
	}
}
