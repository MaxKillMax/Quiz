using UnityEngine;
using DG.Tweening;

public sealed class Card : MonoBehaviour
{
    // Скрипт отвечает за нажатия каждой карты и её анимации
    [SerializeField] SpriteRenderer Sprite;
    LevelGenerator levelGenerator;

    private int Answer;

    private bool firstActivated = false;
    private Vector3 startPosition;


    private void OnMouseDown()
    {
        if (firstActivated == false)
        {
            firstActivated = true;
            startPosition = transform.position;
        }

        if (levelGenerator == null)
        {
            levelGenerator = GetComponentInParent<LevelGenerator>();
            Answer = levelGenerator.SendToPlayerTask(gameObject);
        }
        else
        {
            Answer = levelGenerator.SendToPlayerTask(gameObject);
        }

        switch (Answer)
        {
            case -1:
                Bounce(gameObject);
                break;
            case 1:
                easeinBounce(gameObject);
                break;
            default:
                break;
        }
    }

    public void Bounce(GameObject gameObject)
    {
        gameObject.transform.DOShakePosition(0.5f, new Vector3(1, 0.2f, 0), 10, 90);
        gameObject.transform.position = startPosition;
    }

    public void easeinBounce(GameObject gameObject)
    {
        gameObject.transform.DOShakeScale(1, 0.3f, 10, 90);
        gameObject.transform.position = startPosition;
    }
}
