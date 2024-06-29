using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class troisiemePersonCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        player.forward = viewDir.normalized;
        
        Vector3 inputDir = player.forward * Mathf.Abs(verticalInput) + player.forward * Mathf.Abs(horizontalInput);

    
        player.forward = Vector3.Slerp(player.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        
    }
}
