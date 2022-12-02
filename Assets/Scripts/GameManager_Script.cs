using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager_Script : MonoBehaviour
{
    public static GameManager_Script Instance;

    public int ptsRock = 10;

    public TMP_Text pointsText;
    public float points = 0;

    public TMP_Text LivesText;
    public int Lives = 5;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
    }

    public void SpaceShipDestroy()
    {
        Lives--;
        LivesText.text = Lives + " Lives";

        if (Lives <= 0)
            GameOver();
    }

    public  void GameOver()
    {
        Debug.Log("GAME OVER");
        Time.timeScale = 0;
    }

    public  void addPoints()
    {
        points = points + ptsRock;
        pointsText.text = points + " pts.";
        playDestroySound();
    }

    public void addDoublePoints()
    {
        points = points + ptsRock*2;
        pointsText.text = points + " pts.";
        playDestroySound();
    }

    public void spaceShipON()
    {
        Debug.Log("Moving SpaceShip");
    }

    public void shooting()
    {
        Debug.Log("Shooting");
    }

    private void playDestroySound()
    {
        Debug.Log("Destroy Metorite");
    }


}
