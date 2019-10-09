using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public int property; // 1: Scissor 2: Scalpel 3: Syringe 4: Surgical Tape 5: Tweezers
    // Combination:
    // 1+4 [3pts]
    // 1+3+5 [5pts]
    // 1+2+3+4+5 [7pts]

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
