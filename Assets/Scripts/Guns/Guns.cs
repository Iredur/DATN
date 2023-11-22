using UnityEngine;

[CreateAssetMenu(fileName = "New Gun",menuName = "Guns")]
public class Guns : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite sprite;

    public int fireRate,damage,bulletType,level;
}
