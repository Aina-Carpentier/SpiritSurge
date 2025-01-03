using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }

    }

    // Update is called once per frame
    void CreateController()
    {
       
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerController"), new Vector3(Random.Range(20, 80), 100, Random.Range(20, 80)), Quaternion.identity);
    }
}
