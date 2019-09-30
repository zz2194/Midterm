using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float speed;
    public float maxspeed;
    public bool grounded;
    private Rigidbody rgbd;

    [Header("Pickup")]
    public GameObject pick;
    public bool canPick;
    public bool picked;
    public TMPro.TMP_Text pickupText;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PickUp();
        Movement();
    }

    void Movement()
    {
        if(rgbd.velocity.magnitude >= maxspeed)
        {
            rgbd.velocity = rgbd.velocity.normalized * maxspeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rgbd.AddRelativeForce(Vector3.forward * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rgbd.AddRelativeForce(Vector3.back * speed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rgbd.AddRelativeForce(Vector3.up * speed * 30);
        }
    }

    void PickUp()
    {
        if (canPick)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
               // pick.transform.position = gameObject.transform.position + new Vector3(0, 2.3f, 0);
                pick.GetComponent<Rigidbody>().isKinematic = true;
                picked = true;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                pick.GetComponent<Rigidbody>().isKinematic = false;
                picked = false;
            }
        }
        if (picked)
        {
            pickupText = pick.GetComponentInChildren<TMPro.TMP_Text>();
            if(pickupText!= null)
            pickupText.text = "Press 'K' to Release";
            pick.transform.position = gameObject.transform.position + new Vector3(0, 2.3f, 0);
            pick.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            if(pickupText != null)
            pickupText.text = "Press 'J' to Pick Up";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            grounded = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Pickupable")
        {
            pick = other.transform.parent.gameObject;
            canPick = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pickupable")
        {
            pickupText = null;
            pick = null;
            canPick = false;
        }
    }
}
