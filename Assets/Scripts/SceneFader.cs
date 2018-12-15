﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

    public Image img;
    public AnimationCurve curve;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(FadeIn());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while(t > 0f)
        {
            t -= Time.deltaTime;
            float alpha = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;             // skip to the next frame
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float alpha = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;             // skip to the next frame
        }

        SceneManager.LoadScene(scene);
    }
}
