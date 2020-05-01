using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInventory : MonoBehaviour
{
    public Player player;
    public Text displayName, displayLevel, displayHealth, displayMaxHealth, displayAttack, displayDefense, displayMana, dislpayMaxMana, displayGold;
    
    void Start() {
        displayName.text = "Name: " + player.characterName;
        displayLevel.text = "lvl: " + player.level.ToString();
        displayMaxHealth.text = "Max Health: " + player.maxHealth.ToString();
        displayAttack.text = "Attack Power: " + player.attackPower.ToString();
        displayDefense.text = "Defense: " + player.defencePower.ToString();
        //displayMana.text = player.manaPoints.ToString();
        dislpayMaxMana.text = "Max Mana: " + player.maxMana.ToString();
        displayGold.text = "Gold: " + player.gold.ToString();
        //displayHealth.text = player.health.ToString();
    }
}
