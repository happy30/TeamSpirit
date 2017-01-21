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

    private bool gameStarted;
    bool aButton;

    public float fadeValue;
    public GameObject blackScreen;
    bool fadeScreen;

    LoadController _load;
    KartGameManager _GameManager;

    void Awake()
    {
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

        if(arrowPos == ArrowPos.NewGame)
        {
            aButton = Input.GetButton("AButton1");
            if (aButton)
            {
                Invoke("FadeScreen", 0.5f);
                Invoke("StartGame", 2.5f);
                Invoke("FadeScreenOff", 3f);
                Invoke("StartCounting", 6f);
            }
        }
        if(arrowPos == ArrowPos.Back){
            Invoke("FadeScreen", 1f);
            Invoke("QuitGame", 2.5f);
        }
        if (fadeScreen)
        {
            fadeValue += 3f * Time.deltaTime;
            blackScreen.GetComponent<Image>().color = new Color(blackScreen.GetComponent<Image>().color.r, blackScreen.GetComponent<Image>().color.g, blackScreen.GetComponent<Image>().color.b, fadeValue / 0.8f);
        }
        else
        {
            fadeValue -= 3f * Time.deltaTime;
            blackScreen.GetComponent<Image>().color = new Color(blackScreen.GetComponent<Image>().color.r, blackScreen.GetComponent<Image>().color.g, blackScreen.GetComponent<Image>().color.b, fadeValue / 0.8f);
        }
    }

    public void FadeScreen()
    {
        fadeScreen = true;
    }
    public void FadeScreenOff()
    {
        fadeScreen = false;       
    }

    public void StartCounting()
    {
        _GameManager.startCountDown = true;
    }

    public void StartGame()
    {   
        menuCam.gameObject.SetActive(false);
        player1Cam.SetActive(true);
        player2Cam.SetActive(true);       
        hudPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void QuitGame()
    {
        _load.LoadScene("MainMenu");
    }
}
