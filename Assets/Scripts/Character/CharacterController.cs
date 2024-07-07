using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterController : MonoBehaviour
{
    /*private Rigidbody2D rb;*/
    [SerializeField] private float _characterSpeed;
    PhotonView _photonView;

    void Start()
    {
/*        rb = GetComponent<Rigidbody2D>();*/
        _photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (_photonView.IsMine)
        {
            /*float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(horizontalInput, verticalInput) * _characterSpeed;
            rb.velocity = movement;*/
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * _characterSpeed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;
        }
    }
}
