using System;

namespace RocketFramework.Mods
{
    public class ModsItemModel
    {
        #region Fields
        private string _title;
        private string _value;
        private bool _state;
        private Action<string> _onAction;
        #endregion

        #region Constructor
        public ModsItemModel(string title, string value, bool state, Action<string> action)
        {
            _title = title;
            _value = value;
            _state = state;
            _onAction += action;
        }
        #endregion

        #region Public Methods
        public void Invoke()
        {
            if (_state)
            {
                _onAction.Invoke(_value);
            }
        }

        public void AddListener(Action<string> onAction)
        {
            _onAction += onAction;
        }

        public void RemoveListener(Action<string> onAction)
        {
            _onAction -= onAction;
        }

        public void RemoveAllListener()
        {
            foreach (Delegate item in _onAction.GetInvocationList())
            {
                _onAction -= (Action<string>)item;
            }
        }
        #endregion
    }
}