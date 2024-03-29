﻿using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;
using System.Drawing;

namespace Client.Logic.ExpKit.Modules
{
    class kitDebug : Panel, IKitModule
    {
        int moduleIndex;
        Timer tmrUpdate;
        Label lblFps;
        Button btnTest;
        bool enabled;

        public kitDebug(string name)
            : base(name) {
            enabled = true;

            base.BackColor = Color.Transparent;

            tmrUpdate = new Timer("tmrUpdate");
            tmrUpdate.Interval = 500;
            tmrUpdate.Elapsed += new EventHandler(tmrUpdate_Elapsed);

            lblFps = new Label("lblFps");
            lblFps.Location = new Point(0, 0);
            lblFps.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblFps.Text = "FPS";
            lblFps.AutoSize = true;

            btnTest = new Button("btnTest");
            btnTest.Location = new Point(0, 40);
            btnTest.Size = new Size(40, 20);
            btnTest.Text = "Test!";
            Skins.SkinManager.LoadButtonGui(btnTest);
            btnTest.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnTest_Click);

            this.AddWidget(tmrUpdate);
            this.AddWidget(lblFps);
            this.AddWidget(btnTest);
        }

        void btnTest_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            lblFps.Text = "Testing!";
        }

        void tmrUpdate_Elapsed(object sender, EventArgs e) {
            lblFps.Text = "FPS: " + SdlDotNet.Core.Events.Fps.ToString();
        }

        public void SwitchOut() {
            tmrUpdate.Stop();
        }

        public void Initialize(Size containerSize) {
            tmrUpdate.Start();
        }

        public int ModuleIndex {
            get { return moduleIndex; }
        }

        public string ModuleName {
            get { return "Debug Module"; }
        }

        public void Created(int index) {
            moduleIndex = index;
        }

        public Panel ModulePanel {
            get { return this; }
        }


        public bool Enabled {
            get { return enabled; }
            set {
                enabled = value;
                if (EnabledChanged != null)
                    EnabledChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler EnabledChanged;


        public Enums.ExpKitModules ModuleID {
            get { return Enums.ExpKitModules.Debug; }
        }
    }
}
