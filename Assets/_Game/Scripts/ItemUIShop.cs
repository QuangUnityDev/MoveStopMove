using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIShop : MonoBehaviour
{
    public Image imageSkin;
    public Image imageButton;
    public Image imageLock;
    public int price;
    public int id;
    public bool isOwnered;
    private void Awake()
    {
        isOwnered = false;
        transform.GetComponent<Button>().onClick.AddListener(ClickOnButton);
    }
    private void ClickOnButton()
    {
        IDataSkin[] dataSkin = PopUpSkin.instance.data_SkinCurrent.iDataSkin;
        LevelManager.GetInstance().player.GetComponent<ChangSkin>().ChangeSkin(dataSkin[id].skin, dataSkin[id].prefabWing, dataSkin[id].prefabTail, dataSkin[id].prefabHead, dataSkin[id].prefabBow, dataSkin[id].shorts);
        PopUpSkin.instance.priceCurrent = price;
        PopUpSkin.instance.txt_Buy.text = price.ToString();
        for (int i = 0; i < PopUpSkin.instance.containButtonCurrent.Count; i++)
        {
            PopUpSkin.instance.containButtonCurrent[i].imageButton.color = Color.white;
        }
        imageButton.color = Color.green;
        PopUpSkin.instance.currentUsingSkin = id;
        PopUpSkin.instance.CheckBought(isOwnered);
    } 
}
