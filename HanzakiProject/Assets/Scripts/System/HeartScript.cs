//HeartScript by Arne & Jordi

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class HeartScript : MonoBehaviour {

	public List<GameObject> heartList = new List<GameObject>();
	public GameObject heartPanel;
	public GameObject hearts;
    public StatsManager stats;

    public GameObject emptyHeart;
    public GameObject filledHeart;

    public Sprite leftSideHeart;
    public Sprite rightSideHeart;
    Sprite heartSprite;

    bool leftSide = true;

    GameObject spawnedHeart;
    GameObject spawnedEmptyHeart;
	

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
    }

    void Start()
    {
        DrawHearts();
    }


	public void DrawEmptyHearts()
	{
        for (int i = 0; i < stats.maxHealth / 2; i++)
        {
            spawnedEmptyHeart = (GameObject)Instantiate(emptyHeart);
            spawnedEmptyHeart.transform.SetParent(heartPanel.transform);
            spawnedEmptyHeart.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            spawnedEmptyHeart.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * 100, 0, 0);
            heartList.Add(spawnedEmptyHeart);
        }


    }
	public void DrawHearts()
	{
        //Clear current hearts so we can redraw them
        for(int i = 0; i < heartList.Count; i++)
        {
            Destroy(heartList[i]);
        }
        heartList.Clear();
        leftSide = true;

        //Draw hearts
        DrawEmptyHearts();

        for(int i = 0; i < stats.health; i++)
        {
            if(leftSide)
            {
                heartSprite = leftSideHeart;
                leftSide = false;
                
            }
            else
            {
                heartSprite = rightSideHeart;
                leftSide = true;

            }

            spawnedHeart = (GameObject)Instantiate(filledHeart);
            spawnedHeart.transform.SetParent(heartPanel.transform);
            spawnedHeart.GetComponent<Image>().sprite = heartSprite;
            spawnedHeart.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            spawnedHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25 + i * 50, 0);
            heartList.Add(spawnedHeart);
        }
    }
}
