//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartMainMenu : MonoBehaviour
{
    public enum ArrowPos
    {
        NewGame,
        Back,
    };

    public ArrowPos arrowPos;
    public float yPos;
    public RectTransform arrow;
    public float arrowSpeed;

    public RectTransform newGameButton;
    public RectTransform backButton;

    public GameObject player1Cam;
    public GameObject player2Cam;
    public Transform menuCam;

    public GameObject menuPanel;
    public GameObject hudPanel;
    public GameObject rankPanel;

    public GameObject player1;
    public GameObject player2;

    private bool gameStarted;
    bool bButton;

    public float fadeValue;
    public GameObject blackScreen;
    bool fadeScreen;

    LoadController _load;
    KartGameManager _GameManager;

    Animator _anim;

    void Awake()
    {
        _anim = blackScreen.GetComponent<Animator>();
        arrowPos = ArrowPos.NewGame;
        _load = GetComponent<LoadController>();
        _GameManager = GetComponent<KartGameManager>();
 
    }
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            arrowPos = ArrowPos.Back;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            arrowPos = ArrowPos.NewGame;
        }


        if (arrowPos == ArrowPos.NewGame)
        {
            yPos = newGameButton.anchoredPosition.y;
        }
        else if (arrowPos == ArrowPos.Back)
        {
            yPos = backButton.anchoredPosition.y;
        }

        arrow.anchoredPosition = Vector2.Lerp(arrow.anchoredPosition, new Vector2(arrow.anchoredPosition.x, yPos), arrowSpeed * Time.deltaTime);

        if (!_GameManager.raceStart)
        {
            if (arrowPos == ArrowPos.NewGame) { 
                bButton = Input.GetButton("BButton1");
                if (bButton)
                {
                    Invoke("FadeScreen", 0.5f);
                    Invoke("StartGame", 1f);
                    Invoke("FadeScreenOff", 2f);
                    Invoke("StartCounting", 4f);
                }
            }
            if (arrowPos == ArrowPos.Back)
            {
                bButton = Input.GetButton("AButton1");
                if (bButton)
                {
                    Invoke("FadeScreen", 1f);
                    Invoke("QuitGame", 2.5f);
                }
            }
        }
    }

    public void FadeScreen()
    {
        menuPanel.SetActive(false);
        _anim.SetBool("Fade", true);
    }
    public void FadeScreenOff()
    {
        _anim.SetBool("Fade", false);
    }

    public void StartCounting()
    {
        _GameManager.startCountDown = true;
    }

    public void StartGame()
    {
        player1.GetComponent<KartController>().ResetKart();
        player2.GetComponent<KartController>().ResetKart();
        rankPanel.SetActive(false);
        player1Cam.SetActive(true);
        player2Cam.SetActive(true);
        menuCam.gameObject.SetActive(false);
        hudPanel.SetActive(true);

    }

    public void QuitGame()
    {
        _load.LoadScene("MainMenu");
    }
}
