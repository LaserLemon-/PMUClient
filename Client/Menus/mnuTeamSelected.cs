﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;

namespace Client.Logic.Menus
{
    class mnuTeamSelected : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }
        int teamSlot;
        
        Label lblSwitch;
        Label lblSendHome;

        Widgets.MenuItemPicker itemPicker;
        const int MAX_ITEMS = 1;

        public int TeamSlot
        {
            get { return teamSlot; }
            set
            {
                teamSlot = value;
            }
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public mnuTeamSelected(string name, int teamSlot)
            : base(name) {

            base.Size = new Size(165, 95);
            base.MenuDirection = Enums.MenuDirection.Horizontal;
            base.Location = new Point(300, 34);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 23);

            

            lblSwitch = new Label("lblSwitch");
            lblSwitch.Font = FontManager.LoadFont("PMU", 32);
            lblSwitch.AutoSize = true;
            lblSwitch.Text = "Switch";
            lblSwitch.Location = new Point(30, 08);
            lblSwitch.HoverColor = Color.Red;
            lblSwitch.ForeColor = Color.WhiteSmoke;
            lblSwitch.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblSwitch_Click);

            lblSendHome = new Label("lblSendHome");
            lblSendHome.Font = FontManager.LoadFont("PMU", 32);
            lblSendHome.AutoSize = true;
            lblSendHome.Text = "Send Home";
            lblSendHome.Location = new Point(30, 38);
            lblSendHome.HoverColor = Color.Red;
            lblSendHome.ForeColor = Color.WhiteSmoke;
            lblSendHome.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblSendHome_Click);

            
            this.AddWidget(lblSwitch);
            this.AddWidget(lblSendHome);

            this.AddWidget(itemPicker);

            this.TeamSlot = teamSlot;
        }


        

        void lblSwitch_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(1, teamSlot);
        }

        void lblSendHome_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(2, teamSlot);
        }

        public void ChangeSelected(int itemNum)
        {
            itemPicker.Location = new Point(18, 23 + (30 * itemNum));
            itemPicker.SelectedItem = itemNum;
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e)
        {
            base.OnKeyboardDown(e);
            switch (e.Key)
            {
                case SdlDotNet.Input.Key.DownArrow:
                    {
                        if (itemPicker.SelectedItem == MAX_ITEMS)
                        {
                            ChangeSelected(0);
                        }
                        else
                        {
                            ChangeSelected(itemPicker.SelectedItem + 1);
                        }
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow:
                    {
                        if (itemPicker.SelectedItem == 0)
                        {
                            ChangeSelected(MAX_ITEMS);
                        }
                        else
                        {
                            ChangeSelected(itemPicker.SelectedItem - 1);
                        }
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Return:
                    {
                        SelectItem(itemPicker.SelectedItem, teamSlot);
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace:
                    {
                        CloseMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        private void SelectItem(int itemNum, int teamSlot)
        {
            switch (itemNum)
            {
                //case 0:
                    //{ // Make Leader
                        //Players.PlayerManager.MyPlayer.LeaderSwap(teamSlot);
                        //CloseMenu();
                    //}
                    //break;
                case 0:
                    { // Switch
                        Players.PlayerManager.MyPlayer.CharSwap(teamSlot);
                        CloseMenu();
                    }
                    break;
                case 1:
                    { // Send Home
                        Players.PlayerManager.MyPlayer.SendHome(teamSlot);
                        CloseMenu();
                    }
                    break;
            }
        }

        private void CloseMenu()
        {
            Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(this);
            Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuTeam");
        }

    }
}
