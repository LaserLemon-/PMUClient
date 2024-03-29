﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SdlDotNet.Widgets;

namespace Client.Logic.Stories.Components
{
    class OptionSelectionMenu : Widgets.BorderedPanel, Menus.Core.IMenu
    {
        Label[] lblOptions;

        public delegate void OptionSelectedDelegate(string option);
        public event OptionSelectedDelegate OptionSelected;

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public bool Modal {
            get;
            set;
        }

        public OptionSelectionMenu(string name, Size storyBounds, string[] options)
            : base(name) {

            lblOptions = new Label[options.Length];

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(0, 15);

            int maxWidth = 140;

            for (int i = 0; i < options.Length; i++) {
                lblOptions[i] = new Label("lblOptions" + i);
                lblOptions[i].Font = Graphics.FontManager.LoadFont("PMU", 24);
                lblOptions[i].Location = new Point(15, i * 26);
                lblOptions[i].AutoSize = true;
                lblOptions[i].ForeColor = Color.WhiteSmoke;
                lblOptions[i].Text = options[i];

                if (lblOptions[i].Width > maxWidth) {
                    maxWidth = lblOptions[i].Width;
                }

                this.WidgetPanel.AddWidget(lblOptions[i]);
            }

            base.Size = new System.Drawing.Size(maxWidth + 30, options.Length * 26 + 20);
            base.Location = new Point(storyBounds.Width - this.Width - 20, storyBounds.Height - this.Height - 110);
            //lblText = new Label("lblText");
            //lblText.BackColor = Color.Transparent;
            //lblText.Font = Graphics.FontManager.LoadFont("unown", 36);
            //lblText.Location = new Point(15, 10);
            //lblText.Size = new System.Drawing.Size(this.Size.Width - lblText.Location.X, this.Size.Height - lblText.Location.Y);

            //picSpeaker = new PictureBox("picSpeaker");
            //picSpeaker.Size = new Size(40, 40);
            //picSpeaker.Location = new Point(10, DrawingSupport.GetCenter(WidgetPanel.Height, 40));
            //picSpeaker.BorderWidth = 1;
            //picSpeaker.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;

            //this.WidgetPanel.AddWidget(lblText);
            //this.WidgetPanel.AddWidget(picSpeaker);

            this.WidgetPanel.AddWidget(itemPicker);
        }
        Widgets.MenuItemPicker itemPicker;

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(0, 15 + (22 * itemNum));
            itemPicker.SelectedItem = itemNum;
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.DownArrow: {
                        if (itemPicker.SelectedItem == lblOptions.Length - 1) {
                            ChangeSelected(0);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem + 1);
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow: {
                        if (itemPicker.SelectedItem == 0) {
                            ChangeSelected(lblOptions.Length - 1);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem - 1);
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.Return: {
                        SelectOption(itemPicker.SelectedItem);
                    }
                    break;
            }
        }

        private void SelectOption(int optionSlot) {
            if (OptionSelected != null)
                OptionSelected(lblOptions[optionSlot].Text);
        }

        public override void Close() {
            base.Close();
            if (this.ParentContainer != null) {
                this.ParentContainer.RemoveWidget(this.GroupedWidget.Name);
            }
        }
    }
}
