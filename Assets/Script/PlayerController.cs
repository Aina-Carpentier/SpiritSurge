using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Stats")] 
    public float speed = 5f;
    public float hp = 100f;


    PhotonView PV;
    Rigidbody rb;
    CinemachineFreeLook cfl;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        cfl = GetComponent<CinemachineFreeLook>();

    }
    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(GetComponentInChildren<CinemachineFreeLook>().gameObject);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;


        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));
        transform.Translate(dir * speed * Time.deltaTime);

        if (Input.GetAxis("CheatCode1") > 0)
            hp = 10000f;
        else if (Input.GetAxis("CheatCode1") < 0)
            hp = 100f;

        if (Input.GetAxis("CheatCode2") > 0)
            speed = 15f;
        else if (Input.GetAxis("CheatCode2") < 0)
            speed = 5f;

    }

    public void TakeDamage(int damage)  
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(int damage)
    {
        //if (!PV.IsMine)

        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
