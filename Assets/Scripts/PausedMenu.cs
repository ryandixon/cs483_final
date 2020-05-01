using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isInventory = false;
    public GameObject pauseMenuUI;
    public GameObject inventoryMenuUI;

    void Start() {
        pauseMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(isPaused == true) {
                Resume();
            } else {
                Pause();
            }
        } else if(Input.GetKeyDown(KeyCode.C)) {
            if(isInventory == true) {
                Resume();
            } else {
                LoadInventory();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        inventoryMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Quit() {
        Application.Quit();
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadInventory() {
        //SceneManager.LoadScene("Inventory", LoadSceneMode.Additive);
        inventoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isInventory = true;
    }
}
