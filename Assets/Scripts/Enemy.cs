﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public void Act() {
        int dieRoll = Random.Range(0, 2);
        Character target = BattleController.Instance.GetRandomPlayer();
        
        switch(dieRoll) {
            case 0:
                Defend(2);
                break;
            case 1:
                Spell spellToCast = GetRandomSpell();
                if(spellToCast.spellType == Spell.SpellType.Heal) {
                    target = BattleController.Instance.GetWeakestEnemy();
                }
                //Not enough mana so regular attack
                if(!CastSpell(spellToCast, target)) {
                    BattleController.Instance.DoAttack(this, target);
                }
                break;
            case 2:
                BattleController.Instance.DoAttack(this, target);
                break;
        }
    }

    public void LootTable() {
        
    }

    Spell GetRandomSpell() {
        return spells[Random.Range(0, spells.Count - 1)];
    }

    //public override void Die() {
        //base.Die();
        //BattleController.Instance.characters[1].Remove(this);
    //}
}
