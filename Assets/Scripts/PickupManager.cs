using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public int property; // 0: Scissor 1: Scalpel 2: Syringe 3: Surgical Tape 4: Tweezers
    // Combination:
    // 0+3 [3pts]
    // 0+2+4 [5pts]
    // 0+1+2+3+4 [7pts]

    public TMPro.TMP_Text note;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        note.color = new Color(1, 1, 1, 1.3f - Vector3.Distance(gameObject.transform.position,
                                                                player.transform.position)/4);
    }
}
