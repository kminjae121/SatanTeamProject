using UnityEngine;
using TMPro;

public interface IUseItem
{
    public void Use(Animator animator);
}

public interface IPuzzleItem
{
    public void TakeItem();
}

public class Item : MonoBehaviour
{
    public ItemSO currentItem;

    [SerializeField]
    private Player player;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private IUseItem itemTest;


    private GameObject handleObj;

    private void Awake()
    {
        player._inputReader.OnThrow += ThrowItem;
        player.OnClick += UseItem;
    }

    private void Start()
    {
        ChangeItem();
    }

    private void Update()
    {
        if (currentItem != null)
        {
            text.text = currentItem.itemName;
        }
        else
        {
            text.text = "맨손";
        }
    }

    public void ChangeItem()
    {
        //손에 바꾸가
        if(handleObj != null)
            Destroy(handleObj);
        GameObject gameObject = Instantiate(currentItem.itemPrefab, transform);
        gameObject.transform.SetParent(transform);
        handleObj = gameObject;
    }

    private void UseItem()
    {
        handleObj.GetComponent<IUseItem>().Use(handleObj.GetComponent<Animator>());
        //if (currentItem == null) return;

        //currentItem.itemPrefab.GetComponent<IUseItem>().Use();
    }

    public void ThrowItem()
    {
        if (currentItem == null) return;
    }

    private void OnDisable()
    {
        player._inputReader.OnThrow -= ThrowItem;
        player.OnClick -= UseItem;
    }
}
