using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

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
    public int pickN;

    [Header("UI Stuff")]
    public GameObject hpBar;
    public TMPro.TMP_Text score;
    public GameObject blackPanel;

    [Header("Checkpoints")]
    public GameObject checkpoint;

    [Header("Game Variables")]
    public int points;
    public float time;
    public float maxTime;
    public bool inGame;
    public bool dead;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rgbd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PickUp();
        Movement();
        MoveHpBar();
        score.text = points + "/50 pts";
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
            rgbd.AddRelativeForce(Vector3.up * speed * 90);
        }
    }

    void PickUp()
    {
        if (canPick)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                pickN += 1;
            }
            if (pickN == 1 && !picked)
            {
               // pick.transform.position = gameObject.transform.position + new Vector3(0, 2.3f, 0);
                pick.GetComponent<Rigidbody>().isKinematic = true;
                picked = true;
            }
            if (pickN == 2 && picked)
            {
                pick.GetComponent<Rigidbody>().isKinematic = false;
                picked = false;
            }
            if(pickN >= 3)
            {
                pickN = 1;
            }
        }
        if (picked)
        {
            pickupText = pick.GetComponentInChildren<TMPro.TMP_Text>();
            if(pickupText!= null)
            pickupText.text = "Press 'K' to Release";
            pick.transform.position = gameObject.transform.position + new Vector3(0, 2.3f, 0);
            //pick.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            if(pickupText != null)
            pickupText.text = "Press 'K' to Pick Up";
        }
    }

    void MoveHpBar()
    {
        time = maxTime - Time.time/30f;
        hpBar.GetComponent<RectTransform>().localScale = new Vector3(time, hpBar.GetComponent<RectTransform>().localScale.y);
        if(time <= 0f)
        {
            StartCoroutine(death());
        }
    }

    IEnumerator death()
    {

        dead = true;
        blackPanel.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(5f);
        //Re-Start Everything
        transform.position = checkpoint.transform.position;
        time = 1;
        points = 0;
        dead = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name.Contains("Ground"))
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Checkpoint"))
        {
            checkpoint = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Pickupable") && !picked)
        {
            pick = other.transform.parent.gameObject;
            canPick = true;
        }
        if (other.name.Contains("Door"))
        {
            if (Input.GetKeyDown(KeyCode.Return) && other.GetComponent<DoorManager>().locked == false)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                            other.GetComponent<DoorManager>().linked.transform.position.y,
                                                            other.GetComponent<DoorManager>().linked.transform.position.z);
            }
        }
        if (other.name.Contains("ReceiverButton"))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                print("TriggerPushed");
                GameObject.Find("Receiver").GetComponent<ReceiveManager>().triggerPushed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickupable") && !picked)
        {

            pickupText = null;
            pick = null;
            canPick = false;
        }
    }
}
