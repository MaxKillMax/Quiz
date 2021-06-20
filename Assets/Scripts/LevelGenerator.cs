using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] Sprite[] NumberPack;
    [SerializeField] Sprite[] WordPack;

    [SerializeField] Vector3[] CardVectors;
    [SerializeField] GameObject CardPrefab;
    [SerializeField] PlayerTask playerTask;

    private List<GameObject> CardList = new List<GameObject>();
    private List<string> PastPickedMainCards = new List<string>();
    public int GameLevel = 0;

    // Какой из паков будет использован
    private int TypePack;
    // Выбрана ли карта из предложенного списка, неповторяющаяся
    private bool Pick;
    // Какая карта выбрана(номер)
    private int TypePick;
    // Список выбранных до этого карт
    private List<int> PickedCards = new List<int>();

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        // Сброс старой доски карт в случае, если она есть
        if (CardList.Count > 0)
        {
            for (int i = CardList.Count - 1; i >= 0; i--)
            {
                GameObject gameObject = CardList[i];
                CardList.RemoveAt(i);
                PickedCards.RemoveAt(i);
                Destroy(gameObject);
            }
        }

        // Выбор пака
        TypePack = Random.Range(0, 2);
        TypePack = 0;

        // Проверка на количество созданных карт
        GameLevel += 3;
        if (GameLevel > 9)
        {
            GameLevel = 3;

            for (int i = PastPickedMainCards.Count - 1; i >= 0; i--)
            {
                PastPickedMainCards.RemoveAt(i);
            }
        }

        // Создание всех карт на сцене
        for (int i = 0; i < GameLevel; i++)
        {
            CardList.Add(Instantiate(CardPrefab));

            // Цикл, который ждёт, пока карта не станет уникальной.
            do
            {
                switch (TypePack)
                {
                    case 0:
                        TypePick = Random.Range(0, NumberPack.Length);
                        CardList[CardList.Count - 1].GetComponent<SpriteRenderer>().sprite = NumberPack[TypePick];
                        break;

                    case 1:
                        TypePick = Random.Range(0, WordPack.Length);
                        CardList[CardList.Count - 1].GetComponent<SpriteRenderer>().sprite = WordPack[TypePick];
                        break;
                }

                if (PickedCards.Count > 0)
                {
                    for (int a = 0; a < PickedCards.Count; a++)
                    {
                        if (TypePick != PickedCards[a])
                        {
                            Pick = true;
                        }
                        else
                        {
                            a = PickedCards.Count;
                            Pick = false;
                        }
                    }
                }
                else
                {
                    Pick = true;
                }
            }
            while (!Pick);

            // Список карт, повернутых налево поворачивается обратно.
            if (CardList[CardList.Count - 1].GetComponent<SpriteRenderer>().sprite.name == "7" ||
                CardList[CardList.Count - 1].GetComponent<SpriteRenderer>().sprite.name == "8")
            {
                CardList[CardList.Count - 1].transform.eulerAngles = new Vector3(0, 0, -90);
            }

            // Установка местоположения в иерархии и на сцене. Также учёт в следующих картах.
            CardList[CardList.Count - 1].transform.parent = transform;
            CardList[CardList.Count - 1].transform.position = CardVectors[i];
            CardList[CardList.Count - 1].transform.name = TypePick.ToString("N0");
            PickedCards.Add(TypePick);
        }

        // Определение карты, которую нужно выбрать, опираясь на то, что повторений не должно быть
        for (int i = 0; i < 1;)
        {
            Pick = true;
            TypePick = Random.Range(0, CardList.Count);

            for (int a = 0; a < PastPickedMainCards.Count; a++)
            {
                if (CardList[TypePick].name == PastPickedMainCards[a])
                {
                    Pick = false;
                }
            }

            if (Pick)
            {
                i = 10;
            }
        }

        playerTask.RandomCard(CardList[TypePick]);
        PastPickedMainCards.Add(CardList[TypePick].name);

        if (GameLevel == 3)
        {
            for (int i = 0; i < CardList.Count; i++)
            {
                CardList[i].GetComponent<Card>().Bounce(CardList[i]);
            }
        }
    }

    public int SendToPlayerTask(GameObject gameObject)
    {
        return playerTask.Answer(gameObject);
    }
}
