using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPrefs : MonoBehaviour
{
    private float controllerSensitivity;

    void Start()
    {
        // Memuat sensitivitas dari PlayerPrefs
        controllerSensitivity = PlayerPrefs.GetInt("controllerSensitivity", 4);

        // Terapkan sensitivitas pada kontrol pemain
        ApplyControllerSensitivity(controllerSensitivity);
    }

    private void ApplyControllerSensitivity(float sensitivity)
    {
        // Contoh penerapan: Ubah sensitivitas input
        // Misal untuk kamera atau kontrol pemain
        Debug.Log("Controller Sensitivity set to: " + sensitivity);

        // Implementasikan logika khusus untuk kontrol sensitivitas di game
    }
}
