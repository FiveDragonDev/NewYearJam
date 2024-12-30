using UnityEngine;

public class ChampagneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _champagnePrefab;

    [SerializeField] private Entity _currentChampagne;

    private void Start()
    {
        if (_currentChampagne == null) CreateChampagne();
        else _currentChampagne.OnLose.AddListener(() => CreateChampagne());
    }

    private void CreateChampagne()
    {
        Destroy(_currentChampagne.gameObject);
        _currentChampagne = Instantiate(_champagnePrefab, transform.position,
            Quaternion.identity).GetComponent<Entity>();
        PlayerDialogue.OpenDialogue("Ну и нахер ты это сделал, я ж верну", 0.2f, 1.5f);
        _currentChampagne.OnLose.AddListener(() => CreateChampagne());
    }
}
