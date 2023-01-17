//Attach this script to a GameObject
//Create an Image GameObject by going to Create>UI>Image. Attach this Image to the Image field in your GameObject’s Inspector window.
//This script creates a toggle that fades an Image in and out.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeImage : MonoBehaviour
{
    //Attach an Image you want to fade in the GameObject's Inspector
    public Image m_Image;
    //Use this to tell if the toggle returns true or false
    bool m_Fading;


    void Start()
    {
        var tempColor = m_Image.color;
        tempColor.a = 0f;
        m_Image.color = tempColor;
        StartCoroutine(fadeIn());

    }

    public IEnumerator fadeIn()
    {
        // retardo de aparicion del bocadillo
        // para el mensaje de texto
        //yield return new WaitForSeconds(1f);
        for(float i = 0; i < 1; i+=0.02f)
        {
            // velocidad de aparicion del bocadillo
            // el fade in
            yield return new WaitForSeconds(0.03f);
            var tempColor2 = m_Image.color;
            tempColor2.a += i;
            m_Image.color = tempColor2;


        }

        // delay when cg logo is on screen
        yield return new WaitForSeconds(1f);

        // start fading out
        StartCoroutine(fadeOut());
    }

    public IEnumerator fadeOut()
    {
    
        for(float i = 1; i > 0; i-=0.02f)
        {
            yield return new WaitForSeconds(0.03f);
            var tempColor2 = m_Image.color;
            tempColor2.a -= i;
            m_Image.color = tempColor2;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
