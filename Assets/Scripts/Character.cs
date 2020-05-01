using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
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
    public Vector3 playerPosition;
    //public Player player;

    //for now player will level up every 10 exp
    public void LevelUp() {
            level += 1;
            maxHealth += 1;
            maxMana += 1;
            attackPower += 1;
            defencePower += 1;
            currentExp = 0;
    }

    public void EarnEXP(int amount) {
        currentExp += amount;
    }

    public bool Hurt(int amount) {
        //int damageAmount = Random.Range(0, 1) * (amount - defencePower);
        //health = Mathf.Max(health - damageAmount, 0);
        health -= amount;

        if(health <= 0) {
            return true;
        } else {
            return false;
        }
    }
    public void Heal(int amount) {
        int healAmount = Random.Range(0, 1) * (int)(amount + (maxHealth * .33f));
        health = Mathf.Min(health + healAmount, maxHealth);

        if(health > currentExp) {
            health = maxHealth;
        }
    }

    public void Defend(int amount) {
        defencePower += amount; 
    }

    public bool CastSpell(Spell spell, Character target) {
        //checks to see if char has enough mana to cast spell
        bool isSuccessful = manaPoints >= spell.manaCost;

        if(isSuccessful) {
            Spell spellToCast = Instantiate<Spell>(spell, transform.position, Quaternion.identity);
            manaPoints -= spell.manaCost;
            spellToCast.Cast(target);
        }
        return isSuccessful;
    }

    public void TeleportTo(Vector3 targetPosition) {
        transform.position = targetPosition; 
    }

    public void AddGold(int amount) {
        gold += amount;
    }

    public int GoldAmount() {
        return gold;
    }

    public IEnumerator MoveTo(Vector3 targetPosition, System.Action callback, float delay = 0f) {
        while(targetPosition != new Vector3(transform.position.x, transform.position.y, transform.position.z)) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(delay);
        callback();
    }

    //public virtual void Die() {
        //Destroy(this.gameObject);
        //Debug.LogFormat("{0} has died", characterName);
    //}

    public bool Dead() {
        if(health <=0 ) {
            return true;
        } else {
            return false;
        }
    }
}
