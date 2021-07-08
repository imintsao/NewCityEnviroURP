using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshPro))]
public class TypeWriter : MonoBehaviour
{
    TextMeshPro _textMesh;
    public string[] _textCharacter;
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



        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
