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
    public class MultiplayerTeamStatistics : IBLFChunk
    {
        public Statistics[] team_statistics;

        public ushort GetAuthentication()
        {
            return 1;
        }

        public uint GetLength()
        {
            return 0x664;
        }

        public string GetName()
        {
            return "mps3";
        }

        public ushort GetVersion()
        {
            return 2;
        }

        public void ReadChunk(ref BitStream<StreamByteStream> hoppersStream, BLFChunkReader reader)
        {
            byte teamCount = 16;

            hoppersStream.SeekRelative(4);

            team_statistics = new Statistics[teamCount];
            for (int i = 0; i < teamCount; i++)
            {
                team_statistics[i] = new Statistics(ref hoppersStream);
            }
            hoppersStream.Seek(hoppersStream.NextByteIndex, 0);
        }

        public void WriteChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            throw new NotImplementedException();
        }


        public class Statistics
        {
            public short games_played;
            public short games_completed;
            public short games_won;
            public short games_tied;
            public short rounds_completed;
            public short rounds_won;
            public short in_round_score;
            public short in_game_total_score;
            public short kills;
            public short assists;
            public short deaths;
            public short betrayals;
            public short suicides;
            public short most_kills_in_a_row;
            public short seconds_alive;
            public short ctf_flag_scores;
            public short ctf_flag_grabs;
            public short ctf_flag_carrier_kills;
            public short ctf_flag_returns;
            public short assault_bomb_arms;
            public short assault_bomb_grabs;
            public short assault_bomb_disarms;
            public short assault_bomb_detonations;
            public short oddball_time_with_ball;
            public short oddball_unused;
            public short oddball_kills_as_carrier;
            public short oddball_ball_carrier_kills;
            public short king_time_on_hill;
            public short king_total_control_time;
            public short king_unused0;
            public short king_unused1;
            public short unused0;
            public short unused1;
            public short unused2;
            public short vip_takedowns;
            public short vip_kills_as_vip;
            public short vip_guard_time;
            public short vip_time_as_vip;
            public short vip_lives_as_vip;
            public short juggernaut_kills;
            public short juggernaut_kills_as_juggernaut;
            public short juggernaut_total_control_time;
            public short total_wp;
            public short juggernaut_unused;
            public short territories_owned;
            public short territories_captures;
            public short territories_ousts;
            public short territories_time_in_territory;
            public short infection_zombie_kills;
            public short infection_infections;
            public short infection_time_as_human;

            public Statistics(ref BitStream<StreamByteStream> stream)
            {
                games_played = (short)(stream.Read<short>(16) >> 1);
                games_completed = (short)(stream.Read<short>(16) >> 1);
                games_won = (short)(stream.Read<short>(16) >> 1);
                games_tied = (short)(stream.Read<short>(16) >> 1);
                rounds_completed = (short)(stream.Read<short>(16) >> 1);
                rounds_won = (short)(stream.Read<short>(16) >> 1);
                in_round_score = (short)(stream.Read<short>(16) >> 1);
                in_game_total_score = (short)(stream.Read<short>(16) >> 1);
                kills = (short)(stream.Read<short>(16) >> 1);
                assists = (short)(stream.Read<short>(16) >> 1);
                deaths = (short)(stream.Read<short>(16) >> 1);
                betrayals = (short)(stream.Read<short>(16) >> 1);
                suicides = (short)(stream.Read<short>(16) >> 1);
                most_kills_in_a_row = (short)(stream.Read<short>(16) >> 1);
                seconds_alive = (short)(stream.Read<short>(16) >> 1);
                ctf_flag_scores = (short)(stream.Read<short>(16) >> 1);
                ctf_flag_grabs = (short)(stream.Read<short>(16) >> 1);
                ctf_flag_carrier_kills = (short)(stream.Read<short>(16) >> 1);
                ctf_flag_returns = (short)(stream.Read<short>(16) >> 1);
                assault_bomb_arms = (short)(stream.Read<short>(16) >> 1);
                assault_bomb_grabs = (short)(stream.Read<short>(16) >> 1);
                assault_bomb_disarms = (short)(stream.Read<short>(16) >> 1);
                assault_bomb_detonations = (short)(stream.Read<short>(16) >> 1);
                oddball_time_with_ball = (short)(stream.Read<short>(16) >> 1);
                oddball_unused = (short)(stream.Read<short>(16) >> 1);
                oddball_kills_as_carrier = (short)(stream.Read<short>(16) >> 1);
                oddball_ball_carrier_kills = (short)(stream.Read<short>(16) >> 1);
                king_time_on_hill = (short)(stream.Read<short>(16) >> 1);
                king_total_control_time = (short)(stream.Read<short>(16) >> 1);
                king_unused0 = (short)(stream.Read<short>(16) >> 1);
                king_unused1 = (short)(stream.Read<short>(16) >> 1);
                unused0 = (short)(stream.Read<short>(16) >> 1);
                unused1 = (short)(stream.Read<short>(16) >> 1);
                unused2 = (short)(stream.Read<short>(16) >> 1);
                vip_takedowns = (short)(stream.Read<short>(16) >> 1);
                vip_kills_as_vip = (short)(stream.Read<short>(16) >> 1);
                vip_guard_time = (short)(stream.Read<short>(16) >> 1);
                vip_time_as_vip = (short)(stream.Read<short>(16) >> 1);
                vip_lives_as_vip = (short)(stream.Read<short>(16) >> 1);
                juggernaut_kills = (short)(stream.Read<short>(16) >> 1);
                juggernaut_kills_as_juggernaut = (short)(stream.Read<short>(16) >> 1);
                juggernaut_total_control_time = (short)(stream.Read<short>(16) >> 1);
                total_wp = (short)(stream.Read<short>(16) >> 1);
                juggernaut_unused = (short)(stream.Read<short>(16) >> 1);
                territories_owned = (short)(stream.Read<short>(16) >> 1);
                territories_captures = (short)(stream.Read<short>(16) >> 1);
                territories_ousts = (short)(stream.Read<short>(16) >> 1);
                territories_time_in_territory = (short)(stream.Read<short>(16) >> 1);
                infection_zombie_kills = (short)(stream.Read<short>(16) >> 1);
                infection_infections = (short)(stream.Read<short>(16) >> 1);
                infection_time_as_human = (short)(stream.Read<short>(16) >> 1);
            }
        }
    }
}