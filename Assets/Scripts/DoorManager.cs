using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject linked; //This is where the door will bring the person to
    public GameObject player;
    public TMPro.TMP_Text note;
    public GameObject lockup;
    public GameObject lockbut;
    public bool locked;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        note = transform.Find("Text (TMP)").gameObject.GetComponent<TMPro.TMP_Text>();
        lockup = transform.Find("padlockup").gameObject;
        lockbut = transform.Find("padlockbut").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        note.color = new Color(1, 1, 1, 1.3f - Vector3.Distance(gameObject.transform.position, player.transform.position) / 4);
        lockup.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1.3f - Vector3.Distance(gameObject.transform.position, player.transform.position) / 4);
        lockbut.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1.3f - Vector3.Distance(gameObject.transform.position, player.transform.position) / 4);
        if (locked)
        {
            lockup.transform.localPosition = Vector3.Lerp(new Vector3(4.27f, 0.18f, 0.93f), new Vector3(4.27f, 0.18f, 0.6f), Time.deltaTime);
        }
        if (!locked)
        {
            lockup.transform.localPosition = Vector3.Lerp(new Vector3(4.27f, 0.18f, 0.6f), new Vector3(4.27f, 0.18f, 0.93f), Time.deltaTime);
        }
    }

}
