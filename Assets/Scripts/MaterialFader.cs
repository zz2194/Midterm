using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFader : MonoBehaviour
{
    public Material mtr;
    public GameObject player;
    public Light lig;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (mtr.GetFloat("_Metallic") <= 1)
        {
            mtr.SetFloat("_Metallic", Vector3.Distance(gameObject.transform.position,
                                                       player.transform.position) / 4);
            if (mtr.GetFloat("_Metallic") > 1)
            {
                mtr.SetFloat("_Metallic", 1);
            }
        }
        
        if(lig.intensity <= 1)
        {
            lig.intensity = Vector3.Distance(gameObject.transform.position,
                                                   player.transform.position) / 4;
            if (lig.intensity > 1)
            {
                lig.intensity = 1;
            }
        }
    }
}
