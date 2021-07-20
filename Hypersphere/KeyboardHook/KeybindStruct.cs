using System;
using System.Collections.Generic;

namespace Hypersphere
{
    /// <резюме>
    /// Структура для представления привязки клавиш (клавиша + модификаторы)
    /// Когда структура keybind сравнивается с другой структурой keybind, равенство основывается на
    /// модификаторы и код виртуальной клавиши
    /// </summary>
    internal class KeybindStruct : IEquatable<KeybindStruct>
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields   



        #region Private_Fields

        #endregion Private_Fields



        #region Properties
        public readonly int VirtualKeyCode;
        public readonly List<ModifierKeys> Modifiers;
        public readonly Guid? Identifier;
        #endregion Properties



        #region Public_Methods
        public KeybindStruct(IEnumerable<ModifierKeys> modifiers, int virtualKeyCode, Guid? identifier = null)
        {
            this.VirtualKeyCode = virtualKeyCode;
            this.Modifiers = new List<ModifierKeys>(modifiers);
            this.Identifier = identifier;
        }
        public bool Equals(KeybindStruct other)
        {
            if (other == null)
            {
                return false;
            }
            if (this.VirtualKeyCode != other.VirtualKeyCode)
            {
                return false;
            }
            if (this.Modifiers.Count != other.Modifiers.Count)
            {
                return false;
            }
            
            foreach (var modifier in this.Modifiers)
            {
                if (!other.Modifiers.Contains(modifier))
                {
                    return false;
                }
            }

            return true;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((KeybindStruct)obj);
        }

        public override int GetHashCode()
        {
            var hash = 13;
            hash = (hash * 7) + VirtualKeyCode.GetHashCode();

            // Суммируем модификаторы отдельно, чтобы их порядок не влиял на окончательный хеш
            var modifiersHashSum = 0;
            foreach (var modifier in this.Modifiers)
            {
                modifiersHashSum += modifier.GetHashCode();
            }

            hash = (hash * 7) + modifiersHashSum;

            return hash;
        }
        #endregion Public_Methods



        #region Private_Methods

        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers
    }
}
