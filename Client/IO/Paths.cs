﻿namespace Client.Logic.IO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Paths
    {
        #region Fields

        /// <summary>
        /// Directory seperator character used by the OS.
        /// </summary>
        static char dirChar = System.IO.Path.DirectorySeparatorChar;
        static string fontPath;
        static string gfxPath;
        static string mapPath;
        static string musicPath;
        static string sfxPath;
        static string skinPath;
        static string startupPath;
        static string storyDataPath;

        #endregion Fields

        #region Properties

        public static char DirChar {
            get { return dirChar; }
        }

        public static string FontPath {
            get { return fontPath; }
        }

        public static string StoryDataPath {
            get { return storyDataPath; }
        }

        public static string GfxPath {
            get { return gfxPath; }
        }

        public static string MapPath {
            get { return mapPath; }
        }

        public static string MusicPath {
            get { return musicPath; }
        }

        public static string SfxPath {
            get { return sfxPath; }
        }

        public static string SkinPath {
            get { return skinPath; }
        }

        public static string StartupPath {
            get { return startupPath; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates a file path in the format used by the host OS.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>A file path in the format used by the host OS</returns>
        public static string CreateOSPath(string fileName) {
            if (Environment.OSVersion.Platform == PlatformID.Unix) {
                if (fileName.Contains("\\"))
                    fileName = fileName.Replace('\\', dirChar);
            } else if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                if (fileName.Contains("/"))
                    fileName = fileName.Replace('/', dirChar);
            }
            if (fileName.StartsWith(StartupPath) == false) {
                fileName = StartupPath + fileName;
            }
            return fileName;
        }

        /// <summary>
        /// Initializes this class
        /// </summary>
        public static void Initialize() {
            Paths.startupPath = System.Windows.Forms.Application.StartupPath;
            //#if DEBUG
            if (/*Globals.InDebugMode &&*/ Globals.CommandLine.ContainsCommandArg("-overridepath")) {
                int index = Globals.CommandLine.FindCommandArg("-overridepath");
                Paths.startupPath = Globals.CommandLine.CommandArgs[index + 1];
            }
            //#endif
            Paths.startupPath = System.IO.Path.GetFullPath(Paths.startupPath);
            if (Paths.startupPath.EndsWith(dirChar.ToString()) == false)
                Paths.startupPath += dirChar;

            Paths.gfxPath = Paths.StartupPath + "GFX" + dirChar;
            Paths.skinPath = Paths.StartupPath + "Skins" + dirChar;
            Paths.fontPath = Paths.StartupPath + "Fonts" + dirChar;
            Paths.mapPath = Paths.StartupPath + "MapData" + dirChar;
            Paths.musicPath = Paths.StartupPath + "Music" + dirChar;
            Paths.sfxPath = Paths.StartupPath + "SFX" + dirChar;
            Paths.storyDataPath = Paths.StartupPath + "Story" + dirChar;
        }

        #endregion Methods
    }
}