using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Data
    {
        /// <summary>
        /// Instantiate a match in the DB
        /// </summary>
        /// <param name="player1">Player1 is rebels</param>
        /// <param name="player2">Player2 is empire</param>
        public static Guid CreateMatch(Guid player1, Guid player2)
        {
            Match m = new Match { Id = Guid.NewGuid(), Player1 = player1, Player2 = player2, Turn = player1 };
            DB.Matches.Insert(m);
            //create the match units
            MatchUnit mu;
            var rebels = DB.VanillaUnits.Where(v => v.IsRebel);
            var empire = DB.VanillaUnits.Where(v => !v.IsRebel);
            foreach (var v in rebels)
            {
                mu = new MatchUnit { MatchId = m.Id, PlayerId = player1, UnitId = v.Id, MaxHealth = v.MaxHealth, Health = v.MaxHealth, Name = v.Name, Id = Guid.NewGuid(), Col = 0, Row = 0 };
                DB.MatchUnits.Insert(mu);
            }
            foreach (var v in empire)
            {
                mu = new MatchUnit { MatchId = m.Id, PlayerId = player2, UnitId = v.Id, MaxHealth = v.MaxHealth, Health = v.MaxHealth, Name = v.Name, Id = Guid.NewGuid(), Col = 0, Row = 0 };
                DB.MatchUnits.Insert(mu);
            }

            return m.Id;
        }


        public static void DeleteMatch(Guid matchId)
        {
            DB.MatchUnits.Delete(new { MatchId = matchId });
            DB.Matches.Delete(new { Id = matchId });
        }

        public static Match GetMatch(Guid matchId)
        {
            return DB.Matches.GetSingle(new { Id = matchId });
        }


        public static IEnumerable<MatchUnit> GetUnits(Guid matchId)
        {
            return DB.MatchUnits.GetMany(new { MatchId = matchId });
        }


        public static void UpdateMatch(object values, object where)
        {
            DB.Matches.UpdateWithWhere(values: values, where: where);
        }


        public static void UpdateMatchUnit(object values, object where)
        {
            DB.MatchUnits.UpdateWithWhere(values: values, where: where);
        }

        public static void AddNotification(PlayerNotification notification)
        {
            DB.PlayerNotifications.Insert(notification);
        }


        public static IEnumerable<Match> GetMatches()
        {
            return DB.Matches.GetAll();
        }

        public static IEnumerable<Player> GetPlayers()
        {
            return DB.Players.GetAll();
        }

        public static void UpdatePlayer(Player player)
        {
            DB.Players.Update(player);
        }

        public static void UpdatePlayer(object values, object where)
        {
            DB.Players.UpdateWithWhere(values: values, where: where);
        }

        public static void PlayerIsOnline(string liveId, string clientId, double latitude, double longitude)
        {
            var location = SqlGeography.Point(latitude, longitude, 4326);
            DB.Players.UpdateWithWhere(values: new { IsOnline = true, Status = PlayerStatus.OnLine, ClientId = clientId, Location = location }, where: new { LiveId = liveId });
        }

        public static void PlayerWin(Guid playerId)
        {
            DB.Players.ExecuteNonQuery(string.Format("Update Player set Wins = Wins + 1 Where id='{0}'", playerId), new Dictionary<string, object>());
        }

        public static void PlayerLose(Guid playerId)
        {
            DB.Players.ExecuteNonQuery(string.Format("Update Player set Losses = Losses + 1 Where id='{0}'", playerId), new Dictionary<string, object>());
        }


        public static int ActiveMatches()
        {
            return DB.Matches.ExecuteScalar<int>("Select Count(id) from Match where Initialized=true ", new Dictionary<string, object>());
        }


        public static Challenge GetChallenge(Guid challengeId)
        {
            return DB.Challenges.GetSingle(where: new { Id = challengeId });
        }

        public static Guid GetPlayerId(string clientId)
        {
            return DB.Players.ExecuteScalar<Guid>(string.Format("Select Id From Player Where ClientId='{0}'", clientId), new Dictionary<string, object>()); ;
        }

        public static string GetPlayerName(string clientId)
        {
            return DB.Players.ExecuteScalar<string>(string.Format("Select DisplayName From Player Where ClientId='{0}'", clientId), new Dictionary<string, object>()); ;
        }


        public static string GetClientId(Guid playerId)
        {
            return DB.Players.ExecuteScalar<string>(string.Format("Select ClientId From Player Where Id='{0}'", playerId), new Dictionary<string, object>());
        }

        public static Guid? IsPlayingMatch(Guid playerId)
        {
            var id = DB.Matches.ExecuteScalar<Guid>(string.Format("Select Id From Match Where (Player1 ='{0}' OR Player2 ='{0}') AND PlayingNow=1", playerId), new Dictionary<string, object>());
            if (id == Guid.Empty)
                return null;
            return id;
        }

        public static Guid ChallengePlayer(Guid fromId, Guid toId)
        {
            var c = new Challenge { Id = Guid.NewGuid(), Player1 = fromId, Player2 = toId };
            DB.Challenges.Insert(c);
            return c.Id;
        }

        public static void AcceptChallenge(Guid challengeId)
        {
            //accepting the challenge means to delete it
            DeleteChallenge(challengeId);
        }

        public static void RejectChallenge(Guid challengeId)
        {
            DeleteChallenge(challengeId);
        }


        private static void DeleteChallenge(Guid challengeId)
        {
            DB.Challenges.Delete(where: new { Id = challengeId });
        }

        public static int OnlinePlayers()
        {
            return DB.Players.ExecuteScalar<int>("Select Count(id) from Player where IsOnline=true ", new Dictionary<string, object>());
        }


        public static IEnumerable<Player> GetOnlinePlayers()
        {
            return DB.Players.GetMany(new { IsOnline = true }, Needletail.DataAccess.Engines.FilterType.AND, new { Wins = "DESC" }, null);
        }

        public static IEnumerable<Player> GetOnlinePlayers(string liveId)
        {
            return DB.Players.GetMany("Select *", string.Format("IsOnline = 1 AND LiveId<>'{0}'", liveId), "Wins DESC");
        }

        public static IEnumerable<Player> GetOnlinePlayersNearBy(string liveId)
        {
            string select = string.Format("DECLARE @Iam_at geography SET @Iam_at = (select Location from player where LiveId='{0}')", liveId);
            string where = string.Format("@Iam_at.STDistance(Location)<100  and IsOnline = 1 And LiveId <> '{0}'", liveId);
            string orderBy = "Wins";
            return DB.Players.GetMany(string.Format("{0} Select * ", select), where, orderBy);
        }

        public static IEnumerable<Player> GetTop10Players()
        {
            return DB.Players.GetMany(new { }, Needletail.DataAccess.Engines.FilterType.AND, new { Wins = "DESC" }, 10);
        }

        public static Player GetPlayer(Guid playerId)
        {
            return DB.Players.GetSingle(where: new { Id = playerId });
        }
    }
}
