using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    GameObject player;
    float positionOfPlayer;
    [SerializeField] ParticleSystem finishEffect;
    [SerializeField] Text percentText;
    [SerializeField] TMP_Text rankText;
    [SerializeField] Button playButton;
    [SerializeField] Button restartButton;
    [SerializeField] Image congratsImage;
    [SerializeField] Button quitButton;
    [SerializeField] GameObject[] agentsArray;
    public ArrayList ranking = new ArrayList();
    void Start()
    {
        positionOfPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.position.z;
        playButton.gameObject.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (GameObject opponent in GameObject.FindGameObjectsWithTag("Opponent"))
        {
            opponent.GetComponent<GirlController>().enabled = false;
        }
        player.GetComponent<PlayerMovement>().enabled = false;
    }

    void Update()
    {
        if (percentText.text == "% 100 of the wall has been painted.")
        {
            GameOver();
        }
    }

    private void FixedUpdate()
    {
        agentsArray = GameObject.FindGameObjectsWithTag("Opponent");

        for (int i = 0; i < agentsArray.Length; i++)
        {

            ranking.Add(agentsArray[i].transform.position.z);
            ranking.Add(GameObject.FindGameObjectWithTag("Player").transform.position.z);
            ranking.Sort();

            rankText.text = "Position: " + (11 - ranking.IndexOf(GameObject.FindGameObjectWithTag("Player").transform.position.z)).ToString() + "/11";
        }
        ranking.Clear();
    }

    public void Play()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        foreach (GameObject opponent in GameObject.FindGameObjectsWithTag("Opponent"))
        {
            opponent.GetComponent<GirlController>().enabled = true;
        }
        playButton.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        finishEffect.Play();
        restartButton.gameObject.SetActive(true);
        congratsImage.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        Cursor.SetCursor(default, Vector2.zero, CursorMode.Auto);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
