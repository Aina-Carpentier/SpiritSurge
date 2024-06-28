using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScriptController : MonoBehaviour
{
    Animator animator;
    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        //Lance les animations 
        if (Input.GetAxis("Vertical") > 0)
            animator.SetBool("isWalkingForward", true);

        else if (Input.GetAxis("Vertical") < 0)
            animator.SetBool("isWalkingBackward", true);

        else if(Input.GetAxis("Horizontal") > 0)
            animator.SetBool("isWalkingRight", true);

        else if(Input.GetAxis("Horizontal") < 0)
            animator.SetBool("isWalkingLeft", true);


        // Stop les animations
        if (!(Input.GetAxis("Vertical") > 0))
            animator.SetBool("isWalkingForward", false);

        if (!(Input.GetAxis("Vertical") < 0))
            animator.SetBool("isWalkingBackward", false);

        if (!(Input.GetAxis("Horizontal") > 0))
            animator.SetBool("isWalkingRight", false);

        if (!(Input.GetAxis("Horizontal") < 0))
            animator.SetBool("isWalkingLeft", false);
    }
}
