using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthText : MonoBehaviour
{
    TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Awake()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    public void SetHealthText(int Health)
    {
        healthText.text = Health.ToString();
    }
}
