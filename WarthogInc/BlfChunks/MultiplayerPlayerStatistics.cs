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
    public class MultiplayerPlayerStatistics : IBLFChunk
    {
        public PlayerStatistics[] players;

        public ushort GetAuthentication()
        {
            return 1;
        }

        public uint GetLength()
        {
            return 0x4184;
        }

        public string GetName()
        {
            return "mps1";
        }

        public ushort GetVersion()
        {
            return 2;
        }

        public void ReadChunk(ref BitStream<StreamByteStream> hoppersStream, BLFChunkReader reader)
        {
            byte playerCount = 16;

            hoppersStream.SeekRelative(4);

            players = new PlayerStatistics[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                players[i] = new PlayerStatistics(ref hoppersStream);
            }
            hoppersStream.Seek(hoppersStream.NextByteIndex, 0);
        }

        public void WriteChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            throw new NotImplementedException();
        }
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


    public class Medals
    {
        public short extermination;
        public short perfection;
        public short multiple_kill_2;
        public short multiple_kill_3;
        public short multiple_kill_4;
        public short multiple_kill_5;
        public short multiple_kill_6;
        public short multiple_kill_7;
        public short multiple_kill_8;
        public short multiple_kill_9;
        public short multiple_kill_10;
        public short kills_in_a_row_5;
        public short kills_in_a_row_10;
        public short kills_in_a_row_15;
        public short kills_in_a_row_20;
        public short kills_in_a_row_25;
        public short kills_in_a_row_30;
        public short sniper_kill_5;
        public short sniper_kill_10;
        public short shotgun_kill_5;
        public short shotgun_kill_10;
        public short collision_kill_5;
        public short collision_kill_10;
        public short sword_kill_5;
        public short sword_kill_10;
        public short juggernaut_kill_5;
        public short juggernaut_kill_10;
        public short zombie_kill_5;
        public short zombie_kill_10;
        public short human_kill_5;
        public short human_kill_10;
        public short human_kill_15;
        public short koth_kill_5;
        public short shotgun_kill_sword;
        public short vehicle_impact_kill;
        public short vehicle_hijack;
        public short aircraft_hijack;
        public short deadplayer_kill;
        public short player_kill_spreeplayer;
        public short spartanlaser_kill;
        public short stickygrenade_kill;
        public short sniper_kill;
        public short bashbehind_kill;
        public short bash_kill;
        public short flame_kill;
        public short driver_assist_gunner;
        public short assault_bomb_planted;
        public short assault_player_kill_carrier;
        public short vip_player_kill_vip;
        public short juggernaut_player_kill_juggernaut;
        public short oddball_carrier_kill_player;
        public short ctf_flag_captured;
        public short ctf_flag_player_kill_carrier;
        public short ctf_flag_carrier_kill_player;
        public short infection_survive;
        public short nemesis;
        public short avenger;
        public short unused3;

        public Medals(ref BitStream<StreamByteStream> stream)
        {
            extermination = (short)(stream.Read<short>(16) >> 1);
            perfection = (short)(stream.Read<short>(16) >> 1);
            multiple_kill_2 = (short)(stream.Read<short>(16) >> 1);
            multiple_kill_3 = (short)(stream.Read<short>(16) >> 1);
            multiple_kill_4 = (short)(stream.Read<short>(16) >> 1);
            multiple_kill_5 = (short)(stream.Read<short>(16) >> 1);
            multiple_kill_6 = (short)(stream.Read<short>(16) >> 1);
            multiple_kill_7 = (short)(stream.Read<short>(16) >> 1);
            multiple_kill_8 = (short)(stream.Read<short>(16) >> 1);
            multiple_kill_9 = (short)(stream.Read<short>(16) >> 1);
            multiple_kill_10 = (short)(stream.Read<short>(16) >> 1);
            kills_in_a_row_5 = (short)(stream.Read<short>(16) >> 1);
            kills_in_a_row_10 = (short)(stream.Read<short>(16) >> 1);
            kills_in_a_row_15 = (short)(stream.Read<short>(16) >> 1);
            kills_in_a_row_20 = (short)(stream.Read<short>(16) >> 1);
            kills_in_a_row_25 = (short)(stream.Read<short>(16) >> 1);
            kills_in_a_row_30 = (short)(stream.Read<short>(16) >> 1);
            sniper_kill_5 = (short)(stream.Read<short>(16) >> 1);
            sniper_kill_10 = (short)(stream.Read<short>(16) >> 1);
            shotgun_kill_5 = (short)(stream.Read<short>(16) >> 1);
            shotgun_kill_10 = (short)(stream.Read<short>(16) >> 1);
            collision_kill_5 = (short)(stream.Read<short>(16) >> 1);
            collision_kill_10 = (short)(stream.Read<short>(16) >> 1);
            sword_kill_5 = (short)(stream.Read<short>(16) >> 1);
            sword_kill_10 = (short)(stream.Read<short>(16) >> 1);
            juggernaut_kill_5 = (short)(stream.Read<short>(16) >> 1);
            juggernaut_kill_10 = (short)(stream.Read<short>(16) >> 1);
            zombie_kill_5 = (short)(stream.Read<short>(16) >> 1);
            zombie_kill_10 = (short)(stream.Read<short>(16) >> 1);
            human_kill_5 = (short)(stream.Read<short>(16) >> 1);
            human_kill_10 = (short)(stream.Read<short>(16) >> 1);
            human_kill_15 = (short)(stream.Read<short>(16) >> 1);
            koth_kill_5 = (short)(stream.Read<short>(16) >> 1);
            shotgun_kill_sword = (short)(stream.Read<short>(16) >> 1);
            vehicle_impact_kill = (short)(stream.Read<short>(16) >> 1);
            vehicle_hijack = (short)(stream.Read<short>(16) >> 1);
            aircraft_hijack = (short)(stream.Read<short>(16) >> 1);
            deadplayer_kill = (short)(stream.Read<short>(16) >> 1);
            player_kill_spreeplayer = (short)(stream.Read<short>(16) >> 1);
            spartanlaser_kill = (short)(stream.Read<short>(16) >> 1);
            stickygrenade_kill = (short)(stream.Read<short>(16) >> 1);
            sniper_kill = (short)(stream.Read<short>(16) >> 1);
            bashbehind_kill = (short)(stream.Read<short>(16) >> 1);
            bash_kill = (short)(stream.Read<short>(16) >> 1);
            flame_kill = (short)(stream.Read<short>(16) >> 1);
            driver_assist_gunner = (short)(stream.Read<short>(16) >> 1);
            assault_bomb_planted = (short)(stream.Read<short>(16) >> 1);
            assault_player_kill_carrier = (short)(stream.Read<short>(16) >> 1);
            vip_player_kill_vip = (short)(stream.Read<short>(16) >> 1);
            juggernaut_player_kill_juggernaut = (short)(stream.Read<short>(16) >> 1);
            oddball_carrier_kill_player = (short)(stream.Read<short>(16) >> 1);
            ctf_flag_captured = (short)(stream.Read<short>(16) >> 1);
            ctf_flag_player_kill_carrier = (short)(stream.Read<short>(16) >> 1);
            ctf_flag_carrier_kill_player = (short)(stream.Read<short>(16) >> 1);
            infection_survive = (short)(stream.Read<short>(16) >> 1);
            nemesis = (short)(stream.Read<short>(16) >> 1);
            avenger = (short)(stream.Read<short>(16) >> 1);
            unused3 = (short)(stream.Read<short>(16) >> 1);
        }
    }

    public class Achievements
    {
        public short landfall;
        public short holdout;
        public short the_road;
        public short assault;
        public short cleansing;
        public short refuge;
        public short last_stand;
        public short the_key;
        [JsonPropertyName("return")]
        public short _return;
        public short campaign_complete_normal;
        public short campaign_complete_heroic;
        public short campaign_complete_legendary;
        public short iron;
        public short black_eye;
        public short tough_luck;
        [JsonPropertyName("catch")]
        public short _catch;
        public short fog;
        public short famine;
        public short thunderstorm;
        public short tilt;
        public short mythic;
        public short marathon_man;
        public short guerilla;
        public short demon;
        public short cavalier;
        public short askar;
        public short exterminator;
        public short ranger;
        public short vanguard;
        public short orpheus;
        public short reclaimer;
        public short graduate;
        public short unsc_spartan;
        public short spartan_officer;
        public short two_for_one;
        public short triple_kill;
        public short overkill;
        public short lee_r_wilson_memorial;
        public short killing_frenzy;
        public short steppin_razor;
        public short mongoose_mowdown;
        public short up_close_and_personal;
        public short mvp;
        public short maybe_next_time_buddy;
        public short too_close_to_the_sun;
        public short we_re_in_for_some_chop;
        public short fear_the_pink_mist;
        public short headshot_honcho;
        public short used_car_salesman;

        public Achievements(ref BitStream<StreamByteStream> stream)
        {
            landfall = (short)(stream.Read<short>(16) >> 1);
            holdout = (short)(stream.Read<short>(16) >> 1);
            the_road = (short)(stream.Read<short>(16) >> 1);
            assault = (short)(stream.Read<short>(16) >> 1);
            cleansing = (short)(stream.Read<short>(16) >> 1);
            refuge = (short)(stream.Read<short>(16) >> 1);
            last_stand = (short)(stream.Read<short>(16) >> 1);
            the_key = (short)(stream.Read<short>(16) >> 1);
            _return = (short)(stream.Read<short>(16) >> 1);
            campaign_complete_normal = (short)(stream.Read<short>(16) >> 1);
            campaign_complete_heroic = (short)(stream.Read<short>(16) >> 1);
            campaign_complete_legendary = (short)(stream.Read<short>(16) >> 1);
            iron = (short)(stream.Read<short>(16) >> 1);
            black_eye = (short)(stream.Read<short>(16) >> 1);
            tough_luck = (short)(stream.Read<short>(16) >> 1);
            _catch = (short)(stream.Read<short>(16) >> 1);
            fog = (short)(stream.Read<short>(16) >> 1);
            famine = (short)(stream.Read<short>(16) >> 1);
            thunderstorm = (short)(stream.Read<short>(16) >> 1);
            tilt = (short)(stream.Read<short>(16) >> 1);
            mythic = (short)(stream.Read<short>(16) >> 1);
            marathon_man = (short)(stream.Read<short>(16) >> 1);
            guerilla = (short)(stream.Read<short>(16) >> 1);
            demon = (short)(stream.Read<short>(16) >> 1);
            cavalier = (short)(stream.Read<short>(16) >> 1);
            askar = (short)(stream.Read<short>(16) >> 1);
            exterminator = (short)(stream.Read<short>(16) >> 1);
            ranger = (short)(stream.Read<short>(16) >> 1);
            vanguard = (short)(stream.Read<short>(16) >> 1);
            orpheus = (short)(stream.Read<short>(16) >> 1);
            reclaimer = (short)(stream.Read<short>(16) >> 1);
            graduate = (short)(stream.Read<short>(16) >> 1);
            unsc_spartan = (short)(stream.Read<short>(16) >> 1);
            spartan_officer = (short)(stream.Read<short>(16) >> 1);
            two_for_one = (short)(stream.Read<short>(16) >> 1);
            triple_kill = (short)(stream.Read<short>(16) >> 1);
            overkill = (short)(stream.Read<short>(16) >> 1);
            lee_r_wilson_memorial = (short)(stream.Read<short>(16) >> 1);
            killing_frenzy = (short)(stream.Read<short>(16) >> 1);
            steppin_razor = (short)(stream.Read<short>(16) >> 1);
            mongoose_mowdown = (short)(stream.Read<short>(16) >> 1);
            up_close_and_personal = (short)(stream.Read<short>(16) >> 1);
            mvp = (short)(stream.Read<short>(16) >> 1);
            maybe_next_time_buddy = (short)(stream.Read<short>(16) >> 1);
            too_close_to_the_sun = (short)(stream.Read<short>(16) >> 1);
            we_re_in_for_some_chop = (short)(stream.Read<short>(16) >> 1);
            fear_the_pink_mist = (short)(stream.Read<short>(16) >> 1);
            headshot_honcho = (short)(stream.Read<short>(16) >> 1);
            used_car_salesman = (short)(stream.Read<short>(16) >> 1);
        }
    }

    public class DamageStatistics
    {
        public class DamageSourceStatistics
        {
            public bool valid;
            public short kills;
            public short deaths;
            public short betrayals;
            public short suicides;
            public short headshots;

            public DamageSourceStatistics(ref BitStream<StreamByteStream> stream)
            {
                valid = stream.Read<byte>(8) > 0;
                stream.SeekRelative(8);
                kills = (short)(stream.Read<short>(16) >> 1);
                deaths = (short)(stream.Read<short>(16) >> 1);
                betrayals = (short)(stream.Read<short>(16) >> 1);
                suicides = (short)(stream.Read<short>(16) >> 1);
                headshots = (short)(stream.Read<short>(16) >> 1);
            }
        }

        public DamageSourceStatistics guardians;
        public DamageSourceStatistics falling_damage;
        public DamageSourceStatistics generic_collision_damage;
        public DamageSourceStatistics generic_melee_damage;
        public DamageSourceStatistics generic_explosion;
        public DamageSourceStatistics magnum_pistol;
        public DamageSourceStatistics plasma_pistol;
        public DamageSourceStatistics needler;
        public DamageSourceStatistics excavator;
        public DamageSourceStatistics smg;
        public DamageSourceStatistics plasma_rifle;
        public DamageSourceStatistics battle_rifle;
        public DamageSourceStatistics carbine;
        public DamageSourceStatistics shotgun;
        public DamageSourceStatistics sniper_rifle;
        public DamageSourceStatistics beam_rifle;
        public DamageSourceStatistics assault_rifle;
        public DamageSourceStatistics spike_rifle;
        public DamageSourceStatistics flak_cannon;
        public DamageSourceStatistics missile_launcher;
        public DamageSourceStatistics rocket_launcher;
        public DamageSourceStatistics spartan_laser;
        public DamageSourceStatistics brute_shot;
        public DamageSourceStatistics flame_thrower;
        public DamageSourceStatistics sentinal_gun;
        public DamageSourceStatistics energy_sword;
        public DamageSourceStatistics gravity_hammer;
        public DamageSourceStatistics frag_grenade;
        public DamageSourceStatistics plasma_grenade;
        public DamageSourceStatistics claymore_grenade;
        public DamageSourceStatistics firebomb_grenade;
        public DamageSourceStatistics flag_melee_damage;
        public DamageSourceStatistics bomb_melee_damage;
        public DamageSourceStatistics bomb_explosion_damage;
        public DamageSourceStatistics ball_melee_damage;
        public DamageSourceStatistics human_turret;
        public DamageSourceStatistics plasma_cannon;
        public DamageSourceStatistics plasma_mortar;
        public DamageSourceStatistics plasma_turret;
        public DamageSourceStatistics shade_turret;
        public DamageSourceStatistics banshee;
        public DamageSourceStatistics ghost;
        public DamageSourceStatistics mongoose;
        public DamageSourceStatistics scorpion;
        public DamageSourceStatistics scorpion_gunner;
        public DamageSourceStatistics spectre_driver;
        public DamageSourceStatistics spectre_gunner;
        public DamageSourceStatistics warthog_driver;
        public DamageSourceStatistics warthog_gunner;
        public DamageSourceStatistics warthog_gunner_gauss;
        public DamageSourceStatistics wraith;
        public DamageSourceStatistics wraith_anti_infantry;
        public DamageSourceStatistics tank;
        public DamageSourceStatistics chopper;
        public DamageSourceStatistics hornet;
        public DamageSourceStatistics mantis;
        public DamageSourceStatistics mauler;
        public DamageSourceStatistics sentinel_beam;
        public DamageSourceStatistics sentinel_rpg;
        public DamageSourceStatistics teleporter;
        public DamageSourceStatistics prox_mine;

        public DamageStatistics(ref BitStream<StreamByteStream> stream)
        {
            guardians = new DamageSourceStatistics(ref stream);
            falling_damage = new DamageSourceStatistics(ref stream);
            generic_collision_damage = new DamageSourceStatistics(ref stream);
            generic_melee_damage = new DamageSourceStatistics(ref stream);
            generic_explosion = new DamageSourceStatistics(ref stream);
            magnum_pistol = new DamageSourceStatistics(ref stream);
            plasma_pistol = new DamageSourceStatistics(ref stream);
            needler = new DamageSourceStatistics(ref stream);
            excavator = new DamageSourceStatistics(ref stream);
            smg = new DamageSourceStatistics(ref stream);
            plasma_rifle = new DamageSourceStatistics(ref stream);
            battle_rifle = new DamageSourceStatistics(ref stream);
            carbine = new DamageSourceStatistics(ref stream);
            shotgun = new DamageSourceStatistics(ref stream);
            sniper_rifle = new DamageSourceStatistics(ref stream);
            beam_rifle = new DamageSourceStatistics(ref stream);
            assault_rifle = new DamageSourceStatistics(ref stream);
            spike_rifle = new DamageSourceStatistics(ref stream);
            flak_cannon = new DamageSourceStatistics(ref stream);
            missile_launcher = new DamageSourceStatistics(ref stream);
            rocket_launcher = new DamageSourceStatistics(ref stream);
            spartan_laser = new DamageSourceStatistics(ref stream);
            brute_shot = new DamageSourceStatistics(ref stream);
            flame_thrower = new DamageSourceStatistics(ref stream);
            sentinal_gun = new DamageSourceStatistics(ref stream);
            energy_sword = new DamageSourceStatistics(ref stream);
            gravity_hammer = new DamageSourceStatistics(ref stream);
            frag_grenade = new DamageSourceStatistics(ref stream);
            plasma_grenade = new DamageSourceStatistics(ref stream);
            claymore_grenade = new DamageSourceStatistics(ref stream);
            firebomb_grenade = new DamageSourceStatistics(ref stream);
            flag_melee_damage = new DamageSourceStatistics(ref stream);
            bomb_melee_damage = new DamageSourceStatistics(ref stream);
            bomb_explosion_damage = new DamageSourceStatistics(ref stream);
            ball_melee_damage = new DamageSourceStatistics(ref stream);
            human_turret = new DamageSourceStatistics(ref stream);
            plasma_cannon = new DamageSourceStatistics(ref stream);
            plasma_mortar = new DamageSourceStatistics(ref stream);
            plasma_turret = new DamageSourceStatistics(ref stream);
            shade_turret = new DamageSourceStatistics(ref stream);
            banshee = new DamageSourceStatistics(ref stream);
            ghost = new DamageSourceStatistics(ref stream);
            mongoose = new DamageSourceStatistics(ref stream);
            scorpion = new DamageSourceStatistics(ref stream);
            scorpion_gunner = new DamageSourceStatistics(ref stream);
            spectre_driver = new DamageSourceStatistics(ref stream);
            spectre_gunner = new DamageSourceStatistics(ref stream);
            warthog_driver = new DamageSourceStatistics(ref stream);
            warthog_gunner = new DamageSourceStatistics(ref stream);
            warthog_gunner_gauss = new DamageSourceStatistics(ref stream);
            wraith = new DamageSourceStatistics(ref stream);
            wraith_anti_infantry = new DamageSourceStatistics(ref stream);
            tank = new DamageSourceStatistics(ref stream);
            chopper = new DamageSourceStatistics(ref stream);
            hornet = new DamageSourceStatistics(ref stream);
            mantis = new DamageSourceStatistics(ref stream);
            mauler = new DamageSourceStatistics(ref stream);
            sentinel_beam = new DamageSourceStatistics(ref stream);
            sentinel_rpg = new DamageSourceStatistics(ref stream);
            teleporter = new DamageSourceStatistics(ref stream);
            prox_mine = new DamageSourceStatistics(ref stream);
        }
    }

    public class PlayerStatistics
    {
        public Statistics statistics;
        public Medals medals;
        public Achievements achievements;
        public DamageStatistics damageStatistics;

        public PlayerStatistics(ref BitStream<StreamByteStream> stream)
        {
            statistics = new Statistics(ref stream);
            medals = new Medals(ref stream);
            achievements = new Achievements(ref stream);
            damageStatistics = new DamageStatistics(ref stream);
        }
    }
}