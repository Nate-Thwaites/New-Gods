using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityEngine.InputSystem.Samples.RebindUI
{
    
    public class RebindSaveLoad : MonoBehaviour
    {
        [Tooltip("The associated input action asset to be serialized to player preferences (Required).")]
        public InputActionAsset actions;

        [Tooltip("The player preference key to be used when serializing binding overrides to player preferences (Required).")]
        public string playerPreferenceKey;

        [Tooltip("Specifies whether to load and apply binding overrides when the component is enabled")]
        public bool loadOnEnable = true;

        [Tooltip("Specifies whether to save binding overrides when the component is disabled")]
        public bool saveOnDisable = true;

       
        public void Load()
        {
            if (!IsValidConfiguration())
                return;

            var rebinds = PlayerPrefs.GetString(playerPreferenceKey);
            if (string.IsNullOrEmpty(rebinds))
                return; 

            actions.LoadBindingOverridesFromJson(rebinds);
        }

       
        public void Save()
        {
            if (!IsValidConfiguration())
                return;

            var rebinds = actions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString(playerPreferenceKey, rebinds);
        }

        private void OnEnable()
        {
            if (loadOnEnable)
                Load();
        }

        private void OnDisable()
        {
            if (saveOnDisable)
                Save();
        }

        private bool IsValidConfiguration()
        {
            if (actions == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(playerPreferenceKey))
            {
                return false;
            }

            return true;
        }
    }
}
