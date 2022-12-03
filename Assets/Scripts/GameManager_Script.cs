using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Entities;
using UnityEngine.UIElements;
using Unity.Entities.UniversalDelegates;
using UnityEngine.SceneManagement;

namespace SFGA.Test
{
    public class GameManager_Script : MonoBehaviour
    {
        public static GameManager_Script Instance;

        [Header("Game Variables")]
        public float points = 0;
        public int ptsPerRock = 10;
        public int Lives = 5;

        [Header("GameObjects")]
        public TMP_Text pointsText;
        public GameObject[] livesArray;
        public GameObject gameOverPanel;
        private bool isGameOver = false;

        [Header("AudioSourcs Objects")]
        public AudioSource shootingAudio;
        public AudioSource destroyAudio;
        public AudioSource spaceShipAudio;
        public AudioSource loseLiveAudio;

        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
            Time.timeScale = 1;
        }
        private void Start()
        {
            //Activate Lives textures
            for (int i = 0; i < livesArray.Length; i++)
            {
                livesArray[i].SetActive(true);
            }
            Lives = livesArray.Length;
            gameOverPanel.SetActive(false);
        }
        private void Update()
        {
            //Change State of the movement Player
            if (Input.GetKeyDown(KeyCode.W))
                spaceShipON(true);
            if (Input.GetKeyUp(KeyCode.W))
                spaceShipON(false);
        }

        //Rest Lives when player Crash
        public void SpaceShipDestroy()
        {
            if (isGameOver)
                return;

            Lives--;
            if (Lives <= 0)
                GameOver();
            else
            {
                livesArray[Lives].SetActive(false);
                loseLiveAudio.Play();
            }
        }

        public void GameOver()
        {
            Debug.Log("GAME OVER");
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            isGameOver = true;
        }

        //Add point when destroy meteorite/Enemy
        public void addPoints(int multiply)
        {
            points = points + ptsPerRock * multiply;
            pointsText.text = points + " pts.";
            playDestroySound();
        }


        //State of the SpaceShip audio
        public void spaceShipON(bool isOn)
        {
            if (isOn)
            {
                Debug.Log("Moving SpaceShip");
                spaceShipAudio.Play();
            }
            else
            {
                spaceShipAudio.Stop();
            }
        }

        //Play shooting audio
        public void shooting()
        {
            shootingAudio.Play();
            Debug.Log("Shooting");
        }

        //Play destroy Sound
        private void playDestroySound()
        {
            destroyAudio.pitch = Random.Range(0.80f, 1.3f);
            destroyAudio.Play();
            Debug.Log("Destroy Metorite");
        }

        public void playAgain()
        {
            SceneManager.LoadScene(0);
        }
    }
}

