using UnityEngine;
using UnityEngine.UI;

namespace MVVM.Binders.Common
{
    public class ColorToImageColorBinder : ObservableBinder<Color>
    {
        [SerializeField] private Image _img;

        protected override Color HandleValue(Color value)
        {
            _img.color = value;
            return value;
        }

#if UNITY_EDITOR
        private void Reset()
        {
            _img = GetComponent<Image>();
        }
#endif
    }
}