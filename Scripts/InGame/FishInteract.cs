using UnityEngine;

public class FishInteract : MonoBehaviour
{
    private Flock flockScript;  // Referensi ke Flock
    private QuestManager questManager; // Referensi ke QuestManager
    public GameObject interactUi; // UI interaksi yang akan ditampilkan
    private bool isInteractable = false;

    void Start()
    {
        // Mencari Flock di scene dan mendapatkan referensi ke UI Interact dari Flock
        flockScript = FindObjectOfType<Flock>();
        if (flockScript != null && flockScript.interactUiPrefab != null)
        {
            interactUi = flockScript.interactUiPrefab;
        }
        else
        {
            Debug.LogError("Flock or Interact UI not found!");
        }

        // Mencari QuestManager di scene
        questManager = FindObjectOfType<QuestManager>();
        if (questManager == null)
        {
            Debug.LogError("QuestManager not found in the scene!");
        }

        // Nonaktifkan UI Interaksi di awal
        if (interactUi != null)
        {
            interactUi.SetActive(false);
        }
    }

    void Update()
    {
        // Jika ikan bisa berinteraksi dan tombol F ditekan
        if (isInteractable && Input.GetKeyDown(KeyCode.F))
        {
            // Kirim tag ikan ke QuestManager untuk menyelesaikan quest
            if (questManager != null)
            {
                questManager.CompleteQuest(gameObject.tag); // Kirim tag ikan ke QuestManager
            }

            Debug.Log("Quest completed for fish: " + gameObject.name);

            // Nonaktifkan UI interaksi setelah quest selesai
            if (interactUi != null)
            {
                interactUi.SetActive(false); // Menonaktifkan UI setelah quest selesai
            }

            // Hancurkan ikan setelah interaksi
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Submarine"))
        {
            if (gameObject.CompareTag("hiu") && flockScript != null)
            {
                // Jika menyentuh hiu, panggil GameOver melalui Flock
                flockScript.TriggerGameOver("Anda ditangkap hiu!");
                return;
            }

            isInteractable = true;

            // Aktifkan UI saat interaksi dimulai
            if (interactUi != null)
            {
                interactUi.SetActive(true); // Mengaktifkan UI interaksi
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Submarine"))
        {
            isInteractable = false;

            // Nonaktifkan UI ketika keluar dari trigger
            if (interactUi != null)
            {
                interactUi.SetActive(false); // Menonaktifkan UI interaksi
            }
        }
    }
}
