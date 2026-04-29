using UnityEngine;
using TMPro;
using System.Collections;

public class WaveAnnouncement : MonoBehaviour
{
    public TextMeshProUGUI announcementText;
    public float displayTime = 3f;  //how long text shows

    void OnEnable()
    {
        WaveManager.OnWaveStarted += ShowWaveAnnouncement;
    }

    void OnDisable()
    {
        WaveManager.OnWaveStarted -= ShowWaveAnnouncement;
    }

    void Start()
    {
        //hide text at start
        if (announcementText != null)
            announcementText.gameObject.SetActive(false);
    }

    void ShowWaveAnnouncement(int wave)
    {
        StartCoroutine(DisplayAnnouncement(wave));
    }

    IEnumerator DisplayAnnouncement(int wave)
    {
        if(announcementText == null) yield break;

        //set text based on wave number
        if(wave == 1)
            announcementText.text = "Wave 1\nThe Forest Awakens!";
        else if (wave == 2)
            announcementText.text = "Wave 2\nThe Forest Fights Back!";
        else if (wave == 3)
            announcementText.text = "Wave 3\nThe Forest Strikes Back!";

        //show text
        announcementText.gameObject.SetActive(true);

        //fade in
        float elapsed = 0f;
        Color color = announcementText.color;
        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsed / 0.5f);
            announcementText.color = color;
            yield return null;
        }

        //hold only for display time
        yield return new WaitForSeconds(displayTime);

        //fade out the text
        elapsed = 0f;
        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsed / 0.5f);
            announcementText.color = color;
            yield return null;
        }

        //hide text
        announcementText.gameObject.SetActive(false);
    }
}