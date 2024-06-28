using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    public int damage = 35;
    private Rigidbody rb;
    private bool targetHit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (transform.parent != collision.gameObject.transform.parent && transform.parent != collision.gameObject.transform)
        {
            if (collision.gameObject.GetComponent<PlayerController>() != null)
            {
                PlayerController enemy = collision.gameObject.GetComponent<PlayerController>();
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
