using UnityEngine;
using UnityEngine.UI;

public class ComboBar : MonoBehaviour
{
    public Image comboBarFill;  // Dışarıdan atanacak Image (bar)
    public float fillSpeed = 1.5f;  // Barın dolma hızı
    private float currentFill = 0f;  // Mevcut doluluk oranı
    public static ComboBar instance;
    private void Awake()
    {
        instance = this;
    }
    public void NewGame()
    {
        currentFill = 0;
        comboBarFill.fillAmount = currentFill;

    }
    void Update()
    {
 
    }

    public void AddCombo(float amount)
    {
        currentFill += amount;
        if (currentFill > 1f)
        {
            currentFill = 1f; // Maksimum 100%
        }
        comboBarFill.fillAmount = currentFill;
    }
}
