using System;

namespace CoreLib.SceneManagement
{
    [Serializable]
    public struct SceneId<TEnum> where TEnum : Enum
    {
        [UnityEngine.SerializeField] private readonly TEnum _value;

        public SceneId(TEnum value)
        {
            _value = value;
        }

        public string Name => _value.ToString();
        public TEnum Value => _value;

        public override string ToString() => Name;

        public static implicit operator TEnum(SceneId<TEnum> id) => id._value;
        public static implicit operator SceneId<TEnum>(TEnum value) => new SceneId<TEnum>(value);
    }
}