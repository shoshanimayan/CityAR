using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ARObjects
{
    public class ARBuilding : BaseARObject
    {

        ///  INSPECTOR VARIABLES       ///
        [Tooltip("either use a random material from the Material Array to change the objects color, or generate totally random color if false")]
        [SerializeField] bool _useRandomMaterialFromMaterialArray;


        [Tooltip("array of materials to randomly select material for ArBuilding")]
        [SerializeField] Material[] _materialArray;


        [Header("Randomly generated color ranges")]

        [SerializeField][Range(0f, 1f)] private float _hueMin = .25f;
        [SerializeField][Range(0f, 1f)] private float _hueMax = 1f;
        [SerializeField][Range(0f, 1f)] private float _saturationMin = .75f;
        [SerializeField][Range(0f, 1f)] private float _saturationMax = 1f;
        [SerializeField][Range(0f, 1f)] private float _valueMin = .25f;
        [SerializeField][Range(0f, 1f)] private float _valueMax = 1f;

        [Header("Randomly generated dimension ranges")]
        [SerializeField] private float _xMin = 1f;
        [SerializeField] private float _xMax = 1.1f;
        [SerializeField] private float _yMin = 1.5f;
        [SerializeField] private float _yMax = 2f;
        [SerializeField] private float _zMin = 1f;
        [SerializeField] private float _zMax = 1.1f;

        ///  PRIVATE VARIABLES         ///
        private MeshRenderer _meshRenderer;
        ///  PRIVATE METHODS           ///

        private void OnValidate()
        {
            if (_hueMin > _hueMax)
            { 
                _hueMin = _hueMax;
            }

            if (_saturationMin > _saturationMax)
            { 
                _saturationMin = _saturationMax;
            }

            if (_valueMin > _valueMax)
            {
                _valueMin = _valueMax;
            }

            if (_xMin > _xMax)
            { 
                _xMin = _xMax;
            }

            if (_yMin > _yMax)
            {
                _yMin = _yMax;
            }

            if (_zMin>_zMax)
            {
                _zMin = _zMax;
            }

        }

        private void Awake()
        {
            MeshRenderer _meshRenderer = GetComponent<MeshRenderer>();

            if (!_useRandomMaterialFromMaterialArray || _materialArray.Length==0)
            {
                MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
                propertyBlock.SetColor("_BaseColor", Random.ColorHSV(_hueMin,_hueMax,_saturationMin,_saturationMax,_valueMin,_valueMax));
                _meshRenderer.SetPropertyBlock(propertyBlock);
            }
            else
            {
                List<Material> newMaterials = new List<Material>();
                int length = _meshRenderer.materials.Length;
                for (int i = 0; i < length; i++)
                {
                    newMaterials.Add(_materialArray[Random.Range(0, _materialArray.Length)]); 
                }
                _meshRenderer.SetMaterials(newMaterials);

            }

            float randomX= Random.Range(_xMin, _xMax);
            float randomY= Random.Range(_yMin, _yMax);
            float randomZ= Random.Range(_zMin, _zMax);
            Debug.Log(new Vector3(randomX, randomY, randomZ));
            transform.localScale= new Vector3(randomX,randomY,randomZ);



        }






    }
}
