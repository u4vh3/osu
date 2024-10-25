// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Screens.Play.HUD;
using osu.Game.Localisation;
using osuTK;

namespace osu.Game.Skinning
{
    public partial class LegacyKeyCounter : KeyCounter
    {
        private const float transition_duration = 160;

        public Bindable<KeyCounterAnimation> Animation { get; set; } = new Bindable<KeyCounterAnimation>();

        public Colour4 ActiveColour { get; set; }

        private Colour4 textColour;

        public Colour4 TextColour
        {
            get => textColour;
            set
            {
                textColour = value;
                overlayKeyText.Colour = value;
            }
        }

        private readonly Container keyContainer;
        private readonly OsuSpriteText overlayKeyText;
        public readonly Sprite KeySprite;

        public LegacyKeyCounter(InputTrigger trigger)
            : base(trigger)
        {
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            Child = keyContainer = new Container
            {
                AutoSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Children = new Drawable[]
                {
                    KeySprite = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                    new UprightAspectMaintainingContainer
                    {
                        AutoSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Child = overlayKeyText = new OsuSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = trigger.Name,
                            Colour = textColour,
                            Font = OsuFont.GetFont(weight: FontWeight.SemiBold),
                        },
                    },
                }
            };

            // matches longest dimension of default skin asset
            Height = Width = 46;
        }

        [BackgroundDependencyLoader]
        private void load(ISkinSource source)
        {
            Animation.BindValueChanged(_ => deactivateKeyContainer(keyContainer));
        }

        protected override void Activate(bool forwardPlayback = true)
        {
            base.Activate(forwardPlayback);
            activateKeyContainer(keyContainer, Animation.Value);
            KeySprite.Colour = ActiveColour;
            overlayKeyText.Text = CountPresses.Value.ToString();
            overlayKeyText.Font = overlayKeyText.Font.With(weight: FontWeight.SemiBold);
        }

        protected override void Deactivate(bool forwardPlayback = true)
        {
            base.Deactivate(forwardPlayback);
            deactivateKeyContainer(keyContainer);
            KeySprite.Colour = Colour4.White;
        }

        private void deactivateKeyContainer(Container target)
        {
            keyContainer.MoveTo(Vector2.Zero, transition_duration, Easing.Out);
            keyContainer.ScaleTo(1f, transition_duration, Easing.Out);
        }

        private void activateKeyContainer(Container target, KeyCounterAnimation animation)
        {
            switch (animation)
            {
                case KeyCounterAnimation.NoAnimation:
                    return;

                case KeyCounterAnimation.Shrink:
                    target.ScaleTo(0.75f, transition_duration, Easing.Out);
                    return;

                case KeyCounterAnimation.Expand:
                    target.ScaleTo(1.25f, transition_duration, Easing.Out);
                    return;

                case KeyCounterAnimation.MoveUp:
                    target.MoveToY(-20f, transition_duration, Easing.Out);
                    return;

                case KeyCounterAnimation.MoveRight:
                    target.MoveToX(20f, transition_duration, Easing.Out);
                    return;

                case KeyCounterAnimation.MoveDown:
                    target.MoveToY(20f, transition_duration, Easing.Out);
                    return;

                case KeyCounterAnimation.MoveLeft:
                    target.MoveToX(-20f, transition_duration, Easing.Out);
                    return;
            }
        }
    }

    public enum KeyCounterAnimation
    {
        [LocalisableDescription(typeof(LegacyKeyCounterStrings), nameof(LegacyKeyCounterStrings.NoAnimation))]
        NoAnimation,

        [LocalisableDescription(typeof(LegacyKeyCounterStrings), nameof(LegacyKeyCounterStrings.Shrink))]
        Shrink,

        [LocalisableDescription(typeof(LegacyKeyCounterStrings), nameof(LegacyKeyCounterStrings.Expand))]
        Expand,

        [LocalisableDescription(typeof(LegacyKeyCounterStrings), nameof(LegacyKeyCounterStrings.MoveUp))]
        MoveUp,

        [LocalisableDescription(typeof(LegacyKeyCounterStrings), nameof(LegacyKeyCounterStrings.MoveRight))]
        MoveRight,

        [LocalisableDescription(typeof(LegacyKeyCounterStrings), nameof(LegacyKeyCounterStrings.MoveDown))]
        MoveDown,

        [LocalisableDescription(typeof(LegacyKeyCounterStrings), nameof(LegacyKeyCounterStrings.MoveLeft))]
        MoveLeft,
    }
}
