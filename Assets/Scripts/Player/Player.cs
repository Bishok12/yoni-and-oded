using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Movement
    private float MoveSpeed;
    private float SpeedIncPerSec;

    // Gravity
    public float Gravity;
    private float GravityMultiplyer;

    // Jump
    private float Jump;
    private float InitJump;
    private float MaxJump;
    private float jumpKeyDownStartTime;

    // Time
    private float nextSpeedIncTime = 0.0f;
    public float period = 1.0f;

    private CharacterController _cc;
    private Vector3 _velocity;


    public void Init(float initSpeed, float speedIncPerSec, float initJump, float maxJump, float gravityMult) {
        if (initJump > maxJump) {
            throw new System.ArgumentException("initial jump force greater than max jump force");
        }

        MoveSpeed = initSpeed;
        SpeedIncPerSec = speedIncPerSec;

        GravityMultiplyer = gravityMult;

        Jump = initJump;
        InitJump = Jump;
        MaxJump = maxJump;

        GameManager.Instance.OnPlatformEnterEvent += OnPlatformEnter;
    }

    public void DeInit() {
        GameManager.Instance.OnPlatformEnterEvent -= OnPlatformEnter;
    }

    private void OnPlatformEnter(float platformJumpMult) {
        Jump = InitJump;
        if (platformJumpMult <= 0) {
            throw new System.ArgumentException("zero or negative platform jump multiplyer not supported");
        }
        Jump = Mathf.Min(MaxJump, (Jump * platformJumpMult));
    }

    protected virtual void Awake()
	{
		_cc = GetComponent<CharacterController>();
	}

    protected virtual void Update() 
    {
        if (Time.time > nextSpeedIncTime) {
            nextSpeedIncTime = Time.time + period;
            MoveSpeed += SpeedIncPerSec;
        }

		// Movement
		var z = MoveSpeed;
		var move = new Vector3(0, 0, z);

		move = transform.TransformDirection(move);
        _velocity.x = move.x;
        _velocity.z = move.z;

        // Gravity
        if (!_cc.isGrounded)
		{
			var gravity = Vector3.up * Gravity;
            _velocity += gravity;
        }

        // Jump
        if (_cc.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            var jump = Vector3.up * Jump;
            _velocity.y = jump.y;
        }

        _cc.Move(_velocity * Time.deltaTime);
	}
}