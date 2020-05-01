using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemies : MonoBehaviour
{
    GameObject[] objs;
    void Awake() {
        objs = GameObject.FindGameObjectsWithTag("Marked");
        for(int i = 0; i < objs.Length; i++) {
            Destroy(objs[i]);
        }
    }
}
