using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject linked; //This is where the door will bring the person to
    public GameObject player;
    public TMPro.TMP_Text note;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        note = transform.Find("Text (TMP)").gameObject.GetComponent<TMPro.TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        note.color = new Color(1, 1, 1, 1.3f - Vector3.Distance(gameObject.transform.position, player.transform.position) / 4);

    }

}
