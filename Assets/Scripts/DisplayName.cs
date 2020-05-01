using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{
    private Player player;
    public Text displayName;
    // Start is called before the first frame update
    void Awake()
    {
        displayName.text = PlayerStats.Instance.characterName;
    }
    public void LoadWorldScene() {
        SceneManager.LoadScene("World");
    }

    public void LoadInputNameScene() {
        SceneManager.LoadScene("InputName");
    }
}
