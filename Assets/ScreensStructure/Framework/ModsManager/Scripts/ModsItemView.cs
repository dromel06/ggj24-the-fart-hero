using UnityEngine;
using UnityEngine.UI;

namespace GodFramework
{
    public class ModsItemView : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Text _title;
        [SerializeField] private InputField _input;
        [SerializeField] private Toggle _toggle;
        #endregion

        #region Properties
        public string Value => _input.text;
        public bool Enabled => _toggle.isOn;
        #endregion

        #region Public Methods
        public void Initialize(ModData modData)
        {
            this.name = modData.Id.ToString().SplitCamelCase();

            _title.text = name;
            _input.text = modData.Value;
            _toggle.isOn = modData.Enabled;
        }

        public void Clear()
        {
            _title.text = "";
            _input.text = "";
            _toggle.isOn = false;
        }
        #endregion
    }
}