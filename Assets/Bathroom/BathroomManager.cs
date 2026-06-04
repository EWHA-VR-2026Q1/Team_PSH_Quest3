using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BathroomManager : MonoBehaviour
{
    [Header("Particles")]
    public ParticleSystem waterParticle;
    public ParticleSystem steamParticle;

    [Header("Audio")]
    public AudioSource waterSound;

    [Header("UI")]
    public TextMeshProUGUI limitationText;
    public GameObject nextButton;

    private int phase = 0;

    void Start()
    {
        limitationText.gameObject.SetActive(false);
        nextButton.SetActive(false);
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnTap();
        }

        // 에디터 테스트용
        if (Input.GetMouseButtonDown(0))
        {
            OnTap();
        }
    }

    void OnTap()
    {
        if (phase == 0)
        {
            waterParticle.Play();
            steamParticle.Play();
            waterSound.Play();
            phase = 1;
        }
        else if (phase == 1)
        {
            StartCoroutine(ShowLimitation());
            phase = 2;
        }
    }

    IEnumerator ShowLimitation()
    {
        limitationText.gameObject.SetActive(true);

        limitationText.text = "Water passed through you";
        yield return new WaitForSeconds(2f);

        limitationText.text = "You are not wet";
        yield return new WaitForSeconds(2f);

        limitationText.text = "You feel no heat";
        yield return new WaitForSeconds(2f);

        limitationText.text = "The shower is over";
        yield return new WaitForSeconds(2f);

        nextButton.SetActive(true);
    }

    public void OnNextButton()
    {
        SceneManager.LoadScene("Main");
    }
}