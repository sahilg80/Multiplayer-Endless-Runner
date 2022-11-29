using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    float speed;
    Vector3 offsetPos;
    GameObject _targetPlayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetOffsetValue(GameObject target)
    {
        _targetPlayer = target;
        offsetPos = _targetPlayer.transform.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_targetPlayer != null)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, _targetPlayer.transform.position.z - offsetPos.z);
            transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
        }
    }
}
