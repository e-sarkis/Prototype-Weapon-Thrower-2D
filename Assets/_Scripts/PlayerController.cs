﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	[HideInInspector] public bool inputJump			= false;
	[HideInInspector] public bool inputJumpHeld		= false;
	[HideInInspector] public bool inputParry		= false;
	[HideInInspector] public Vector2 axisInputDirectionMovement;
	[HideInInspector] public Vector2 axisInputDirectionThrow;

	public float parryCooldown;	// Time between Parry use and reset
	[HideInInspector] public float timeSinceParry;

	[HideInInspector] public string joyStr;
	//public GameController.Joystick joy = GameController.Joystick.Joy1;
	//public GameController.PlayerNum playerNum = GameController.PlayerNum.P1;

	void Start()
	{
		joyStr = "Joy1";
		//joyStr = GameController.Instance.GetJoystickInputString(joy);
	}

	void Update()
	{
		timeSinceParry += Time.deltaTime;

		inputJump 	= Input.GetButton(joyStr + "Jump");
		inputParry	= Input.GetButton(joyStr + "Parry");

		axisInputDirectionMovement = new Vector2(Input.GetAxisRaw(joyStr + "LStickHorizontal"), Input.GetAxisRaw(joyStr + "LStickVertical"));
		axisInputDirectionMovement.Normalize();
		axisInputDirectionThrow = new Vector2(Input.GetAxisRaw(joyStr + "RStickHorizontal"), Input.GetAxisRaw(joyStr + "RStickVertical"));
		axisInputDirectionThrow.Normalize();

		//Debug.DrawLine(Vector3.zero, axisInputDirectionMovement);
		//Debug.DrawLine(Vector3.zero, axisInputDirectionThrow);
	}

	public string GetJoyString()
	{
		return joyStr;
		//return GameController.Instance.GetJoystickInputString(joy);
	}
}
