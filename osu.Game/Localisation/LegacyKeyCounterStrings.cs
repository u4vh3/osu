// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;

namespace osu.Game.Localisation
{
    public static class LegacyKeyCounterStrings
    {
        private const string prefix = @"osu.Game.Resources.Localisation.LegacyKeyCounter";

        /// <summary>
        /// "No animation"
        /// </summary>
        public static LocalisableString NoAnimation => new TranslatableString(getKey(@"no_animation"), @"No animation");

        /// <summary>
        /// "Shrink"
        /// </summary>
        public static LocalisableString Shrink => new TranslatableString(getKey(@"shrink"), @"Shrink");

        /// <summary>
        /// "Expand"
        /// </summary>
        public static LocalisableString Expand => new TranslatableString(getKey(@"expand"), @"Expand");

        /// <summary>
        /// "Move up"
        /// </summary>
        public static LocalisableString MoveUp => new TranslatableString(getKey(@"move_up"), @"Move up");

        /// <summary>
        /// "Move right"
        /// </summary>
        public static LocalisableString MoveRight => new TranslatableString(getKey(@"move_right"), @"Move right");

        /// <summary>
        /// "Move down"
        /// </summary>
        public static LocalisableString MoveDown => new TranslatableString(getKey(@"move_down"), @"Move down");

        /// <summary>
        /// "Move left"
        /// </summary>
        public static LocalisableString MoveLeft => new TranslatableString(getKey(@"move_left"), @"Move left");

        private static string getKey(string key) => $@"{prefix}:{key}";
    }
}
