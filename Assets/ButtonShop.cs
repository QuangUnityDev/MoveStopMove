using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShop : MonoBehaviour
{
    public Image imageSkin;
    public Image imageButton;
    public int price;
    public int id;
    private void Awake()
    {
        transform.GetComponent<Button>().onClick.AddListener(ClickOnButton);
    }
    void ClickOnButton()
    {
        PopUpSkin.GetInstance().priceCurrent = price;
        PopUpSkin.GetInstance().txt_Buy.text = price.ToString();
        for (int i = 0; i < PopUpSkin.GetInstance().containButtonCurrent.Count; i++)
        {
            PopUpSkin.GetInstance().containButtonCurrent[i].imageButton.color = Color.white;
        }
        imageButton.color = Color.green;
    }
}
