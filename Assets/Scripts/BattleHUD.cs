using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText, hpText, mpText;
    public Slider hpSlider, mpSlider, expSlider;

    public void SetHUD(Character character) {
        nameText.text = character.characterName;
        levelText.text = "Lvl " + character.level;
        hpText.text = "Hp " + character.health;
        mpText.text = "Mp " + character.manaPoints;

        expSlider.maxValue = 10;
        expSlider.value = character.currentExp;

        hpSlider.maxValue = character.maxHealth;
        hpSlider.value = character.health;

        mpSlider.maxValue = character.maxMana;
        mpSlider.value = character.manaPoints;
    }

    public void SetHP(int hp) {
        hpSlider.value = hp;
        hpText.text = "Hp" + hp;
    }
}
