using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIShop : MonoBehaviour
{
    [SerializeField] private IDataSkin dataItem;
    [SerializeField] private Image imageSkin;
    
    [SerializeField] private Image imageButton;
    [SerializeField] private Image imageLock;

    public bool isOwnered;
    private void Awake()
    {
        SetUp();
    }
    private void SetUp()
    {
        isOwnered = false;
        OnImageLock();
        transform.GetComponent<Button>().onClick.AddListener(ClickOnButton);
    }

    private void ReSetButton()
    {
        isOwnered = false;
        OnImageLock();
    }

    public void ClickOnButton()
    {
        PopUpSkin.instance.UnSelectButtonItem();
        SelectButton();       
    } 
    public void OffImageLock()
    {
        imageLock.gameObject.SetActive(false);
    }
    public void OnImageLock()
    {
        imageLock.gameObject.SetActive(true);
    }
    public void SetDataButton(List<int> _skinOwner,IDataSkin dataSkin)
    {
        transform.gameObject.SetActive(true);
        dataItem = dataSkin;
        imageSkin.sprite = dataItem.spriteSkill;
        ReSetButton();
        for (int i = 0; i < _skinOwner.Count; i++)
        {          
            if (dataItem.idSkin == _skinOwner[i])
            {
                OwneredItem();
            }
        }
    }
    public void OwneredItem()
    {
        isOwnered = true;
        if (isOwnered)
        {
            OffImageLock();
        }
        else OnImageLock();
    }
    public void SelectButton()
    {
        PopUpSkin.instance.priceCurrent = dataItem.price;
        PopUpSkin.instance.buttonItemCurrent = this;
        PopUpSkin.instance.buttonItemIDCurrent = dataItem.idSkin;
        imageButton.color = Color.green;
        LevelManager.GetInstance().player.GetComponent<ChangSkin>().ChangeSkin(dataItem.skin, dataItem.prefabWing, dataItem.prefabTail, dataItem.prefabHead, dataItem.prefabBow, dataItem.shorts);
        if (isOwnered)
        {
            PopUpSkin.instance.OnOwner();
        }
        else
        {
            PopUpSkin.instance.OnNotOwner(dataItem);
        }
    }
    public void UnSelectButton()
    {
        imageButton.color = Color.white;
    }
}
