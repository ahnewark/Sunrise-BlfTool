using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using SunriseBlfTool.BlfChunks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SunriseBlfTool
{
    public class MultiplayerPlayerVsPlayerStatistics : IBLFChunk
    {
        public PlayerVsPlayerStatistics[] players;

        public ushort GetAuthentication()
        {
            return 1;
        }

        public uint GetLength()
        {
            return 0x404;
        }

        public string GetName()
        {
            return "mps2";
        }

        public ushort GetVersion()
        {
            return 2;
        }

        public void ReadChunk(ref BitStream<StreamByteStream> hoppersStream, BLFChunkReader reader)
        {
            byte playerCount = 16;

            hoppersStream.SeekRelative(4);

            players = new PlayerVsPlayerStatistics[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                players[i] = new PlayerVsPlayerStatistics(ref hoppersStream);
            }
            hoppersStream.Seek(hoppersStream.NextByteIndex, 0);
        }

        public void WriteChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            throw new NotImplementedException();
        }
    }

    public class PlayerVsPlayerStatistics
    {
        public class VsPlayerStatistics
        {
            public short kills;
            public short deaths;

            public VsPlayerStatistics(ref BitStream<StreamByteStream> stream)
            {
                kills = (short)(stream.Read<short>(16) >> 1);
                deaths = (short)(stream.Read<short>(16) >> 1);
            }
        }

        public VsPlayerStatistics[] vs_players;

        public PlayerVsPlayerStatistics(ref BitStream<StreamByteStream> stream)
        {
            vs_players = new VsPlayerStatistics[16];
            for (int i = 0; i < 16; i++)
            {
                vs_players[i] = new VsPlayerStatistics(ref stream);
            }
        }
    }
}