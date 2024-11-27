using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [System.Serializable]
    public class Quest
    {
        public string targetFishTag;
        public Image questImage;
        public bool isCompleted = false; // Menambahkan flag untuk status quest
    }

    public Quest[] quests;
    public FinishGameScreen finishGameScreen; // Referensi ke FinishGameScreen

    public void CompleteQuest(string fishTag)
    {
        foreach (Quest quest in quests)
        {
            if (quest.targetFishTag == fishTag && !quest.isCompleted)
            {
                Debug.Log("Quest completed for fish tag: " + fishTag);
                quest.questImage.color = Color.green; // Ubah warna ke hijau
                quest.isCompleted = true; // Tandai quest sebagai selesai
                CheckAllQuestsCompleted(); // Cek jika semua quest selesai
                return;
            }
        }
    }

    private void CheckAllQuestsCompleted()
    {
        foreach (Quest quest in quests)
        {
            if (!quest.isCompleted) return; // Jika ada quest yang belum selesai, return
        }

        // Jika semua quest selesai, tampilkan layar selesai
        if (finishGameScreen != null)
        {
            Time.timeScale = 0;
            finishGameScreen.setup(); // Memanggil method setup di FinishGameScreen
        }
    }
}
