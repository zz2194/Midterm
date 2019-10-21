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
    public GameObject whitePanel;

    [Header("Checkpoints & Doors")]
    public GameObject checkpoint;
    public GameObject door;

    [Header("Animation")]
    public Animation anim;

    [Header("Game Variables")]
    public int points;
    public float time;
    public bool inGame;
    public bool dead;

    [Header("Audio")]
    public AudioSource deadsound;
    public AudioSource regularsound;



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
        HideGameUI();
        Win();
        Animation();
        score.text = points + "/20 pts";
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
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rgbd.AddRelativeForce(Vector3.forward * speed);
            transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rgbd.AddRelativeForce(Vector3.up * speed * 170);
        }
        else if (grounded != true)
        {
            rgbd.AddRelativeForce(Vector3.down * speed/2);
        }
    }

    void Animation()
    {
        if (rgbd.velocity.magnitude > 0.1f && grounded)
        {
            anim.Play("Walking");
        }
        else if (rgbd.velocity.magnitude < 0.1f)
        {
            anim.Play("Standing");
        }
        else if (grounded == false)
        {
            anim.Play("Falling");
        }
    }

    void PickUp()
    {
        if (canPick)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickN += 1;
            }
            if (pickN == 1 && !picked)
            {
                // pick.transform.position = gameObject.transform.position + new Vector3(0, 2.3f, 0);
                if(pick!= null)
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
            pickupText.text = "Press 'E' to Release";
            pick.transform.position = gameObject.transform.position + new Vector3(0, 2.6f, 0);
            //pick.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            if(pickupText != null)
            pickupText.text = "Press 'E' to Pick Up";
        }
    }

    void MoveHpBar()
    {
        if (dead == false && inGame)
        time -= Time.deltaTime/20f;
        hpBar.GetComponent<RectTransform>().localScale = new Vector3(time, hpBar.GetComponent<RectTransform>().localScale.y);
        if(time < 0f && dead == false)
        {
            StartCoroutine(death());
        }
    }

    void HideGameUI()
    {
        if(inGame == false)
        {
            hpBar.SetActive(false);
            score.gameObject.SetActive(false);
        }
        if(inGame == true)
        {
            hpBar.SetActive(true);
            score.gameObject.SetActive(true);
        }
    }

    void Win()
    {
        if(points >= 20)
        {
            door = GameObject.Find("Door (7)");
            door.GetComponent<DoorManager>().locked = false;
            inGame = false;
            if(GameObject.Find("Startpoint")!= null)
            GameObject.Find("Startpoint").SetActive(false);
        }
    }

    IEnumerator death()
    {

        dead = true;
        blackPanel.GetComponent<Animation>().Play();
        deadsound.Play();
        regularsound.Stop();
        yield return new WaitForSeconds(5f);
        deadsound.Stop();
        pick = null;
        canPick = false;
        time = 1;
        points = 0;
        inGame = false;
        transform.position = checkpoint.transform.position;
        //Unlock door 3
        door = GameObject.Find("Door (3)");
        door.GetComponent<DoorManager>().locked = false;
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
        if (other.name.Equals("Startpoint"))
        {
            inGame = true;
            door = GameObject.Find("Door (3)");
            door.GetComponent<DoorManager>().locked = true;
            other.GetComponent<AudioSource>().Play();
        }
        if (other.name.Equals("Endpoint"))
        {
            whitePanel.GetComponent<Animation>().Play();
        }
        if (other.name.Contains("HangingBottle"))
        {
            other.GetComponent<Animation>().Play();
            other.GetComponent<AudioSource>().Play();
        }
        if (other.name.Contains("Screen"))
        {
            if (!other.GetComponent<ScreenManager>().falled)
            {
                other.GetComponent<Animation>().Play();
                other.GetComponent<AudioSource>().Play();
                other.GetComponent<ScreenManager>().falled = true;
            }
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
                                                            other.GetComponent<DoorManager>().linked.transform.position.y -1f,
                                                            other.GetComponent<DoorManager>().linked.transform.position.z);
                other.GetComponent<AudioSource>().Play();
            }
        }
        if (other.name.Contains("ReceiverButton"))
        {
            if (Input.GetKeyDown(KeyCode.E))
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
