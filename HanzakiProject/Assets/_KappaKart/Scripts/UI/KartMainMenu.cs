using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        arrowPos = ArrowPos.NewGame;

    }
    void Update()
    {
        if(Input.GetAxisRaw("Vertical") < 0)
        {
            arrowPos = ArrowPos.Back;
        }
        else if(Input.GetAxisRaw("Vertical") > 0)
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
    }
}
