using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    public void Interact(Character player = null)
    {
        if (GetComponent<BattleLaunchCharacter>() != null)
        {
            GetComponent<BattleLaunchCharacter>().PrepareBattle(player);

        }
    }   
}
