using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class Animate : MonoBehaviour
{
    [SerializeField] GameObject Restart;
    [SerializeField] LevelGenerator levelGenerator;
    [SerializeField] Image FadeObject;
    [SerializeField] GameObject button;
    [HideInInspector] public bool RestartActive;

    private bool StartTimer;
    private bool MiddleTimer;
    private float Timer;

    public void RestartOpen()
    {
        Restart.SetActive(true);
        RestartActive = true;

        FadeObject.DOFade(0.7f, 0.5f);
    }

    public void RestartClose()
    {
        FadeObject.DOFade(1, 0.5f);
        button.SetActive(false);
        StartTimer = true;
        MiddleTimer = false;
    }

    private void Update()
    {
        if (StartTimer)
        {
            Timer += Time.deltaTime;

            if (Timer >= 1f && !MiddleTimer)
            {
                levelGenerator.Generate();
                FadeObject.DOFade(0, 0.5f);
                MiddleTimer = true;
            }

            if (Timer >= 1.5f)
            {
                Restart.SetActive(false);
                button.SetActive(true);
                RestartActive = false;

                StartTimer = false;
                MiddleTimer = false;
                Timer = 0;
            }
        }
    }
}
