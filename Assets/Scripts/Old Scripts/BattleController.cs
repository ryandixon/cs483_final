using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController Instance{ get; set; }

    //Party = 0
    //Enemies = 1
    public Dictionary<int, List<Character>> characters = new Dictionary<int, List<Character>>();
    public int characterTurnIndex;
    public Spell playerSelectedSpell;
    public bool playerIsAttacking;

    [SerializeField]
    //Party 3-5
    //Enemies 0-2
    private BattleSpawnPoint[] spawnPoints;

    public int actTurn;
    [SerializeField]
    private BattleUIController uIController;
    BattleLauncher battleLauncher;

    private void Start() {
        if(Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }

        characters.Add(0, new List<Character>());
        characters.Add(1, new List<Character>());
        FindObjectOfType<BattleLauncher>().Launch();
    }

    public Character GetRandomPlayer() {
        return characters[0][Random.Range(0, characters[0].Count - 1)];
    }

    public Character GetWeakestEnemy() {
        Character weakestEnemy = characters[1][0];
        foreach(Character character in characters[1]) {
            if(character.health < weakestEnemy.health) {
                weakestEnemy = character;
            }
        }
        return weakestEnemy;
    }

    private void NextTurn() {
        actTurn = actTurn == 0 ? 1 : 0;
    }

    private void NextAct() {
        if(characters[0].Count > 0 && characters[1].Count > 0) {
            if(characterTurnIndex < characters[actTurn].Count - 1) {
                characterTurnIndex++;
            } else {
                NextTurn();
                characterTurnIndex = 0;
                Debug.Log("turn: " + actTurn);
            }

            switch(actTurn) {
            case 0: //Party
                uIController.ToggleActionState(true);
                uIController.BuildSpellList(GetCurrentCharacter().spells);
                break;
            case 1: //Enemy
                StartCoroutine(PerformAct());
                uIController.ToggleActionState(false);
                break;
        }
        } else {
            Debug.Log("Battle over");
        }
    }
    //Enemy waits before performing actions
    IEnumerator PerformAct() {
        yield return new WaitForSeconds(.75f);
        if(GetCurrentCharacter().health > 0) {
            GetCurrentCharacter().GetComponent<Enemy>().Act();
        }

        uIController.UpdateCharacterUI();
        yield return new WaitForSeconds(1f);
        NextAct();
    }

    public void SelectCharacter(Character character) {
        if(playerIsAttacking) {
            DoAttack(GetCurrentCharacter(), character);
        } else if (playerSelectedSpell != null) {
            if(GetCurrentCharacter().CastSpell(playerSelectedSpell, character)) {
                uIController.UpdateCharacterUI();
                NextAct();
            } else {
                Debug.LogWarning("Not enough mana");
            }
        }
    }

    public void DoAttack(Character attacker, Character target){
        target.Hurt(attacker.attackPower);
        if(actTurn == 0) {
            NextAct();
        }
    }

    public void StartBattle(List<Character> players, List<Character> enemies) {
        Debug.Log("Start Battle");
        //Spawn party on spawn points 3-5
        for(int i = 0; i < players.Count; i++) {
            characters[0].Add(spawnPoints[i+3].Spawn(players[i]));
        }
        //Spawn enemies on points 0-2
        for(int i = 0; i < enemies.Count; i++) {
            characters[1].Add(spawnPoints[i].Spawn(enemies[i]));
        }
    }

    public Character GetCurrentCharacter() {
        return characters[actTurn][characterTurnIndex];
    }
}
