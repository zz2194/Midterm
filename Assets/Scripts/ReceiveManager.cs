using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveManager : MonoBehaviour
{
    public List<GameObject> collects;
    public int triggerPushed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerPushed == 2)
        {
            //Combo1
            if (collects.Contains(GameObject.Find("Scissor")) && collects.Contains(GameObject.Find("Tape")))
            {
                PlayerController.instance.points += 3;
                collects.Clear();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pickupable"))
        {
            if (other.name.Contains("Scissor"))
            {
                collects.Add(other.gameObject);
            }

            if (other.name.Contains("Tape"))
            {
                collects.Add(other.gameObject);
            }
        }
    }

}
