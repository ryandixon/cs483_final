using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class BattleSystem : MonoBehaviour {
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Text dialog;
    public BattleHUD playerHUD;

    public Button button;

    public List<Inventory> inventories = new List<Inventory>();

    Character player;
    Character enemy;

    private int tmpDefend = 0;

    //AudioSource battleTheme;
    public AudioClip skeleAttack, heal, defend, special;
    AudioSource audioSource;

    void Start() {
        state = BattleState.Start;
        audioSource = GetComponent<AudioSource>();
        //button = GetComponent<Button>();
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle() {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        player = playerGO.GetComponent<Character>();
        
        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemy = enemyGO.GetComponent<Character>();

        dialog.text = "A " + enemy.characterName + " draws near...";

        playerHUD.SetHUD(player);

        yield return new WaitForSeconds(2f);

        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    void PlayerTurn() {
        //button.IsActive(true);
        button.interactable = true;
        dialog.text = "Choose a command?";
    }

    public void OnAttackButton() {
        if(state != BattleState.PlayerTurn) {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton() {
        if(state != BattleState.PlayerTurn) {
            return;
        }

        StartCoroutine(PlayerHeal());
    }

    public void OnDefendButton() {
        if(state != BattleState.PlayerTurn) {
            return;
        }

        StartCoroutine(PlayerDefend());
    }

    //currently base heal on attack power
    //TODO add spell power and allow it to modify damage of heal and attack spells
    IEnumerator PlayerHeal() {
        player.Heal(player.attackPower);

        playerHUD.SetHP(player.health);
        dialog.text = player.characterName + " has healed for " + player.attackPower;

        yield return new WaitForSeconds(2f);

        state = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerDefend() {
        tmpDefend += 1;
        player.Defend(tmpDefend);
        dialog.text = player.characterName + "'s defense has increased";

        yield return new WaitForSeconds(2f);

        state = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }

    //IEnumerator PlayerCast() {
        //player.CastSpell(player.spells)
    //}

    IEnumerator PlayerAttack() {
        
        bool isDead = enemy.Hurt(player.attackPower);
        dialog.text = player.characterName + " attacked " + enemy.characterName + " for " + (player.attackPower - enemy.defencePower).ToString();
        yield return new WaitForSeconds(2f);

        if(isDead) {
            state = BattleState.Won;
            StartCoroutine(EndBattle());
        } else {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EndBattle() {
        if(state == BattleState.Won) {
            player.defencePower -= tmpDefend;
            if(GameObject.FindGameObjectWithTag("Dragon")) {

                yield return new WaitForSeconds(2f);


                SceneManager.LoadScene("GameWon");
            }
            dialog.text = enemy.characterName + " was defeated!";

            yield return new WaitForSeconds(2f);

            dialog.text = player.characterName + " has earned 5 gold";
            player.AddGold(5);
            yield return new WaitForSeconds(2f);

            dialog.text = player.characterName + " has recived Rusty Knife!";
            inventories.Add(new Inventory() {ItemName = "Rusty Knife", Item = ItemType.Gear});
            foreach(Inventory aInventory in inventories) {
                Debug.Log(aInventory);
            }
            yield return new WaitForSeconds(2f);

            dialog.text = player.characterName + " has earned 2 exp";

            yield return new WaitForSeconds(2f);

            player.EarnEXP(2);
            if (player.currentExp >= 10) {
                player.LevelUp();
                dialog.text = player.characterName + "'s level has increased to lvl: " + player.level;
                yield return new WaitForSeconds(2f);
            }
            //player.playerPosition = PlayerStats.Instance.playerPosition; 
            SceneManager.LoadScene("World");
            Debug.Log(" World Scene Loaded");
        } else if (state == BattleState.Lost){
            dialog.text = player.characterName + " has died...";
            SceneManager.LoadScene("GameOver");
        } else {

        }
    }

    IEnumerator EnemyTurn() {
        //enemy will have a random choice of an action between x & y
        //TODO Dont make choice random, have enemy choose to heal when health is low, etc
        //TODO add more abilities for an enemy to choose from, these arent necessarily player abilites, so I can make up whatever
        //TODO add sound effects for each ability

        //button.IsActive(false);
        button.interactable = false;
        int choice = Random.Range(0,3);
        bool isDead;

        switch(choice) {
            case 0:
                //Attack
                dialog.text = enemy.characterName + " attacks!";
                audioSource.PlayOneShot(skeleAttack, 1f);
                
                yield return new WaitForSeconds(2f);
                
                isDead = player.Hurt(enemy.attackPower);
                dialog.text = enemy.characterName + " attacked " + player.characterName + " for " + enemy.attackPower.ToString();
                playerHUD.SetHP(player.health);
                
                yield return new WaitForSeconds(2f);

                if(isDead) {
                    state = BattleState.Lost;
                    StartCoroutine(EndBattle());
                } else {
                    state = BattleState.PlayerTurn;
                    PlayerTurn();
                }
                break;
            case 1:
                //Cast Spell(s)
                //TODO Add random (or not so random) list of spells for enemy to choose from
                dialog.text = enemy.characterName + " performs necrotic touch!";
                
                yield return new WaitForSeconds(2f);
                
                isDead = player.Hurt(enemy.attackPower + 2);
                dialog.text = enemy.characterName + "'s necrotic touch damaged " + player.characterName + " for " + ((enemy.attackPower + 2) - player.defencePower).ToString();
                playerHUD.SetHP(player.health);
                
                yield return new WaitForSeconds(2f);

                if(isDead) {
                    state = BattleState.Lost;
                    StartCoroutine(EnemyTurn());
                } else {
                    state = BattleState.PlayerTurn;
                    PlayerTurn();
                }
                break;
            case 2:
                //Heal
                audioSource.PlayOneShot(heal, 1f);
                dialog.text = enemy.characterName + " has healed for " + enemy.attackPower.ToString();
                enemy.health += enemy.attackPower; 
                
                yield return new WaitForSeconds(2f);

                state = BattleState.PlayerTurn;
                PlayerTurn();
                break;
            case 3:
                //Defend
                //TODO make defend only last x amount of turns
                dialog.text = enemy.characterName + "'s defence has increased";
                enemy.Defend(1);

                yield return new WaitForSeconds(2f);

                state = BattleState.PlayerTurn;
                PlayerTurn();
                break;
            default:
                //Do nothing
                dialog.text = enemy.characterName + " looks confused";
                yield return new WaitForSeconds(2f);

                state = BattleState.PlayerTurn;
                PlayerTurn();
                break;
        }
    }
}