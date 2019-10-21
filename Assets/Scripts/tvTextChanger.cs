using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tvTextChanger : MonoBehaviour
{
    public TextMesh tvText;

    // Start is called before the first frame update
    void Start()
    {
		tvText.text = "No Signal";
        StartCoroutine(changeText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator changeText()
    {
        yield return new WaitForSeconds(1f);
        if (tvText.text == "No Signal")
            tvText.text = "無訊號";
        else if (tvText.text == "無訊號")
            tvText.text = "No Signal";
        StartCoroutine(changeText());
    }
}
