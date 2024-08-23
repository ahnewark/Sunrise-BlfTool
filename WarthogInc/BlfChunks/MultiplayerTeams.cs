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
using System.Threading.Tasks;

namespace SunriseBlfTool
{
    public class MultiplayerTeams : IBLFChunk
    {
        public int unknown;
        public MultiplayerTeam[] teams;

        public ushort GetAuthentication()
        {
            return 1;
        }

        public uint GetLength()
        {
            return 0x11E4;
        }

        public string GetName()
        {
            return "mptm";
        }

        public ushort GetVersion()
        {
            return 1;
        }

        public void ReadChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            byte teamCount = 16;
            byte validTeamCount = 0;

            unknown = hoppersStream.Read<int>(32);

            teams = new MultiplayerTeam[teamCount];
            for (int i = 0; i < teamCount; i++)
            {
                MultiplayerTeam player = new MultiplayerTeam();

                bool teamExists = hoppersStream.Read<byte>(8) > 0;

                if (!teamExists)
                {
                    hoppersStream.SeekRelative(3);
                    continue;
                }

                validTeamCount++;

                player.team_standing = hoppersStream.Read<byte>(8);
                player.team_score = hoppersStream.Read<ushort>(16);

                teams[i] = player;
            }
            hoppersStream.Seek(hoppersStream.NextByteIndex, 0);

            Array.Resize(ref teams, validTeamCount);
        }

        public void WriteChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiplayerTeam
    {
        public byte team_standing;
        public ushort team_score;
    }
}
