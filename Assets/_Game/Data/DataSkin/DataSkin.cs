using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DataSkin", menuName = "ScriptableObjects/DataSkin", order = 1)]
public class DataSkin : ScriptableObject
{
    [SerializeField] private string nameData;
    public int amountOfSkin;
    public IDataSkin[] iDataSkin;
}
