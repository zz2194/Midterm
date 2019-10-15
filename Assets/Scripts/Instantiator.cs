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
            if (Input.GetKeyDown(KeyCode.K))
            {
                Instantiate(elements[property], transform.position + new Vector3(0, 3f), Quaternion.Euler(-90f, 0, -90f));
            }
        }
    }
}
