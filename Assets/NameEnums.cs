using System;

namespace uPalette.Generated
{
public enum ColorTheme
    {
        Default,
        MintPastel,
    }

    public static class ColorThemeExtensions
    {
        public static string ToThemeId(this ColorTheme theme)
        {
            switch (theme)
            {
                case ColorTheme.Default:
                    return "2e0ed1df-c682-4843-aff2-10b1d5949dbd";
                case ColorTheme.MintPastel:
                    return "5b1a6eea-114a-42c1-af4b-c6d97f729865";
                default:
                    throw new ArgumentOutOfRangeException(nameof(theme), theme, null);
            }
        }
    }

    public enum ColorEntry
    {
        General_BackgroundFill,
        General_LabelText,
        General_OverlayFill,
        General_ButtonText,
        General_ButtonFill,
        Board_BoardFill,
        Board_CellFill,
        Score_LabelText,
        Score_ValueText,
        Score_BackgroundFill,
        Tile_LabelText,
        Tile_2Text,
        Tile_2,
        Tile_4,
        Tile_8,
        Tile_16,
        Tile_32,
        Tile_64,
        Tile_128,
        Tile_256,
        Tile_512,
        Tile_1024,
        Tile_2048,
        Tile_4096,
        Tile_8192,
        Tile_16384,
        Tile_32768,
        Tile_65536,
        Tile_131072,
        Tile_Shadow,
    }

    public static class ColorEntryExtensions
    {
        public static string ToEntryId(this ColorEntry entry)
        {
            switch (entry)
            {
                case ColorEntry.General_BackgroundFill:
                    return "7142aaa1-e84d-48d8-9442-5e63cd758323";
                case ColorEntry.General_LabelText:
                    return "b506c046-c093-4c3a-a302-3ce4966c4fd4";
                case ColorEntry.General_OverlayFill:
                    return "7e156ee8-5d2b-4d3f-a99b-1481ea8f7117";
                case ColorEntry.General_ButtonText:
                    return "a0c2a6fa-f1b2-437b-b352-1d2bbf5a9d9a";
                case ColorEntry.General_ButtonFill:
                    return "325d0446-556a-4f22-b21a-74eff279dd27";
                case ColorEntry.Board_BoardFill:
                    return "539ffe1b-e51b-4886-8e52-b991e087d5de";
                case ColorEntry.Board_CellFill:
                    return "0e1dcb02-def3-42cc-9b45-dcecc9d8504f";
                case ColorEntry.Score_LabelText:
                    return "8538d10d-094e-414f-960e-37239aa22078";
                case ColorEntry.Score_ValueText:
                    return "905a2713-50e3-4099-950e-affd7781d328";
                case ColorEntry.Score_BackgroundFill:
                    return "5dde880d-f0f3-41ab-acde-f8c16370aeb6";
                case ColorEntry.Tile_LabelText:
                    return "8745f601-fc05-45e3-8095-ae07b4f09c5b";
                case ColorEntry.Tile_2Text:
                    return "4c4d3d48-9f77-44b0-bf91-b9a3f23bdf45";
                case ColorEntry.Tile_2:
                    return "62c12acb-6441-4d0b-9b5e-7955fa5a114a";
                case ColorEntry.Tile_4:
                    return "e7434a70-1888-4a16-8f4d-c786816e2d5c";
                case ColorEntry.Tile_8:
                    return "122cbb7b-e1e5-430c-90d7-d0e73b6184b6";
                case ColorEntry.Tile_16:
                    return "79c78c12-1450-40a2-94a4-b70e20e3ab17";
                case ColorEntry.Tile_32:
                    return "f938fe48-7cce-4cad-ace6-2dd68135390d";
                case ColorEntry.Tile_64:
                    return "25e713b7-e19e-4ff1-969f-05369e4138c8";
                case ColorEntry.Tile_128:
                    return "96976bac-51ac-405e-b82e-48070affeae1";
                case ColorEntry.Tile_256:
                    return "4f8f26fa-065b-4a06-bfaf-6c36492a05b7";
                case ColorEntry.Tile_512:
                    return "7ccdee7d-5935-4bbf-98a9-33145a62668d";
                case ColorEntry.Tile_1024:
                    return "93cb64c8-386a-4a7d-ac16-de1359eaba36";
                case ColorEntry.Tile_2048:
                    return "f8178d7d-a18a-45ac-a0e3-7b3090f383b4";
                case ColorEntry.Tile_4096:
                    return "e7ec7dee-1a3b-4c1a-88ec-6cb279629c89";
                case ColorEntry.Tile_8192:
                    return "73e61600-ca26-4dcc-9e06-cc4351f8e7b7";
                case ColorEntry.Tile_16384:
                    return "fe565b7c-3f28-4dfd-b9b9-c77619412db9";
                case ColorEntry.Tile_32768:
                    return "f4718865-8fab-4c79-aaf3-fa4a397c8c0e";
                case ColorEntry.Tile_65536:
                    return "34839df2-3c20-4644-8b16-9e420c6c4c3a";
                case ColorEntry.Tile_131072:
                    return "a482929a-a122-4787-acb7-637fe58f95a5";
                case ColorEntry.Tile_Shadow:
                    return "767c4763-20f8-4f35-b2a5-a63280286446";
                default:
                    throw new ArgumentOutOfRangeException(nameof(entry), entry, null);
            }
        }
    }

    public enum GradientTheme
    {
        Default,
    }

    public static class GradientThemeExtensions
    {
        public static string ToThemeId(this GradientTheme theme)
        {
            switch (theme)
            {
                case GradientTheme.Default:
                    return "8eb1adb6-dac0-43b6-8d2e-17c257a12c78";
                default:
                    throw new ArgumentOutOfRangeException(nameof(theme), theme, null);
            }
        }
    }

    public enum GradientEntry
    {
    }

    public static class GradientEntryExtensions
    {
        public static string ToEntryId(this GradientEntry entry)
        {
            switch (entry)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(entry), entry, null);
            }
        }
    }

    public enum CharacterStyleTheme
    {
        Default,
    }

    public static class CharacterStyleThemeExtensions
    {
        public static string ToThemeId(this CharacterStyleTheme theme)
        {
            switch (theme)
            {
                case CharacterStyleTheme.Default:
                    return "3f002683-a4e6-410e-b275-09c3c81520f3";
                default:
                    throw new ArgumentOutOfRangeException(nameof(theme), theme, null);
            }
        }
    }

    public enum CharacterStyleEntry
    {
    }

    public static class CharacterStyleEntryExtensions
    {
        public static string ToEntryId(this CharacterStyleEntry entry)
        {
            switch (entry)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(entry), entry, null);
            }
        }
    }

    public enum CharacterStyleTMPTheme
    {
        Default,
    }

    public static class CharacterStyleTMPThemeExtensions
    {
        public static string ToThemeId(this CharacterStyleTMPTheme theme)
        {
            switch (theme)
            {
                case CharacterStyleTMPTheme.Default:
                    return "6381ceff-f68f-4bc0-aa3b-25e07c5ea598";
                default:
                    throw new ArgumentOutOfRangeException(nameof(theme), theme, null);
            }
        }
    }

    public enum CharacterStyleTMPEntry
    {
    }

    public static class CharacterStyleTMPEntryExtensions
    {
        public static string ToEntryId(this CharacterStyleTMPEntry entry)
        {
            switch (entry)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(entry), entry, null);
            }
        }
    }
}
