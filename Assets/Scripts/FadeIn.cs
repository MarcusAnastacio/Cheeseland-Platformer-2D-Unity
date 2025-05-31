using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    public float fadeTime;

    private Image blackScreen;

    // Start is called before the first frame update
    void Start()
    {
        //Makes a reference to image in order to recognize blackScreen as an image.
        blackScreen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //makes the screen fade over a course of time (fadeTime)
        blackScreen.CrossFadeAlpha(0f, fadeTime, false);

        //makes the blackScreen inactive once the fading is complete
        if (blackScreen.color.a == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
