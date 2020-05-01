using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gateway : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "Battle";
    [SerializeField] 
    private Vector3 spawnLocation = new Vector3(1.78f, 0f, -9.73f);
    GatewayManager gatewayManager;
    [SerializeField]
    private List<Character> players, enemies;
    [SerializeField]
    private BattleLauncher launcher;
    //GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

    //private void OnCollisionEnter(Collision other) {
        //if(other.gameObject.tag == "Player") {
            //Debug.Log("You are teleporting");
            //SceneManager.LoadScene(sceneName);
            //GatewayManager.Instance.SetSpawnPosition(spawnLocation);
            //Launch();
        //}
    //}

    private void OnCollisionEnter(Collision other) {
        //if(objs.Length > 1) {
            //Destroy(this.gameObject);
        //}
        //DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(sceneName);
        GatewayManager.Instance.SetSpawnPosition(spawnLocation);
    }

        //public void Launch(){
        //launcher.PrepareBattle(enemies, players);
    //}

}
