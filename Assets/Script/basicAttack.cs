using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicAttack : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public float throwCooldown;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow = true;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && readyToThrow)
            Throw();
    }

    void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);
        projectile.transform.SetParent(transform);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }


        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
