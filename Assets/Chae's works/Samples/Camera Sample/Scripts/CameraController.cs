using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Camera에 직접 넣는 스크립트

    [SerializeField]
    Transform _player;
    [SerializeField]
    Transform _target;
    Transform _camera;

    float _dist = 5f; // player와 target 사이의 거리
    Vector3 _playerSide; // camera가 player로부터 얼마나 옆으로 가는지 <- 줌 상태일 경우 살짝 변경
    Vector3 _playerHeight; // camera가 player로부터 얼마나 위로 가는지
    Vector3 _targetpos; // target의 x, z 좌표를 저장할 변수
    Vector3 _camerapos; // dist를 고려 안 한 camera의 초기 위치
    Vector3 _reverseDist; // player와 target 사이의 거리에 비례해서 camera가 얼마나 플레이어의 뒤로 가는지

    public bool _isZoom; // 줌 여부
    public float _toggle;

    public bool _isShake; // 카메라 shake 여부
    public float _ShakeAmount; // shake 강도
    public float _ShakeTime; // shake 지속시간

    void Start()
    {
        _camera = transform;
        _playerSide = new Vector3(0f, 0f, 0f);
        _playerHeight = new Vector3(0f, 1.5f, 0f);

        _ShakeTime = 0.1f;
        _ShakeAmount = 0.05f;
    }


    void Update()
    {
        IsZoom();
        AutoTargeting();

        if (_isShake)
        {
            _isShake = false;
            StartCoroutine("CameraShake");
        }
    }

    void AutoTargeting()
    {
        // target의 위치를 기반으로 player의 시점 조정


        // player의 위치와 target과의 거리를 기반으로 camera의 위치 조정
        if(_target == null)
        {
            TargetNotExist();
            return;
        }

        _camerapos = _player.position + _playerHeight + _player.rotation * _playerSide;
        _dist = Vector3.Distance(_player.position, _target.position);

        if (!_isZoom)
        {
            if (_dist >= 3f) // 너무 가깝거나 멀 경우에 적절히 위치 조정
            {
                _reverseDist = new Vector3(0f, 0f, -3f);
            }
            else if (_dist <= 2f)
            {
                _reverseDist = new Vector3(0f, 0f, -2f);
            }
            else
            {
                _reverseDist = new Vector3(0f, 0f, -_dist);
            }
        }
        else
        {
            _reverseDist = new Vector3(0f, 0.1f, -1f);
        }

        // _camera.position = _camerapos + _camera.rotation * _reverseDist; // <- camera의 최종 위치
        transform.position = Vector3.Lerp(transform.position, _camerapos + _camera.rotation * _reverseDist, Time.deltaTime * 5f);

        // target의 위치를 기반으로 camera의 시점 조정
        _camera.LookAt(new Vector3(_target.transform.position.x, 0.8f, _target.transform.position.z));
    }

    void TargetNotExist()
    {
        transform.rotation = _player.rotation;
        transform.position = _player.position - transform.forward * 5f + Vector3.up * 3f;
    }

    void IsZoom()
    {
        //if (Input.GetKeyDown(KeyCode.C) && !_isZoom)
        //{
        //    _isZoom = true;
        //}
        //else if (Input.GetKeyDown(KeyCode.C) && _isZoom)
        //{
        //    _isZoom = false;
        //}

        if (_isZoom && _toggle <= 0.4999f)
        {
            _toggle = Mathf.Lerp(_toggle, 0.5f, 0.2f);
            _playerSide = new Vector3(_toggle, 0f, 0f);
        }
        else if (!_isZoom && _toggle >= 0.0001f)
        {
            _toggle = Mathf.Lerp(_toggle, 0f, 0.2f);
            _playerSide = new Vector3(_toggle, 0f, 0f);
        }
    }

    IEnumerator CameraShake()
    {
        Vector3 camPos = transform.position;

        float _timer = 0f;

        while (_timer <= _ShakeTime)
        {
            transform.localPosition = Random.insideUnitSphere * _ShakeAmount + camPos;
            yield return null;

            _timer += Time.deltaTime;
        }

        transform.localPosition = camPos;
    }
}
