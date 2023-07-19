using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Roulette : MonoBehaviour
{
    [Header("SpawnItems")]
    [SerializeField] private List<GameObject> _normalList;
    [SerializeField] private List<GameObject> _rareList;
    [SerializeField] private List<GameObject> _epicList;
    [SerializeField] private List<GameObject> _legendaryList;
    [Space]
    [SerializeField] private float _offsetBlock;
    [SerializeField, Range(1,10)] private float _speed;

    private List<RectTransform> _newListItems = new List<RectTransform>();

    private RectTransform _selectItem;

    private bool _isStart = false;
    private void Start()
    {
        SpawnItems();
    }

    private void FixedUpdate()
    {
        RouletteMovement();
    }

    private void RouletteMovement()
    {
        if (_isStart)
        {
            foreach (var item in _newListItems)
            {
                if (_selectItem.anchoredPosition.x > 219f)
                {
                    item.anchoredPosition -= new Vector2(_speed,0);
                }
                else
                {
                    Debug.Log("End");
                    _isStart = false;
                }
            }
        }
    }

    private void SpawnItems()
    {
        if (_normalList.Count != 0 && _rareList.Count != 0 && _epicList.Count != 0 && _legendaryList.Count != 0)
        {
            GameObject _newItems = null;
            for (int i = 0; i < 25; i++)
            {
                float proc = Random.Range(0, 100);
                if (proc <= 2)
                {
                     _newItems = Instantiate(_legendaryList[0], Vector3.zero, quaternion.identity, gameObject.transform);
                }
                else if (proc > 2 && proc <= 10)
                {
                    _newItems = Instantiate(_epicList[0], Vector3.zero, quaternion.identity, gameObject.transform);
                }
                else if (proc > 10 && proc <= 30)
                {
                    _newItems = Instantiate(_rareList[0], Vector3.zero, quaternion.identity, gameObject.transform);
                }
                else if (proc > 30 && proc <= 100)
                {
                    _newItems = Instantiate(_normalList[0], Vector3.zero, quaternion.identity, gameObject.transform);
                }
                RectTransform _rectTransform = _newItems.GetComponent<RectTransform>();
                _rectTransform.anchoredPosition = new Vector2(i * _offsetBlock, 0);
                _newListItems.Add(_rectTransform);
            }

            int randomItem = Random.Range(6, _newListItems.Count-1);
            _selectItem = _newListItems[randomItem];
            _isStart = true;
        }
        else
        {
            Debug.Log("Error");
        }
    }
}
