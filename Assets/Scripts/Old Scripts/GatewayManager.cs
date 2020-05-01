﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatewayManager : MonoBehaviour {
    private Vector3 spawnPosition;
    private bool spawnPrepared;
    public static GatewayManager Instance { get; set; }
	// Use this for initialization
	void Start () {
		if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += SceneLoaded;
	}
	
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spawnPrepared)
        {
            MovePosition();
        }
    }

    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        spawnPrepared = true;
        this.spawnPosition = spawnPosition;
    }

    private void MovePosition()
    {
        FindObjectOfType<Player>().TeleportTo(spawnPosition);
        spawnPrepared = false;
    }
}
