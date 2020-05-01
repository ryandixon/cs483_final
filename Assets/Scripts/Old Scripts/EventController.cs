using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static event System.Action OnBattleCompleted = delegate {};
    public static void BattleCompleted() {
        OnBattleCompleted();
    }
}
