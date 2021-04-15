using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum Direction { UP, DOWN };
public class floatingStone : MonoBehaviour
{
    public float _distance;

    // Variables for floating
    private Vector3 _top, _bottom;
    private float _percent = 0.0f;
    public float _speed = 0.3f;
    private Direction _direction;
    public bool rotating = false;
    // Start is called before the first frame update
    void Start()
    {
        _direction = Direction.UP;
        _top = new Vector3(transform.position.x,
                           transform.position.y + _distance,
                           transform.position.z);
        _bottom = new Vector3(transform.position.x,
                              transform.position.y - _distance,
                              transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        ApplyFloatingEffect();
        if (rotating)
        {
            ApplyRotationEffect();
        }
    }
    void ApplyFloatingEffect()
    {
        if (_direction == Direction.UP && _percent < 1)
        {
            _percent += Time.deltaTime * _speed;
            transform.position = Vector3.Lerp(_top, _bottom, _percent);
        }
        else if (_direction == Direction.DOWN && _percent < 1)
        {
            _percent += Time.deltaTime * _speed;
            transform.position = Vector3.Lerp(_bottom, _top, _percent);
        }
        if (_percent >= 1)
        {
            _percent = 0.0f;
            if (_direction == Direction.UP)
            {
                _direction = Direction.DOWN;
            }
            else
            {
                _direction = Direction.UP;
            }
        }
    }
    void ApplyRotationEffect()
    {
        transform.Rotate(Vector3.right, Time.deltaTime * 10f);
    }
}

