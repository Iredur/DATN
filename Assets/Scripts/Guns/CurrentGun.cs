using UnityEngine;

public class CurrentGun : MonoBehaviour
{

    [SerializeField] AudioClip shoot;
    [SerializeField] public Guns currentGun;
    [SerializeField] Guns defaultGun;
    [SerializeField] GunBehaviour weapon;
    [SerializeField] P90 p90;
    [SerializeField] Vortex vortex;
    [SerializeField] Pearl pearl;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (currentGun == null)
        {
            SwapGun(defaultGun);
        }
    }
    private void Update()
    {

    }
    public void SwapGun(Guns guns)
    {
        (guns, currentGun) = (currentGun, guns);
        spriteRenderer.sprite = currentGun.sprite;
        switch (currentGun.name)
        {
            case "p90":
                weapon = p90;
                break;
            case "vortex":
                weapon = vortex;
                break;
            case "pearl":
                weapon = pearl;
                break;
        }
        weapon.weaponType = currentGun;
    }
    public void Shoot(Vector3 spawnPos, Quaternion rotation, float fireRateMultiplier, PlayerController playerController)
    {
        SoundManager.Instance.PlaySound(shoot);
        weapon.Shoot(spawnPos, rotation, fireRateMultiplier, playerController);
    }
    public void AltShoot(Vector3 spawnPos, Quaternion rotation, PlayerController playerController)
    {
        SoundManager.Instance.PlaySound(shoot);
        weapon.AltShoot(spawnPos, rotation, playerController);
    }

}
