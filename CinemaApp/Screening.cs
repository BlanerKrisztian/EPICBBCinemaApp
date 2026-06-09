namespace CinemaApp
{
    public class Screening
    {
        private readonly string _title;
        private readonly int _totalSeats;
        private List<string> _bookedNames = new();

        // title nem lehet null vagy üres, totalSeats >= 1
        public Screening(string title, int totalSeats)
        {
            if (title is null || title.Trim() == "" || title == "" || totalSeats <= 0)
            {
                throw new ArgumentException();
            }
            _title = title.Trim();
            _totalSeats = totalSeats;
        }

        public string GetTitle()
        {
            return _title;
        }

        // Visszatér false ha nincs szabad hely, vagy a személy már foglalt
        public bool BookSeat(string name)
        {
            
            if (_bookedNames.Contains(name) || IsHouseFull())
            {
                return false;
            }
            else
            {

                _bookedNames.Add(name);
                Console.WriteLine();
                return true;
            }
        }

        // Visszatér false ha a személy neve nem szerepel a _bookedNames listában
        public bool CancelBooking(string name)
        {
            if (_bookedNames.Contains(name))
            {
                _bookedNames.Remove(name);
                return true;
            }
            return false;
        }

        public bool IsBooked(string name)
        {
            if (_bookedNames.Contains(name))
            {
                return true;
            }
            return false;
        }

        // Szabad helyek = _totalSeats - _bookedNames.Count
        public int GetAvailableSeats()
        {
            int returnInt = _totalSeats - _bookedNames.Count;
            return returnInt;
        }

        public int GetBookedCount()
        {
            return _bookedNames.Count;
        }

        public bool IsHouseFull()
        {
            if (_bookedNames.Count == _totalSeats)
            {
                return true;
            }
            return false;
        }

        // -------------------------------------------------------
        // EXTRA FELADAT — Várólista
        // Az alábbi mezőt és metódusokat csak akkor vedd fel,
        // ha az alap feladattal már végzett vagy!
        // -------------------------------------------------------

        // private readonly List<string> _waitingList;

        // public bool AddToWaitingList(string name)
        // {
        //     throw new NotImplementedException();
        // }

        // public bool RemoveFromWaitingList(string name)
        // {
        //     throw new NotImplementedException();
        // }

        // public bool IsOnWaitingList(string name)
        // {
        //     throw new NotImplementedException();
        // }

        // public int GetWaitingListCount()
        // {
        //     throw new NotImplementedException();
        // }

        // public int GetWaitingPosition(string name)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
