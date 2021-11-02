using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public sealed class PlayerTask : MonoBehaviour
{
    [SerializeField] LevelGenerator levelGenerator;
    [SerializeField] Animate animate;
    [SerializeField] Text text;
    [SerializeField] PlayerTask playerTask;
    private float timer;
    private GameObject GoodGameObject;

    public void RandomCard(GameObject Random)
    {
        text.DOFade(0, 0);
        text.DOFade(1, 0.25f);
        text.text = "Find " + Random.GetComponent<SpriteRenderer>().sprite.name;
        GoodGameObject = Random;
    }

    public int Answer(GameObject gameObject)
    {
        if (!animate.RestartActive)
        {
            if (gameObject == GoodGameObject)
            {
                if (levelGenerator.GameLevel == 9)
                {
                    animate.RestartOpen();
                }
                else
                {
                    playerTask.enabled = true;
                }
                return 1;
            }
            else
            {
                return -1;
            }
        }
        return 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0;
            playerTask.enabled = false;
            levelGenerator.Generate();
        }
    }
}
