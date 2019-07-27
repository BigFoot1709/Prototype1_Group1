using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEIcon : MonoBehaviour
{

    public List<Sprite> iconSprites;
    private Sprite _chosenSprite;

    // Start is called before the first frame update
    void Start()
    {
        _chosenSprite = null;
        //RandomiseIcon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomiseIcon()
    {
        int randomNum = Random.Range(0, iconSprites.Count);

        if (_chosenSprite != null)
        {
            if (_chosenSprite != iconSprites[randomNum])
            {
                _chosenSprite = iconSprites[randomNum];
            }
            else
            {
                if (randomNum - 1 > 0)
                {
                    _chosenSprite = iconSprites[randomNum - 1];
                }
                else
                {
                    _chosenSprite = iconSprites[iconSprites.Count-1];
                }
            }
        } else
        {
            _chosenSprite = iconSprites[randomNum];
        }

        this.GetComponent<Image>().sprite = _chosenSprite;
    }

    public string GetCurrIcon()
    {
        string returnString = "";

        if (_chosenSprite!=null)
        {
            if (_chosenSprite.name.Contains("Circle"))
            {
                returnString = "Cancel";
            } else if (_chosenSprite.name.Contains("Cross"))
            {
                returnString = "Submit";
            }
            else if (_chosenSprite.name.Contains("Square"))
            {
                returnString = "PS4_Square";
            }
            else if (_chosenSprite.name.Contains("Triangle"))
            {
                returnString = "PS4_Triangle";
            }
        }

        return returnString;
    }
}
