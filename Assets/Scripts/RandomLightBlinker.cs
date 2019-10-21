using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLightBlinker : MonoBehaviour
{
    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        StartCoroutine(randomPlay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator randomPlay()
    {
        yield return new WaitForSeconds(3f);
        int randomN = Random.Range(0, 10);
        if(randomN >= 5f && !anim.isPlaying)
        {
            anim.Play();
        }
        StartCoroutine(randomPlay());
    }
}
