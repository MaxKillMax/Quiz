using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer Sprite;
    LevelGenerator levelGenerator;
    private int Answer;

    private void OnMouseDown()
    {
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
        Vector3 vector = gameObject.transform.position;
        gameObject.transform.DOShakePosition(0.5f, new Vector3(1, 0.2f, 0), 10, 90);
        gameObject.transform.position = vector;
    }

    public void easeinBounce(GameObject gameObject)
    {
        Vector3 vector = gameObject.transform.position;
        gameObject.transform.DOShakeScale(1, 0.3f, 10, 90);
        gameObject.transform.position = vector;
    }
}
