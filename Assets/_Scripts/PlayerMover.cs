using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour 
{
	public float moveSpeed = 1.0f;
	public float jumpForce = 100.0f;

	public event System.Action moveEvent;
	public event System.Action<float> moveDirectionEvent;
	public event System.Action stopEvent;
	

	private PlayerController _playerController;
	private Rigidbody2D _rb2d;
	void Awake()
	{
		_playerController 	= GetComponent<PlayerController>();
		_rb2d 				= GetComponent<Rigidbody2D>();
	}

	void Update () 
	{
		// Movement - Horizontal
		if ( Mathf.Pow(_playerController.axisInputDirectionMovement.x, 2) > 0)
		{
			_rb2d.velocity = new Vector2(moveSpeed * _playerController.axisInputDirectionMovement.x, _rb2d.velocity.y);
			if (moveEvent != null)
			{
				moveDirectionEvent(_playerController.axisInputDirectionMovement.x);
			}
		} else
		{
			_rb2d.velocity = new Vector2(0, _rb2d.velocity.y);
			if (stopEvent != null)
			{
				stopEvent();
			}
		}
		// Movement Jumping
		if (_playerController.inputJumpDown && _playerController.isGrounded)
		{
			_rb2d.AddForce(new Vector2(0, jumpForce));
			if (moveEvent != null)
			{
				moveEvent();
			}
		}
	}
}
