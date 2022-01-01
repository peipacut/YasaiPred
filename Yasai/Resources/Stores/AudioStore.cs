﻿using System;
using ManagedBass;
using Yasai.Audio;

namespace Yasai.Resources.Stores
{
    public class AudioStore : Store<AudioStream>
    {
        public override string[] FileTypes => new[] { ".mp3", ".wav", ".ogg" };
        public override IResourceArgs DefaultArgs => new AudioArgs(0);
        protected override AudioStream AcquireResource(string path, IResourceArgs args)
        {
            int stream = Bass.CreateStream(path);
            if (stream == 0)
                throw new Exception("could not open stream");

            return new AudioStream(stream);
        }
    }

    public class AudioArgs : IResourceArgs
    {
        public int Offset;

        public AudioArgs(int offset)
        {
            Offset = offset;
        }
    }
}