namespace CinemaApp
{
    public class Screening
    {
        private readonly string _title;
        private readonly int _totalSeats;
        private readonly List<string> _bookedNames;

        // title nem lehet null vagy üres, totalSeats >= 1
        public Screening(string title, int totalSeats)
        {
            throw new NotImplementedException();
        }

        public string GetTitle()
        {
            throw new NotImplementedException();
        }

        // Visszatér false ha nincs szabad hely, vagy a személy már foglalt
        public bool BookSeat(string name)
        {
            throw new NotImplementedException();
        }

        // Visszatér false ha a személy neve nem szerepel a _bookedNames listában
        public bool CancelBooking(string name)
        {
            throw new NotImplementedException();
        }

        public bool IsBooked(string name)
        {
            throw new NotImplementedException();
        }

        // Szabad helyek = _totalSeats - _bookedNames.Count
        public int GetAvailableSeats()
        {
            throw new NotImplementedException();
        }

        public int GetBookedCount()
        {
            throw new NotImplementedException();
        }

        public bool IsHouseFull()
        {
            throw new NotImplementedException();
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
