﻿using System;
using System.Collections.Generic;

namespace Yasai.Resources
{
    /// <summary>
    /// Object representation of the manager.json. Maps a string to a list of paths.
    /// </summary>
    [Serializable]
    public class ContentManager
    {
        public Dictionary<string, List<string>> Groups;

        public ContentManager(Dictionary<string, List<string>> g) => Groups = g;

        public ContentManager() => Groups = new Dictionary<string, List<string>>();

        public bool Empty => Groups.Count == 0;
    }
}