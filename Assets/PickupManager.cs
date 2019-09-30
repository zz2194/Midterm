using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public TMPro.TMP_Text note;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        note.color = new Color(1, 1, 1, 1.3f - Vector3.Distance(gameObject.transform.position, player.transform.position)/4);
    }
}
