using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button button1;
    public string nameText;
    public GameObject inputText;
    public Text displayName;
    public Player player;

    public void Start() {
        //input = GetComponent<InputField>();
        //button1 = GetComponent<Button>();
    }

    public void LoadGame() {
        SceneManager.LoadScene("World");
    }

    public void LoadControlScence(){
        SceneManager.LoadScene("Controls");
    }

    public void Quit() {
        Application.Quit();
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Main");
    }

    public void LoadSelectName() {
        SceneManager.LoadScene("InputName");
    }

    public void LoadNameSureMenu() {
        SceneManager.LoadScene("NameSure");
    }

    public void InputName() {
        nameText = inputText.GetComponent<Text>().text;
        PlayerStats.Instance.characterName = nameText;
        Seed();
    }

    public void Seed() {
        //initial player seeded stats
        PlayerStats.Instance.level = 1;
        PlayerStats.Instance.currentExp = 0;
        PlayerStats.Instance.health = 10;
        PlayerStats.Instance.maxHealth = 10;
        PlayerStats.Instance.attackPower = 3;
        PlayerStats.Instance.defencePower = 1;
        PlayerStats.Instance.manaPoints = 10;
        PlayerStats.Instance.maxMana = 10;
        PlayerStats.Instance.gold = 0;
    }
}
