﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;
using Client.Logic.Network;

namespace Client.Logic.Menus {
    class mnuAddShop : Widgets.BorderedPanel, Core.IMenu {
        public bool Modal {
            get;
            set;
        }

        Label lblAddTile;
        NumericUpDown nudAmount;
        Label lblPrice;
        Button btnAccept;
        Button btnCancel;
        int price;

        public mnuAddShop(string name, int price)
            : base(name) {
                this.price = price;
            this.Size = new Size(250, 250);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = Client.Logic.Graphics.DrawingSupport.GetCenter(Windows.WindowSwitcher.GameWindow.MapViewer.Size, this.Size);

            lblAddTile = new Label("lblAddTile");
            lblAddTile.Location = new Point(25, 15);
            lblAddTile.AutoSize = false;
            lblAddTile.Size = new System.Drawing.Size(this.Width - lblAddTile.X * 2, 40);
            lblAddTile.Text = "Enter the price you want to sell items on this tile for:";
            lblAddTile.ForeColor = Color.WhiteSmoke;

            nudAmount = new NumericUpDown("nudAmount");
            nudAmount.Size = new Size(120, 24);
            nudAmount.Location = new Point(lblAddTile.X, lblAddTile.Y + lblAddTile.Height + 10);
            nudAmount.Maximum = 1000000;
            nudAmount.Minimum = 1;
            nudAmount.ValueChanged +=new EventHandler<ValueChangedEventArgs>(nudAmount_ValueChanged);

            lblPrice = new Label("lblPrice");
            lblPrice.Location = new Point(lblAddTile.X, nudAmount.Y + nudAmount.Height + 10);
            lblPrice.AutoSize = false;
            lblPrice.Size = new System.Drawing.Size(120, 40);
            lblPrice.Text = "Placing this tile will cost " + (nudAmount.Value / price) + " " + Items.ItemHelper.Items[1].Name + ".";
            lblPrice.ForeColor = Color.WhiteSmoke;

            btnAccept = new Button("btnAccept");
            btnAccept.Location = new Point(lblAddTile.X, lblPrice.Y + lblPrice.Height + 10);
            btnAccept.Size = new Size(80, 30);
            btnAccept.Text = "Place Shop";
            btnAccept.Font = FontManager.LoadFont("tahoma", 10);
            Skins.SkinManager.LoadButtonGui(btnAccept);
            btnAccept.Click += new EventHandler<MouseButtonEventArgs>(btnAccept_Click);
            
            btnCancel = new Button("btnCancel");
            btnCancel.Location = new Point(btnAccept.X + btnAccept.Width, lblPrice.Y + lblPrice.Height + 10);
            btnCancel.Size = new Size(80, 30);
            btnCancel.Text = "Cancel";
            btnCancel.Font = FontManager.LoadFont("tahoma", 10);
            Skins.SkinManager.LoadButtonGui(btnCancel);
            btnCancel.Click += new EventHandler<MouseButtonEventArgs>(btnCancel_Click);

            this.AddWidget(lblAddTile);
            this.AddWidget(nudAmount);
            this.AddWidget(lblPrice);
            this.AddWidget(btnAccept);
            this.AddWidget(btnCancel);
        }

        void nudAmount_ValueChanged(object sender, ValueChangedEventArgs e) {

            lblPrice.Text = "Placing this tile will cost "+(nudAmount.Value/price)+" " + Items.ItemHelper.Items[1].Name + ".  Tiles are re-usable.";
        }

        void btnAccept_Click(object sender, MouseButtonEventArgs e) {
            Messenger.SendAddShopRequest(nudAmount.Value);
            MenuSwitcher.CloseAllMenus();
            Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
        }

        void btnCancel_Click(object sender, MouseButtonEventArgs e) {
            MenuSwitcher.CloseAllMenus();
            Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }
    }
}
