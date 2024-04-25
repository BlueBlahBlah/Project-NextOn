using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Camera�� ���� �ִ� ��ũ��Ʈ

    [SerializeField]
    Transform _player;
    [SerializeField]
    Transform _target;
    Transform _camera;

    float _dist = 5f; // player�� target ������ �Ÿ�
    Vector3 _playerSide; // camera�� player�κ��� �󸶳� ������ ������ <- �� ������ ��� ��¦ ����
    Vector3 _playerHeight; // camera�� player�κ��� �󸶳� ���� ������
    Vector3 _targetpos; // target�� x, z ��ǥ�� ������ ����
    Vector3 _camerapos; // dist�� ��� �� �� camera�� �ʱ� ��ġ
    Vector3 _reverseDist; // player�� target ������ �Ÿ��� ����ؼ� camera�� �󸶳� �÷��̾��� �ڷ� ������

    public bool _isZoom; // �� ����
    public float _toggle;

    public bool _isShake; // ī�޶� shake ����
    public float _ShakeAmount; // shake ����
    public float _ShakeTime; // shake ���ӽð�

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
        // target�� ��ġ�� ������� player�� ���� ����


        // player�� ��ġ�� target���� �Ÿ��� ������� camera�� ��ġ ����
        if(_target == null)
        {
            TargetNotExist();
            return;
        }

        _camerapos = _player.position + _playerHeight + _player.rotation * _playerSide;
        _dist = Vector3.Distance(_player.position, _target.position);

        if (!_isZoom)
        {
            if (_dist >= 3f) // �ʹ� �����ų� �� ��쿡 ������ ��ġ ����
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

        // _camera.position = _camerapos + _camera.rotation * _reverseDist; // <- camera�� ���� ��ġ
        transform.position = Vector3.Lerp(transform.position, _camerapos + _camera.rotation * _reverseDist, Time.deltaTime * 5f);

        // target�� ��ġ�� ������� camera�� ���� ����
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
