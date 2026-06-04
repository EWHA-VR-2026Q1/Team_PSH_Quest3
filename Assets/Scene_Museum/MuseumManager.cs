using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MuseumManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI limitationText;
    public GameObject nextButton;

    [Header("NPCs")]
    public GameObject[] npcs;

    [Header("Texts")]
    public string[] limitationTexts;

    public string nextSceneName = "";

    private int phase = 0;

    void Start()
    {
        limitationText.gameObject.SetActive(false);
        if (nextButton != null)
            nextButton.SetActive(false);
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnTap();
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnTap();
        }
    }

    void OnTap()
    {
        if (phase == 0)
        {
            StartCoroutine(ShowLimitation());
            phase = 1;
        }
    }

    IEnumerator ShowLimitation()
    {
        limitationText.gameObject.SetActive(true);

        foreach (string text in limitationTexts)
        {
            limitationText.text = text;
            yield return new WaitForSeconds(2f);
        }

        if (nextButton != null)
            nextButton.SetActive(true);
    }

    public void OnNextButton()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}