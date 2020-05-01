using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBattle : MonoBehaviour
{
    //public Vector3 tempPos;
    //public Vector3 returnPos;
    //public Player player;

    //void Awake() {
        //returnPos = PlayerStats.Instance.playerPosition;
    //}
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        //tempPos = player.transform.position;
        //PlayerStats.Instance.playerPosition = tempPos;
        if(gameObject.tag == "Skeleton") {
            gameObject.tag = "Marked";
            SceneManager.LoadScene("Battle");
        } else if(gameObject.tag == "Dragon") {
            SceneManager.LoadScene("DragonBattle");
        } else if(gameObject.tag == "Slime") {
            SceneManager.LoadScene("SlimeBattle");
        }
    }
}
