using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour 
{
	float moveSpeed = 1.0f;

	private PlayerController _playerController;
	private Rigidbody2D _rb2d;

	void Awake()
	{
		_playerController 	= GetComponent<PlayerController>();
		_rb2d 				= GetComponent<Rigidbody2D>();
	}

	void Update () 
	{
		if ( Mathf.Pow(_playerController.axisInputDirectionMovement.x, 2) > 0)
		{
			_rb2d.velocity = new Vector2(moveSpeed * _playerController.axisInputDirectionMovement.x, _rb2d.velocity.y);
		} else
		{
			_rb2d.velocity = new Vector2(0, _rb2d.velocity.y);
		}
	}
}
