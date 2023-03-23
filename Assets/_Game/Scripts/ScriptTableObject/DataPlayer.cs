using UnityEngine;

[CreateAssetMenu(fileName = "DataPlayer", menuName = "ScriptableObjects/DataPlayer", order = 1)]
public class DataPlayer : ScriptableObject
{
    public Material[] materials;

    public Material GetMat(int colorType)
    {
        return materials[colorType];
    }
}