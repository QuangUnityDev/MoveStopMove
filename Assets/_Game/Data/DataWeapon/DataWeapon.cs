using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DataWeapon", menuName = "ScriptableObjects/DataWeapon", order = 1)]
public class DataWeapon : ScriptableObject
{
    public string nameWeapon;
    public int idWeapon;
    public int numberOfSkin;
    public int price;
    public IDataWeapon[] iDataWeapon;  
}
