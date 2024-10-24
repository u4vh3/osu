// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Screens.Play.HUD;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Bindables;
using osu.Game.Configuration;
using osu.Game.Localisation;

namespace osu.Game.Skinning
{
    public partial class LegacyKeyCounterDisplay : KeyCounterDisplay
    {
        [SettingSource(typeof(LegacyKeyCounterDisplayStrings), nameof(LegacyKeyCounterDisplayStrings.TopKeysPressColour), nameof(LegacyKeyCounterDisplayStrings.TopKeysPressColourDescription))]
        public BindableColour4 KeyPressColourTop { get; set; } = new BindableColour4(Colour4.FromHex(@"#ffde00"));

        [SettingSource(typeof(LegacyKeyCounterDisplayStrings), nameof(LegacyKeyCounterDisplayStrings.BottomKeysPressColour), nameof(LegacyKeyCounterDisplayStrings.BottomKeysPressColourDescription))]
        public BindableColour4 KeyPressColourBottom { get; set; } = new BindableColour4(Colour4.FromHex(@"#f8009e"));

        [SettingSource(typeof(LegacyKeyCounterDisplayStrings), nameof(LegacyKeyCounterDisplayStrings.Animation), nameof(LegacyKeyCounterDisplayStrings.AnimationDescription))]
        public Bindable<KeyCounterAnimation> Animation { get; } = new Bindable<KeyCounterAnimation>(KeyCounterAnimation.Shrink);

        [SettingSource(typeof(LegacyKeyCounterDisplayStrings), nameof(LegacyKeyCounterDisplayStrings.KeySpacing), nameof(LegacyKeyCounterDisplayStrings.KeySpacingDescription))]
        public BindableFloat KeySpacing { get; set; } = new BindableFloat(1.8f)
        {
            MinValue = 0,
            MaxValue = 100f,
            Precision = 0.2f
        };

        [SettingSource(typeof(LegacyKeyCounterDisplayStrings), nameof(LegacyKeyCounterDisplayStrings.CentreKeys), nameof(LegacyKeyCounterDisplayStrings.CentreKeysDescription))]
        public BindableBool CentreKeys { get; set; } = new BindableBool(false);

        protected override FillFlowContainer<KeyCounter> KeyFlow { get; }

        private readonly Sprite backgroundSprite;

        public LegacyKeyCounterDisplay()
        {
            AutoSizeAxes = Axes.Both;

            AddRange(new Drawable[]
            {
                backgroundSprite = new Sprite
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopLeft,
                    Scale = new Vector2(1.05f, 1),
                    Rotation = 90,
                },
                KeyFlow = new FillFlowContainer<KeyCounter>
                {
                    Direction = FillDirection.Vertical,
                    AutoSizeAxes = Axes.Both,
                },
            });
        }

        [Resolved]
        private ISkinSource source { get; set; } = null!;

        protected override void LoadComplete()
        {
            base.LoadComplete();

            KeyTextColor = source.GetConfig<SkinCustomColourLookup, Color4>(new SkinCustomColourLookup(SkinConfiguration.LegacySetting.InputOverlayText))?.Value ?? Color4.Black;

            Texture? backgroundTexture = source.GetTexture(@"inputoverlay-background");

            if (backgroundTexture != null)
                backgroundSprite.Texture = backgroundTexture;

            Texture? mainKeyTexture = source.GetTexture(@"inputoverlay-key");

            for (int i = 0; i < KeyFlow.Count; i++)
            {
                Texture? keyTexture = source.GetTexture($"inputoverlay-key-{i}");

                if (keyTexture == null && mainKeyTexture == null) continue;

                ((LegacyKeyCounter)KeyFlow[i]).KeySprite.Texture = keyTexture ?? mainKeyTexture;
            }

            for (int i = 0; i < KeyFlow.Count; ++i)
                ((LegacyKeyCounter)KeyFlow[i]).Animation.BindTo(Animation);

            KeyPressColourTop.BindValueChanged(colour =>
            {
                for (int i = 0; i < 2; ++i)
                    ((LegacyKeyCounter)KeyFlow[i]).ActiveColour = colour.NewValue;
            }, true);

            KeyPressColourBottom.BindValueChanged(colour =>
            {
                for (int i = 2; i < KeyFlow.Count; ++i)
                    ((LegacyKeyCounter)KeyFlow[i]).ActiveColour = colour.NewValue;
            }, true);

            KeySpacing.BindValueChanged(spacing => KeyFlow.Spacing = new Vector2(spacing.NewValue), true);

            CentreKeys.BindValueChanged(centreKeys =>
            {
                Anchor keyFlowAnchor = centreKeys.NewValue ? Anchor.Centre : Anchor.TopRight;
                KeyFlow.Anchor = keyFlowAnchor;
                KeyFlow.Origin = keyFlowAnchor;

                KeyFlow.Position = centreKeys.NewValue ? Vector2.Zero : new Vector2(-1.5f, 7);
            }, true);
        }

        protected override KeyCounter CreateCounter(InputTrigger trigger) => new LegacyKeyCounter(trigger)
        {
            TextColour = keyTextColor,
        };

        private Colour4 keyTextColor = Colour4.White;

        public Colour4 KeyTextColor
        {
            get => keyTextColor;
            set
            {
                if (value != keyTextColor)
                {
                    keyTextColor = value;
                    foreach (var child in KeyFlow.Cast<LegacyKeyCounter>())
                        child.TextColour = value;
                }
            }
        }
    }
}
