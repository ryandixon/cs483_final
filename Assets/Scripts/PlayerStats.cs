using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour 
{
    public static PlayerStats Instance;
    public string characterName;
    public int level;
    public int currentExp;
    public int health;
    public int maxHealth;
    public int attackPower;
    //public int spellPower;
    public int defencePower;
    public int maxMana;
    public int manaPoints;
    public List<Spell> spells;
    public List<Inventory> inventories = new List<Inventory>();
    public ItemType itemType;
    public int gold;
    public PlayerData playerData;
    public Vector3 playerPosition;

    /*private PlayerStats() {
        playerData.position = Vector3.zero;
        playerData.rotation = Quaternion.identity;
    }*/

    public void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy (gameObject);
        }
    }
}
