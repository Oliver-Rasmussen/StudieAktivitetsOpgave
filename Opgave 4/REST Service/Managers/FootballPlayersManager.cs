using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Opgave_1;

namespace REST_Service.Managers
{
    public class FootballPlayersManager
    {
        private static int _nextId = 1;
        private static readonly List<FootballPlayer> Data = new List<FootballPlayer>
        {
            new FootballPlayer {ID = _nextId++, Name = "Anders Bjørn", Price = 55000, ShirtNumber = 54},
            new FootballPlayer {ID=_nextId++, Name = "Erik Hansen", Price = 69000, ShirtNumber = 33}
        };

        public List<FootballPlayer> GetAll()
        {
            return new List<FootballPlayer>(Data);
        }

        public FootballPlayer GetById(int id)
        {
            return Data.Find(player => player.ID == id);
        }

        public FootballPlayer Add(FootballPlayer player)
        {
            player.ID = _nextId++;
            Data.Add(player);
            return player;
        }

        public FootballPlayer Delete(int id)
        {
            FootballPlayer player = Data.Find(player1 => player1.ID == id);
            if (player == null) return null;
            Data.Remove(player);
            return player;
        }

        public FootballPlayer Update(int id, FootballPlayer updates)
        {
            FootballPlayer player = Data.Find(book1 => book1.ID == id);
            if (player == null) return null;
            player.Name = updates.Name;
            player.Price = updates.Price;
            return player;
        }
    }
}
