using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSkin : MonoBehaviour
{
    [SerializeField] private Button bt_HornSkin;
    [SerializeField] private Button bt_ShortsSkin;
    [SerializeField] private Button bt_ArmSkin;
    [SerializeField] private Button bt_SkinShop;

    [SerializeField] private List<ButtonSkinShop> btSkinShopPool;

    [SerializeField] private List<ButtonSkinShop> btSkinShopInHornSkin;
    [SerializeField] private List<ButtonSkinShop> btSkinShopInShortsSkin;


    [SerializeField] private DataSkin data_HornSkin;
    [SerializeField] private DataSkin data_ShortsSkin;

    [SerializeField] private GameObject cointainHornSkin;
    [SerializeField] private GameObject cointainShortsSkin;
    [SerializeField] private bool isCreateNew = true;


    [SerializeField] private TypeSkinShop typeSkinShopCurrent;

    private bool _isHadObject = false;
    private void Awake()
    {
        bt_HornSkin.onClick.AddListener(GenHornSkinShop);
        bt_ShortsSkin.onClick.AddListener(GenShortsSkinShop);
    }
    private void OnEnable()
    {
        GenHornSkinShop();
    }
    void Update()
    {

    }
    void ClearAllContain()
    {
        btSkinShopInHornSkin.Clear();
        btSkinShopInShortsSkin.Clear();
    }
    public void ClearButton()
    {
        ClearAllContain();
        for (int i = 0; i < btSkinShopPool.Count; i++)
        {
            btSkinShopPool[i].gameObject.SetActive(false);

        }
    }
    public void GenHornSkinShop()
    {
        if (typeSkinShopCurrent == TypeSkinShop.HornSkin) return;
        typeSkinShopCurrent = TypeSkinShop.HornSkin;
        ClearButton();
        for (int i = 0; i < data_HornSkin.amountOfSkin; i++)
        {
            ButtonSkinShop go = SpawnButtonUI(cointainHornSkin);
            //go.imageSkin.sprite = data_HornSkin.imageSkill[i];
            btSkinShopInHornSkin.Add(go);
        }
    }
    public void GenShortsSkinShop()
    {
        if (typeSkinShopCurrent == TypeSkinShop.ShortsSkin) return;
        ClearButton();
        typeSkinShopCurrent = TypeSkinShop.ShortsSkin;
        for (int i = 0; i < data_ShortsSkin.amountOfSkin; i++)
        {
            ButtonSkinShop go = SpawnButtonUI(cointainShortsSkin);
            //go.imageSkin.sprite = data_HornSkin.imageSkill[i];
            btSkinShopInShortsSkin.Add(go);
        }
    }
    public ButtonSkinShop SpawnButtonUI(GameObject contain)
    {
        for (int i = 0; i < btSkinShopPool.Count; i++)
        {
            if (!btSkinShopPool[i].gameObject.activeSelf)
            {
                btSkinShopPool[i].gameObject.transform.SetParent(contain.transform);
                btSkinShopPool[i].gameObject.SetActive(true);
                _isHadObject = true;
                return btSkinShopPool[i];
            }
            else
            {
                _isHadObject = false;
            }
        }
        if (isCreateNew)
        {
            if (!_isHadObject)
            {
                ButtonSkinShop more = Instantiate(btSkinShopPool[0], contain.transform);
                more.gameObject.SetActive(true);
                btSkinShopPool.Add(more);
                return more;
            }
        }
        return null;

    }
    public enum TypeSkinShop
    {
        None,
        HornSkin,
        ShortsSkin,

    }
}
