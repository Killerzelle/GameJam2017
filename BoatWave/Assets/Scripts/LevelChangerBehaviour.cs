﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerBehaviour : MonoBehaviour {

    public string sceneName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoChange() {
        Debug.Log("hallo");
        SceneManager.LoadScene(sceneName);
    }
}
