using System.Collections.Generic;

namespace RNMessageSystem.ActionParams
{
    public class SceneInitParams
    {
        public string DownloadLink;
        public List<Track> Tracks;

        public class Track
        {
            public string Id;
        }
    }
}