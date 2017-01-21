﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    
    }


	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        { SceneManager.LoadScene("Level 1"); }
        if (Input.GetKeyDown(KeyCode.U))
        { SceneManager.LoadScene("Level 2"); }
        if (Input.GetKeyDown(KeyCode.R))
        { SceneManager.LoadScene("Level 3"); }
    }
}
