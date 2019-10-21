using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public GameObject[] elements; // 0: Scissor 1: Scalpel 2: Syringe 3: Surgical Tape 4: Tweezers
    public int property;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Contains("Player") && !PlayerController.instance.canPick)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(property == 0 || property == 3)
                Instantiate(elements[property], transform.position + new Vector3(0, 3f), Quaternion.Euler(-90f, 0, -90f));
                if(property == 4)
                Instantiate(elements[property], transform.position + new Vector3(0, 3f), Quaternion.Euler(-180f, -90f, 88.606f));
                if(property == 2)
                Instantiate(elements[property], transform.position + new Vector3(0, 3f), Quaternion.Euler(0, -90f, 0));
                if (property == 1)
                Instantiate(elements[property], transform.position + new Vector3(0, 3f), Quaternion.Euler(0, 365f, 0));
            }
        }
    }
}
