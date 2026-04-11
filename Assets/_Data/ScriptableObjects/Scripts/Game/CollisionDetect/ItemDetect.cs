using UnityEngine;

public class ItemDetect : MonoBehaviour
{
    [SerializeField]
    private ItemDetectSO _itemDetectData;
    private int currentHealth;

    public GameObject woodPrefab; // item drop
    public GameObject _root;
    void Awake()
    {
        currentHealth = _itemDetectData.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            ChopDown();
        }
    }

    void ChopDown()
    {
        DropWood();
        Destroy(_root);
    }

    void DropWood()
    {
        Instantiate(woodPrefab, transform.position, Quaternion.identity);
    }
}
