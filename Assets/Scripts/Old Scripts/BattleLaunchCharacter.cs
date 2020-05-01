using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLaunchCharacter : MonoBehaviour
{
    [SerializeField]
    private List<Character> players, enemies;
    [SerializeField]
    private BattleLauncher launcher;

    public void PrepareBattle(Character character) {
        launcher.PrepareBattle(enemies, players, character.transform.position);
    }
}
