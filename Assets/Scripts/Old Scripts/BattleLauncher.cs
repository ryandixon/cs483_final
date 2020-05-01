using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLauncher : MonoBehaviour
{
    public List<Character> Players {get; set; }
    public List<Character> Enemies {get; set; }
    private Vector3 worldPosition;
    private int worldSceneIndex;

    void Awake() {
        if(FindObjectsOfType<BattleLauncher>().Length > 1) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
        EventController.OnBattleCompleted += ReturnToWorld;
    }
    public void PrepareBattle(List<Character> enemies, List<Character> players, Vector3 position) {
        worldPosition = position;
        worldSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Players = players;
        Enemies = enemies;
        SceneManager.LoadScene("Battle");
        //SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
   
    }

    private void ReturnToWorld() {
        GatewayManager.Instance.SetSpawnPosition(worldPosition);
        SceneManager.LoadScene(worldSceneIndex);
    }

    public void Launch() {
        BattleController.Instance.StartBattle(Players, Enemies);
    }
}
