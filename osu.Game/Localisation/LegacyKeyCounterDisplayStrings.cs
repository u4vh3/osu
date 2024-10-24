// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;

namespace osu.Game.Localisation
{
    public static class LegacyKeyCounterDisplayStrings
    {
        private const string prefix = @"osu.Game.Resources.Localisation.LegacyKeyCounterDisplay";

        /// <summary>
        /// "Animation"
        /// </summary>
        public static LocalisableString Animation => new TranslatableString(getKey(@"animation"), @"Animation");

        /// <summary>
        /// "Animation of keys when pressed."
        /// </summary>
        public static LocalisableString AnimationDescription => new TranslatableString(getKey(@"animation_description"), @"Animation of keys when pressed.");

        /// <summary>
        /// "Key Spacing"
        /// </summary>
        public static LocalisableString KeySpacing => new TranslatableString(getKey(@"key_spacing"), @"Key Spacing");

        /// <summary>
        /// "Sets spacing between keys."
        /// </summary>
        public static LocalisableString KeySpacingDescription => new TranslatableString(getKey(@"key_spacing_description"), @"Sets spacing between keys.");

        /// <summary>
        /// "Centre keys"
        /// </summary>
        public static LocalisableString CentreKeys => new TranslatableString(getKey(@"centre_keys"), @"Centre keys");

        /// <summary>
        /// "Places keys at the centre of the component."
        /// </summary>
        public static LocalisableString CentreKeysDescription => new TranslatableString(getKey(@"centre_keys_description"), @"Places keys at the centre of the component.");

        /// <summary>
        /// "Top keys press colour"
        /// </summary>
        public static LocalisableString TopKeysPressColour => new TranslatableString(getKey(@"top_keys_press_colour"), @"Top keys press colour");

        /// <summary>
        /// "The colour of first two keys when pressed."
        /// </summary>
        public static LocalisableString TopKeysPressColourDescription => new TranslatableString(getKey(@"top_keys_press_colour_description"), @"The colour of first two keys when pressed.");

        /// <summary>
        /// "Bottom keys press colour"
        /// </summary>
        public static LocalisableString BottomKeysPressColour => new TranslatableString(getKey(@"bottom_keys_press_colour"), @"Bottom keys press colour");

        /// <summary>
        /// "The colour of bottom keys when pressed."
        /// </summary>
        public static LocalisableString BottomKeysPressColourDescription => new TranslatableString(getKey(@"bottom_keys_press_colour_description"), @"The colour of bottom keys when pressed.");

        private static string getKey(string key) => $@"{prefix}:{key}";
    }
}
