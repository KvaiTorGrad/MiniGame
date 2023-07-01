using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Gallery : ScriptableObject
{
    [SerializeField] private List<Texture2D> _textures;
    private Sprite _selectSprite;
    public Sprite SelectSprite { get => _selectSprite; set => _selectSprite = value; }
    public List<Texture2D> Textures { get => _textures; set => _textures = value; }
}
