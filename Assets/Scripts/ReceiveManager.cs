using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveManager : MonoBehaviour
{
    public bool[] collects; // 0: Scissor 1: Scalpel 2: Syringe 3: Surgical Tape 4: Tweezers
    public bool triggerPushed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerPushed)
        {
            if (collects[0] == true && collects[1] == true && collects[2] == true && collects[3] == true && collects[4] == true)
            {
                PlayerController.instance.points += 7;
                PlayerController.instance.score.gameObject.GetComponent<Animation>().Play();
                PlayerController.instance.time += 1f;
            }
            if (collects[0] == true && collects[2] == true && collects[4] == true)
            {
                PlayerController.instance.points += 5;
                PlayerController.instance.score.gameObject.GetComponent<Animation>().Play();
                PlayerController.instance.time += 0.7f;
            }
            if (collects[0] == true && collects[3] == true)
            {
                PlayerController.instance.points += 3;
                PlayerController.instance.score.gameObject.GetComponent<Animation>().Play();
                PlayerController.instance.time += 0.4f;
            }
            for (int i = 0; i < 5; i++)
            {
                collects[i] = false;
            }
            StartCoroutine(TurnOffTrigger());
        }
    }

    IEnumerator TurnOffTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        triggerPushed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickupable"))
        {
            if (other.name.Contains("Scissor"))
            {
                collects[0] = true;
            }

            if (other.name.Contains("Scalpel"))
            {
                collects[1] = true;
            }

            if (other.name.Contains("Syringe"))
            {
                collects[2] = true;
            }

            if (other.name.Contains("Tape"))
            {
                collects[3] = true;
            }

            if (other.name.Contains("Tweezers"))
            {
                collects[4] = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickupable"))
        {
            if (other.name.Contains("Scissor"))
            {
                collects[0] = false;
            }

            if (other.name.Contains("Scalpel"))
            {
                collects[1] = false;
            }

            if (other.name.Contains("Syringe"))
            {
                collects[2] = false;
            }

            if (other.name.Contains("Tape"))
            {
                collects[3] = false;
            }

            if (other.name.Contains("Tweezers"))
            {
                collects[4] = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (triggerPushed && other.CompareTag("Pickupable"))
        {
            Destroy(other.gameObject);
        }
    }

}
