using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    public float GravityMultiplyer = 2.5f;
    public float LowJumpMultiplyer = 2f;
    Rigidbody _rb;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rb.velocity.y < 0) {
            _rb.velocity += Vector3.up * Physics.gravity.y * (GravityMultiplyer - 1) * Time.deltaTime;
        } else if (_rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            _rb.velocity += Vector3.up * Physics.gravity.y * (LowJumpMultiplyer - 1) * Time.deltaTime;
        }
    }
}
