using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosition : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Awake()
    {
        player.playerPosition = PlayerStats.Instance.playerPosition;
    }

    // Update is called once per frame
   /* void Update()
    {
        PlayerLeaves();
        PlayerReturns();
    }

    public void PlayerLeaves() {
        PlayerPrefs.SetFloat("X", player.transform.position.x);
        PlayerPrefs.SetFloat("Y", player.transform.position.y);
        PlayerPrefs.SetFloat("Z", player.transform.position.z);
    }

    public void PlayerReturns() {
        float x = player.transform.position.x;
        float y = player.transform.position.x;
        float z = player.transform.position.x;
        x = PlayerPrefs.GetFloat("X");
        y = PlayerPrefs.GetFloat("Y");
        z = PlayerPrefs.GetFloat("Z");
    }*/
}
