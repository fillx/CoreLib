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

        public override string ToString()
        {
            return Name;
        }

        public static implicit operator TEnum(SceneId<TEnum> id)
        {
            return id._value;
        }

        public static implicit operator SceneId<TEnum>(TEnum value)
        {
            return new SceneId<TEnum>(value);
        }
    }
}