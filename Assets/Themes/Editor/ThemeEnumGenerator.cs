namespace uPalette.Editor.Core
{
    using UnityEngine;
    using UnityEditor;
    using uPalette.Runtime.Core;
    using Shared;

    /// <summary>
    /// This is an editor utility to patch over an issue in the uPalette package where lost focus of editor windows doesn't generate an enum.
    /// </summary>
    public class ThemeEnumGenerator : MonoBehaviour
    {
        [MenuItem("Window/uPalette/GenerateEnums")]
        public static void GeneratePaletteEnums()
        {
            var store = PaletteStore.Instance;
            var generator = new GenerateNameEnumsFileService(store);
            generator.Run();
        }
    }
}
