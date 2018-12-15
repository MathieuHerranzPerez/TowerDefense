using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Money;
    public static int Lives;
    public static int StartLives;
    public static int Rounds;

    public int startMoney = 400;
    public int startLives = 20;



	// Use this for initialization
	void Start ()
    {
        Money = startMoney;
        StartLives = startLives;
        Lives = StartLives;

        Rounds = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
