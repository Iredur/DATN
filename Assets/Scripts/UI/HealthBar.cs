
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    float health, maximumHealth = 100;
    float lerpSpeed;
    [SerializeField] Player player;


    private void Start()
    {

    }
    private void Update()
    {
        HealthBarFiller();
        lerpSpeed = 3 * Time.deltaTime;
        ColorChange();
    }
    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, player.health / maximumHealth, lerpSpeed);
    }
    void ColorChange()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, player.health / maximumHealth);
        healthBar.color = healthColor;
    }
}
