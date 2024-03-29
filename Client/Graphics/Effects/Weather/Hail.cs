﻿using System;
using System.Collections.Generic;
using System.Text;

using Client.Logic.Graphics.Effects.Overlays;
using SdlDotNet.Graphics;
using System.Drawing;
using SdlDotNet.Graphics.Sprites;

namespace Client.Logic.Graphics.Effects.Weather
{
    class Hail : IWeather
    {

        #region Fields

        bool disposed;

        List<Hailstone> hailstones = new List<Hailstone>();

        #endregion Fields

        #region Constructors

        public Hail() {
            disposed = false;

            for (int i = 0; i < 50; i++) {
                hailstones.Add(new Hailstone());
            }
        }

        #endregion Constructors

        #region Properties

        public bool Disposed {
            get { return disposed; }
        }

        #endregion Properties

        #region Methods

        public void FreeResources() {
            disposed = true;
            for (int i = hailstones.Count - 1; i >= 0; i--) {
                hailstones[i].Dispose();
                hailstones.RemoveAt(i);
            }
        }

        public void Render(Renderers.RendererDestinationData destData, int tick) {
            for (int i = 0; i < hailstones.Count; i++) {
                hailstones[i].UpdateLocation();
                destData.Blit(hailstones[i], new Point(hailstones[i].X, hailstones[i].Y));
            }
        }

        #endregion Methods


        public Enums.Weather ID {
            get { return Enums.Weather.Hail; }
        }
    }
}
