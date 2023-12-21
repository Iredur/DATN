using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Guns")]
public class Guns : ScriptableObject
{
    public new string name;
    public Sprite sprite;

    public float fireRate;
    public float freezeRotationDuration;
}
