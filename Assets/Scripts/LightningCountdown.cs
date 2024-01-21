using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightningCountdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    private float countdown = 5.0f;
    public GameObject specialAttackPrefab; // Assicurati che questo sia disattivato all'inizio

    void Start()
    {
        if (specialAttackPrefab.activeSelf) // Se il prefab è attivo
        {
            specialAttackPrefab.SetActive(false); // Disattivalo
        }
    }

    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            countdownText.text = Mathf.Ceil(countdown).ToString();
        }
        else
        {
            countdownText.text = "GO!";
            specialAttackPrefab.SetActive(true); // Attiva il prefab

            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(WaitAndReset());
            }
        }
    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(0.3f); // Sostituisci con la durata dell'attacco

        ResetCountdown();
    }

    void ResetCountdown()
    {
        countdown = 5.0f;
        specialAttackPrefab.SetActive(false);
    }
}
