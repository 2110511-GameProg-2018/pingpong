using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

    public Card card;
    public Text nameText;
    public Text typeText;
    public Text cidText;
    public Text descriptionText;
    public Image Image;

    // Use this for initialization
    void Start()
    {
        nameText.text = card.nameTH;
        typeText.text = card.type;
        cidText.text = card.cid.ToString();
        descriptionText.text = card.descriptionTh;
        Image.sprite = card.Image;
    }

}
