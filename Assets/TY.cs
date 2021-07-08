using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class TY : MonoBehaviour
{
    TextMeshPro _textMesh;
     string[] _textCharacter;
    public bool _isActive;
    public float _timeInSeconds;
    float _timer;
    int _charCount;
    // Start is called before the first frame update
    void Start()
    {
        _textMesh = GetComponent<TextMeshPro>();
        _textCharacter = new string[_textMesh.text.Length];
        for (int i = 0; i < _textMesh.text.Length; i++)
        {
            _textCharacter[i] = _textMesh.text.Substring(i, 1);
        }
        _textMesh.text = "";
        _charCount = 0;
        _timer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            if (_charCount < _textCharacter.Length)
            { _timer += Time.deltaTime;
                if (_timer >= _timeInSeconds)
                {
                    _textMesh.text += _textCharacter[_charCount];
                    _charCount++;
                    _timer = 0;
                }
            }
        }

        if (_charCount == _textCharacter.Length)
        {
            if (transform.childCount > 0)
            {
                transform.GetChild(0).GetComponent<TY>()._isActive =
                    true;
                _charCount++;
            }
        }
    }
}
