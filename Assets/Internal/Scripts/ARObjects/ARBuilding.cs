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

        [SerializeField] private float _hueMin = .25f;
        [SerializeField] private float _hueMax = 1f;
        [SerializeField] private float _saturationMin = .75f;
        [SerializeField] private float _saturationMax = 1f;
        [SerializeField] private float _valueMin = .25f;
        [SerializeField] private float _valueMax = 1f;

        ///  PRIVATE VARIABLES         ///
        private MeshRenderer _meshRenderer;
        ///  PRIVATE METHODS           ///
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



        }






    }
}
