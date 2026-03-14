using System;
using UnityEngine;

namespace MVVM.Binders.Collections.Mappings
{
    [Serializable]
    public class ViewModelToViewDirectRefMapping
    {
        [SerializeField] private string _viewModelFullTypeName;
        [SerializeField] private View _prefab;
        
        public string ViewModelFullTypeName => _viewModelFullTypeName;
        public View Prefab => _prefab;
    }
}